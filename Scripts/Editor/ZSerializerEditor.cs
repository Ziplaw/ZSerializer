using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using ZSerializer;
using ZSerializer.Editor;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

public static class ZSerializerEditor
{
    [DidReloadScripts]
    static void InitializePackage()
    {
        if (!ZSerializerSettings.Instance || ZSerializerSettings.Instance && !ZSerializerSettings.Instance.packageInitialized)
        {
            ZSerializerEditorWindow.ShowWindow();

            // ZSerializerSettings.Instance.packageInitialized = true;
            // GenerateUnityComponentClasses();
        }
    }

    [DidReloadScripts]
    static void TryRebuildZSerializers()
    {
        if (ZSerializerSettings.Instance && ZSerializerSettings.Instance.packageInitialized)
        {
            ZSerializerStyler styler = new ZSerializerStyler();
            if (styler.settings.autoRebuildZSerializers)
            {
                var types = ZSerialize.GetPersistentTypes().ToArray();

                Class[] classes = new Class[types.Length];

                for (int i = 0; i < types.Length; i++)
                {
                    classes[i] = new Class(types[i], GetClassState(types[i]));
                }

                string path;

                foreach (var c in classes)
                {
                    ClassState state = c.state;

                    if (state == ClassState.NeedsRebuilding)
                    {
                        var pathList = Directory.GetFiles("Assets", $"*{c.classType.Name}*.cs",
                            SearchOption.AllDirectories)[0].Split('.').ToList();
                        pathList.RemoveAt(pathList.Count - 1);
                        path = String.Join(".", pathList) + "ZSerializer.cs";

                        path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
                               path.Replace('\\', '/');

                        CreateZSerializer(c.classType, path);
                        AssetDatabase.Refresh();
                    }
                }
            }
        }
    }

    public static void CreateZSerializer(Type type, string path)
    {
        string newPath = new string((new string(path.Reverse().ToArray())).Substring(path.Split('/').Last().Length)
            .Reverse().ToArray());
        Debug.Log("Editor script being created at " + newPath + "ZSerializers");
        string relativePath = "Assets" + newPath.Substring(Application.dataPath.Length);


        if (!AssetDatabase.IsValidFolder(relativePath + "ZSerializers"))
        {
            Directory.CreateDirectory(newPath + "ZSerializers");
        }


        string newNewPath = newPath + "ZSerializers/" + type.Name + "ZSerializer.cs";
        
        FileStream fileStream = new FileStream(newNewPath, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fileStream);

        string script =
            "[System.Serializable]\n" +
            $"public class {type.Name}ZSerializer : ZSerializer.ZSerializer<{type.Name}>\n" +
            "{\n";

        var fieldInfos = type.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList();

        var currentType = type;

        while (type.BaseType != typeof(MonoBehaviour))
        {
            fieldInfos.AddRange(type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList());
            type = type.BaseType;
        }

        type = currentType;

        foreach (var fieldInfo in fieldInfos)
        {
            var fieldType = fieldInfo.FieldType;

            if (fieldInfo.FieldType.IsArray)
            {
                fieldType = fieldInfo.FieldType.GetElementType();
            }


            int genericParameterAmount = fieldType.GenericTypeArguments.Length;

            script +=
                $"    public {fieldInfo.FieldType} {fieldInfo.Name};\n".Replace('+', '.');

            if (genericParameterAmount > 0)
            {
                string oldString = $"`{genericParameterAmount}[";
                string newString = "<";

                var genericArguments = fieldType.GenericTypeArguments;

                for (var i = 0; i < genericArguments.Length; i++)
                {
                    oldString += genericArguments[i] + (i == genericArguments.Length - 1 ? "]" : ",");
                    newString += genericArguments[i] + (i == genericArguments.Length - 1 ? ">" : ",");
                }

                script = script.Replace(oldString, newString);
            }
        }

        string className = type.Name + "Instance";

        script +=
            $"\n    public {type.Name}ZSerializer({type.Name} {className}) : base({className}.gameObject, {className})\n" +
            "    {\n";

        foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList())
        {
            var fieldType = fieldInfo.FieldType;

            if (fieldInfo.FieldType.IsArray)
            {
                fieldType = fieldInfo.FieldType.GetElementType();
            }


            int genericParameterAmount = fieldType.GenericTypeArguments.Length;

            script +=
                $"         {fieldInfo.Name} = ({fieldInfo.FieldType})typeof({type.Name}).GetField(\"{fieldInfo.Name}\").GetValue({className});\n"
                    .Replace('+', '.');

            if (genericParameterAmount > 0)
            {
                string oldString = $"`{genericParameterAmount}[";
                string newString = "<";

                var genericArguments = fieldType.GenericTypeArguments;

                for (var i = 0; i < genericArguments.Length; i++)
                {
                    oldString += genericArguments[i] + (i == genericArguments.Length - 1 ? "]" : ",");
                    newString += genericArguments[i] + (i == genericArguments.Length - 1 ? ">" : ",");
                }

                script = script.Replace(oldString, newString);
            }
        }


        script += $"         groupID = {className}.GroupID;\n" +
                  $"         autoSync = {className}.AutoSync;\n" +
                  "    }";

        script += "\n\n    public override void RestoreValues(" + type.FullName + " component)\n  {\n";
        
        foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList())
        {
            script +="      component." + fieldInfo.Name + " = " + fieldInfo.Name + ";\n";
        }

        script += $"      component.GroupID = groupID;\n" +
                  $"      component.AutoSync = autoSync;\n"+
                  "    }";
        
        script += "\n}";

        ZSerialize.Log("ZSerializer script being created at " + newNewPath);

        sw.Write(script);

        sw.Close();
    }

    static Type[] typesImplementingCustomEditor = AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass =>
        ass.GetTypes()
            .Where(t => t.GetCustomAttributes(typeof(CustomEditor)).Any() && !t.FullName.Contains("UnityEngine.") &&
                        !t.FullName.Contains("UnityEditor.")).Select(t =>
                t.GetCustomAttribute<CustomEditor>().GetType()
                    .GetField("m_InspectedType", BindingFlags.NonPublic | BindingFlags.Instance)
                    ?.GetValue(t.GetCustomAttribute<CustomEditor>()) as Type)
            .Where(t => t.IsSubclassOf(typeof(MonoBehaviour)))).ToArray();

    public static void CreateEditorScript(Type type, string path)
    {
        if (typesImplementingCustomEditor.Contains(type))
        {
            Debug.Log($"{type} already implements a Custom Editor, and another one won't be created");
            return;
        }

        string editorScript =
            @"using UnityEditor;
using ZSerializer.Editor;

[CustomEditor(typeof(" + type.Name + @"))]
public class " + type.Name + @"Editor : PersistentMonoBehaviourEditor<" + type.Name + @"> { }";


        string newPath = new string((new string(path.Reverse().ToArray())).Substring(path.Split('/').Last().Length)
            .Reverse().ToArray());
        Debug.Log("Editor script being created at " + newPath + "Editor");
        string relativePath = "Assets" + newPath.Substring(Application.dataPath.Length);


        if (!AssetDatabase.IsValidFolder(relativePath + "Editor"))
        {
            Directory.CreateDirectory(newPath + "Editor");
        }


        string newNewPath = newPath + "Editor/Z" + type.Name + "Editor.cs";
        FileStream fileStream = new FileStream(newNewPath, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fileStream);
        sw.Write(editorScript);
        sw.Close();

        AssetDatabase.Refresh();
    }


    public static ClassState GetClassState(Type type)
    {
        Type ZSerializerType = type.Assembly.GetType(type.Name + "ZSerializer");
        if (ZSerializerType == null) return ClassState.NotMade;

        var fieldsZSerializer = ZSerializerType.GetFields()
            .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList();
        var fieldsType = type.GetFields().Where(f =>
                f.GetCustomAttribute<NonZSerialized>() == null || f.GetCustomAttribute<ForceZSerialized>() != null)
            .ToList();

        new Color(0, 0, 0, 1);

        var currentType = type;

        while (type.BaseType != typeof(MonoBehaviour))
        {
            fieldsType.AddRange(type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance).Where(f =>
                f.GetCustomAttribute<NonZSerialized>() == null || f.GetCustomAttribute<ForceZSerialized>() != null));
            type = type.BaseType;
        }

        type = currentType;

        if (fieldsZSerializer.Count == fieldsType.Count - 0)
        {
            for (int j = 0; j < fieldsZSerializer.Count; j++)
            {
                if (fieldsZSerializer[j].Name != fieldsType[j].Name ||
                    fieldsZSerializer[j].FieldType != fieldsType[j].FieldType)
                {
                    return ClassState.NeedsRebuilding;
                }
            }

            return ClassState.Valid;
        }

        return ClassState.NeedsRebuilding;
    }

    public static bool SettingsButton(bool showSettings, ZSerializerStyler styler, int width)
    {
        return GUILayout.Toggle(showSettings, styler.cogWheel, new GUIStyle("button"),
            GUILayout.MaxHeight(width), GUILayout.MaxWidth(width));
    }

    public static void BuildWindowValidityButton(Type componentType, ZSerializerStyler styler)
    {
        int width = 32;

        ClassState state = GetClassState(componentType);

        var textureToUse = GetTextureToUse(state, styler);

        if (state == ClassState.Valid)
        {
            bool defaultOnValue = ZSerializerSettings.Instance.GetDefaultOnValue(componentType);
            textureToUse = defaultOnValue ? textureToUse : styler.offImage;
        }

        if (!Application.isPlaying)
        {
            if (GUILayout.Button(textureToUse,
                GUILayout.MaxWidth(width), GUILayout.Height(width)))
            {
                if (state == ClassState.Valid)
                {
                    
                    bool newOnValue = !ZSerializerSettings.Instance.GetDefaultOnValue(componentType);
                    ZSerializerSettings.Instance.SetDefaultOnValue(componentType, newOnValue);
                    foreach (var component in Object.FindObjectsOfType(componentType).Where(c =>
                        c.GetType() == componentType && ((PersistentMonoBehaviour)c).AutoSync))
                    {
                        ((PersistentMonoBehaviour)component).isOn = newOnValue;
                        EditorUtility.SetDirty(component);
                    }

                    EditorUtility.SetDirty(ZSerializerSettings.Instance);
                }
                else
                {
                    GenerateZSerializer(componentType, state);
                }
            }
        }
    }

    private static void BuildInspectorValidityButton<T>(T component, ZSerializerStyler styler)
        where T : PersistentMonoBehaviour
    {
        int width = 28;
        ClassState state = GetClassState(typeof(T));

        var textureToUse = GetTextureToUse(state, styler);

        if (state == ClassState.Valid && component)
        {
            textureToUse = component.isOn ? styler.validImage : styler.offImage;
        }

        if (!Application.isPlaying)
        {
            if (GUILayout.Button(textureToUse,
                GUILayout.MaxWidth(width), GUILayout.Height(width)))
            {
                if (state != ClassState.Valid)
                    GenerateZSerializer(typeof(T), state);
                else
                {
                    bool componentIsOn = component.isOn;

                    if (component.AutoSync)
                    {
                        foreach (var persistentMonoBehaviour in Object.FindObjectsOfType<T>()
                            .Where(t => t.GetType() == component.GetType() && t.AutoSync))
                        {
                            persistentMonoBehaviour.isOn = !componentIsOn;
                            ZSerializerSettings.Instance.defaultOnDictionary.SetElementAt(persistentMonoBehaviour.GetType(),
                                persistentMonoBehaviour.isOn);
                            
                        }
                    }
                    else
                    {
                        component.isOn = !componentIsOn;
                    }
                    EditorUtility.SetDirty(component);
                    EditorUtility.SetDirty(ZSerializerSettings.Instance);
                }
            }
        }
    }

    static Texture2D GetTextureToUse(ClassState state, ZSerializerStyler styler)
    {
        if (styler.validImage == null) styler.GetEveryResource();

        Texture2D textureToUse = styler.validImage;

        if (state != ClassState.Valid)
        {
            textureToUse = state == ClassState.NeedsRebuilding
                ? styler.needsRebuildingImage
                : styler.notMadeImage;
        }

        return textureToUse;
    }

    static void GenerateZSerializer(Type componentType, ClassState state)
    {
        if (UnityEngine.Random.Range(0, 100) == 0)
        {
            PlayClip(Resources.Load<AudioClip>("surprise"));
        }

        var pathList = Directory.GetFiles("Assets", $"*{componentType.Name}*.cs",
            SearchOption.AllDirectories)[0].Split('.').ToList();
        pathList.RemoveAt(pathList.Count - 1);
        var path = String.Join(".", pathList) + "ZSerializer.cs";

        path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
               path.Replace('\\', '/');


        CreateZSerializer(componentType, path);
        if (state == ClassState.NotMade)
            CreateEditorScript(componentType, path);
        AssetDatabase.Refresh();
    }

    public static void PlayClip(AudioClip clip, int startSample = 0, bool loop = false)
    {
        Assembly unityEditorAssembly = typeof(AudioImporter).Assembly;

        Type audioUtilClass = unityEditorAssembly.GetType("UnityEditor.AudioUtil");
        MethodInfo method = audioUtilClass.GetMethod(
            "PlayPreviewClip",
            BindingFlags.Static | BindingFlags.Public,
            null,
            new Type[] { typeof(AudioClip), typeof(int), typeof(bool) },
            null
        );

        method?.Invoke(
            null,
            new object[] { clip, startSample, loop }
        );
    }

    public static void BuildButtonAll(Class[] classes, int width, ZSerializerStyler styler)
    {
        Texture2D textureToUse = styler.refreshImage;

        if (GUILayout.Button(textureToUse,
            GUILayout.MaxWidth(width), GUILayout.Height(width)))
        {
            string path;

            // string folderPath = EditorUtility.SaveFolderPanel("ZSerializer.cs Save Locations", "Assets", "");

            foreach (var c in classes)
            {
                var pathList = Directory.GetFiles("Assets", $"*{c.classType.Name}*.cs",
                    SearchOption.AllDirectories)[0].Split('.').ToList();
                pathList.RemoveAt(pathList.Count - 1);
                path = String.Join(".", pathList) + "ZSerializer.cs";
                path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
                       path.Replace('\\', '/');

                CreateZSerializer(c.classType, path);
                CreateEditorScript(c.classType, path);
                AssetDatabase.Refresh();
            }
        }
    }


    public static void BuildPersistentComponentEditor<T>(T manager, ZSerializerStyler styler, ref bool showSettings,
        Action<Type, ISaveGroupID, bool> toggleOn) where T : PersistentMonoBehaviour
    {
        // Texture2D cogwheel = styler.cogWheel;

        GUILayout.Space(-15);
        using (new GUILayout.HorizontalScope(ZSerializerStyler.window))
        {
            var state = GetClassState(manager.GetType());
            string color = state == ClassState.Valid ? manager.isOn ? "29cf42" : "999999" :
                state == ClassState.NeedsRebuilding ? "FFC107" : "FF625A";

            GUILayout.Label($"<color=#{color}>  Persistent Component</color>",
                styler.header, GUILayout.Height(28));
            // using (new EditorGUI.DisabledScope(GetClassState(manager.GetType()) != ClassState.Valid))
            //     editMode = GUILayout.Toggle(editMode, cogwheel, new GUIStyle("button"), GUILayout.MaxWidth(28),
            //         GUILayout.Height(28));

            BuildInspectorValidityButton(manager, styler);
            showSettings = SettingsButton(showSettings, styler, 28);
            PrefabUtility.RecordPrefabInstancePropertyModifications(manager);
        }

        if (showSettings)
        {
            toggleOn?.Invoke(typeof(PersistentMonoBehaviour), manager, true);

            SerializedObject serializedObject = new SerializedObject(manager);
            serializedObject.Update();

            foreach (var field in typeof(T).GetFields().Where(f => f.DeclaringType != typeof(PersistentMonoBehaviour)))
            {
                using (new EditorGUILayout.HorizontalScope())
                {
                    string color = field.GetCustomAttribute<NonZSerialized>() == null && manager.isOn
                        ? "29cf42"
                        : "999999";
                    GUILayout.Label($"<color=#{color}>{field.Name.FieldNameToInspectorName()}</color>",
                        new GUIStyle("label") { richText = true },
                        GUILayout.Width(EditorGUIUtility.currentViewWidth / 3f));
                    EditorGUILayout.PropertyField(serializedObject.FindProperty(field.Name), GUIContent.none);
                }
            }

            serializedObject.ApplyModifiedProperties();
        }
    }

    public static string FieldNameToInspectorName(this string value)
    {
        var charArray = value.ToCharArray();

        List<char> chars = new List<char>();

        for (int i = 0; i < charArray.Length; i++)
        {
            if (i == 0)
            {
                chars.Add(Char.ToUpper(charArray[i]));
                continue;
            }

            if (Char.IsUpper(charArray[i]))
            {
                chars.Add(' ');
                chars.Add(Char.ToUpper(charArray[i]));
            }
            else
            {
                chars.Add(charArray[i]);
            }
        }

        return new string(chars.ToArray());
    }

    public static void ShowGroupIDSettings(Type type, ISaveGroupID data, bool canAutoSync)
    {
        GUILayout.Space(-15);
        using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.window))
        {
            GUILayout.Label("Save Group", GUILayout.MaxWidth(80));
            int newValue = EditorGUILayout.Popup(data.GroupID,
                ZSerializerSettings.Instance.saveGroups.Where(s => !string.IsNullOrEmpty(s)).ToArray());
            if (newValue != data.GroupID)
            {
                if (!data.AutoSync)
                {
                    type.GetField("groupID", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public)
                        .SetValue(data, newValue);
                }
                else
                {
                    foreach (var o in GameObject.FindObjectsOfType(data.GetType())
                        .Where(t => ((ISaveGroupID)t).AutoSync))
                    {
                        o.GetType().BaseType.GetField("groupID", BindingFlags.NonPublic | BindingFlags.Instance)
                            .SetValue(o, newValue);
                    }
                }
            }

            if (canAutoSync)
            {
                SerializedObject o = new SerializedObject(data as PersistentMonoBehaviour);

                o.Update();

                EditorGUILayout.PropertyField(
                    o.FindProperty("autoSync"), GUIContent.none, GUILayout.Width(12));
                GUILayout.Label("Sync", GUILayout.Width(35));

                o.ApplyModifiedProperties();
            }
        }
    }

    public static void BuildEditModeEditor<T>(SerializedObject serializedObject, T manager, bool editMode,
        ref bool[] persistentFields, Action OnInspectorGUI)
    {
        if (editMode)
        {
            GUILayout.Label("Select fields to serialize",
                new GUIStyle("helpbox") { alignment = TextAnchor.MiddleCenter }, GUILayout.Height(18));
            var fields = manager.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance);

            for (var i = 0; i < fields.Length; i++)
            {
                using (new GUILayout.HorizontalScope())
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        SerializedProperty prop = serializedObject.FindProperty(fields[i].Name);
                        EditorGUILayout.PropertyField(prop);
                    }

                    persistentFields[i] = GUILayout.Toggle(persistentFields[i], "", GUILayout.MaxWidth(15));
                }
            }
        }
        else
        {
            OnInspectorGUI.Invoke();
        }
    }

    private static Vector2 scrollPos;

    public static void BuildSettingsEditor(ZSerializerStyler styler, ref int selectedMenu, ref int selectedType, float width)
    {
        IEnumerable<FieldInfo> fieldInfos = typeof(ZSerializerSettings)
            .GetFields(BindingFlags.Instance | BindingFlags.Public)
            .Where(f => f.GetCustomAttribute<HideInInspector>() == null);
        SerializedObject serializedObject = new SerializedObject(styler.settings);

        string[] toolbarNames;

        if (ZSerializerSettings.Instance.debugMode)
        {
            toolbarNames = new[] { "Settings", "Saving Groups", "Component Blacklist" /*, "Default On Setting"*/ };
        }
        else
        {
            toolbarNames = new[] { "Settings", "Saving Groups" };
        }

        using (new GUILayout.VerticalScope("box"))
        {
            selectedMenu = GUILayout.Toolbar(selectedMenu, toolbarNames);

            switch (selectedMenu)
            {
                case 0:

                    GUILayout.Space(-15);
                    using (new GUILayout.VerticalScope(ZSerializerStyler.window, GUILayout.Height(1)))
                    {
                        serializedObject.Update();

                        foreach (var fieldInfo in fieldInfos)
                        {
                            EditorGUI.BeginChangeCheck();

                            EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldInfo.Name));

                            if (EditorGUI.EndChangeCheck())
                            {
                                if (fieldInfo.Name == "advancedSerialization")
                                {
                                    if (!ZSerializerSettings.Instance.advancedSerialization)
                                    {
                                        foreach (var persistentGameObject in Object
                                            .FindObjectsOfType<PersistentGameObject>())
                                        {
                                            persistentGameObject.serializedComponents.Clear();
                                        }
                                    }
                                }
                            }
                        }

                        serializedObject.ApplyModifiedProperties();

                        if (GUILayout.Button("Open Save file Directory"))
                        {
                            Process process = new Process();
                            ProcessStartInfo startInfo = new ProcessStartInfo();
                            startInfo.WindowStyle = ProcessWindowStyle.Normal;
                            startInfo.FileName = "cmd.exe";
                            string _path = Application.persistentDataPath;
                            startInfo.Arguments = $"/C start {_path}";
                            process.StartInfo = startInfo;
                            process.Start();
                        }
                    }


                    break;
                case 1:
                    GUILayout.Space(-15);
                    using (new GUILayout.VerticalScope(ZSerializerStyler.window, GUILayout.Height(1)))
                    {
                        serializedObject.Update();

                        for (int i = 0; i < 16; i++)
                        {
                            using (new EditorGUI.DisabledScope(i < 2))
                            {
                                var prop = serializedObject.FindProperty("saveGroups").GetArrayElementAtIndex(i);
                                prop.stringValue = EditorGUILayout.TextArea(prop.stringValue,
                                    new GUIStyle("textField") { alignment = TextAnchor.MiddleCenter });
                            }
                        }

                        if (GUILayout.Button("Reset all Group IDs from Scene"))
                        {
                            ZSerialize.Log("<color=cyan>Resetting All Group IDs</color>");

                            foreach (var monoBehaviour in GameObject.FindObjectsOfType<MonoBehaviour>()
                                .Where(o => o is ISaveGroupID))
                            {
                                monoBehaviour.GetType().GetField("groupID",
                                        BindingFlags.NonPublic | BindingFlags.Instance)
                                    ?.SetValue(monoBehaviour, 0);
                                monoBehaviour.GetType().BaseType
                                    ?.GetField("groupID", BindingFlags.Instance | BindingFlags.NonPublic)
                                    ?.SetValue(monoBehaviour, 0);
                            }
                        }

                        serializedObject.ApplyModifiedProperties();
                    }

                    break;
                case 2:
                    if (ZSerializerSettings.Instance
                        .componentBlackList.Count > 0)
                    {
                        using (new GUILayout.HorizontalScope(GUILayout.Width(1)))
                        {
                            using (new EditorGUILayout.VerticalScope())
                            {
                                GUILayout.Space(-15);
                                using (new EditorGUILayout.VerticalScope(ZSerializerStyler.window, GUILayout.Height(1),
                                    GUILayout.Height(Mathf.Max(88,
                                        20.6f * ZSerializerSettings.Instance.componentBlackList.Count))))
                                {
                                    foreach (var serializableComponentBlackList in ZSerializerSettings.Instance
                                        .componentBlackList)
                                    {
                                        if (GUILayout.Button(serializableComponentBlackList.Type.Name,
                                            GUILayout.Width(150)))
                                        {
                                            selectedType =
                                                ZSerializerSettings.Instance.componentBlackList.IndexOf(
                                                    serializableComponentBlackList);
                                        }
                                    }
                                }
                            }


                            using (new EditorGUILayout.VerticalScope())
                            {
                                GUILayout.Space(-15);
                                using (new EditorGUILayout.VerticalScope(ZSerializerStyler.window, GUILayout.Height(1)))
                                {
                                    using (var scrollView =
                                        new GUILayout.ScrollViewScope(scrollPos, new GUIStyle(),
                                            GUILayout.Width(width - 196),
                                            GUILayout.Height(Mathf.Max(61.8f,
                                                20.6f * ZSerializerSettings.Instance.componentBlackList.Count))))
                                    {
                                        scrollPos = scrollView.scrollPosition;
                                        foreach (var componentName in ZSerializerSettings.Instance
                                            .componentBlackList[selectedType]
                                            .componentNames)
                                        {
                                            GUILayout.Label(componentName);
                                        }
                                    }
                                }
                            }
                        }

                        if (GUILayout.Button("Delete Blacklist"))
                        {
                            ZSerializerSettings.Instance.componentBlackList.Clear();
                            EditorUtility.SetDirty(ZSerializerSettings.Instance);
                            AssetDatabase.SaveAssets();
                            selectedType = 0;
                            GenerateUnityComponentClasses();
                        }
                        if (GUILayout.Button("Open ZSerializer Configurator"))
                        {
                            ZSerializerFineTuner.ShowWindow();
                        }
                    }
                    else
                    {
                        GUILayout.Label("The Component Blacklist is Empty.",
                            new GUIStyle("label") { alignment = TextAnchor.MiddleCenter });
                        if (GUILayout.Button("Open Fine Tuner"))
                        {
                            ZSerializerFineTuner.ShowWindow();
                        }
                    }

                    break;
                // case 3:
                //     for (var i = 0; i < ZSerializerSettings.Instance.defaultOnDictionary.keyList.Count; i++)
                //     {
                //         using (new GUILayout.HorizontalScope())
                //         {
                //             GUILayout.Label(ZSerializerSettings.Instance.defaultOnDictionary.keyList[i], GUILayout.Width(200));//
                //             GUILayout.Label(ZSerializerSettings.Instance.defaultOnDictionary.valueList[i].ToString());
                //         }    
                //     }
                //     break;
            }
        }
    }

    [MenuItem("Tools/ZSerializer/Generate Unity Component ZSerializers")]
    public static void GenerateUnityComponentClasses()
    {
        string longScript = "";

        IEnumerable<Type> types = ZSerialize.ComponentSerializableTypes;
        foreach (var type in types)
        {
            if (type != typeof(PersistentGameObject))
            {
                longScript +=
                    "[System.Serializable]\npublic class " + type.Name + "ZSerializer : ZSerializer.ZSerializer<" +
                    type.FullName +
                    "> {\n";

                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(ZSerialize.PropertyIsSuitableForZSerializer))
                {

                    longScript +=
                        $"    public {propertyInfo.PropertyType.ToString().Replace('+', '.')} " + propertyInfo.Name +
                        ";\n";
                }

                foreach (var fieldInfo in type
                    .GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => f.GetCustomAttribute<ObsoleteAttribute>() == null))
                {
                    var fieldType = fieldInfo.FieldType;

                    if (fieldInfo.FieldType.IsArray)
                    {
                        fieldType = fieldInfo.FieldType.GetElementType();
                    }

                    int genericParameterAmount = fieldType.GenericTypeArguments.Length;

                    longScript +=
                        $"    public {fieldInfo.FieldType.ToString().Replace('+', '.')} " + fieldInfo.Name +
                        ";\n";

                    if (genericParameterAmount > 0)
                    {
                        string oldString = $"`{genericParameterAmount}[";
                        string newString = "<";

                        var genericArguments = fieldType.GenericTypeArguments;

                        for (var i = 0; i < genericArguments.Length; i++)
                        {
                            oldString += genericArguments[i].ToString().Replace('+', '.') +
                                         (i == genericArguments.Length - 1 ? "]" : ",");
                            newString += genericArguments[i].ToString().Replace('+', '.') +
                                         (i == genericArguments.Length - 1 ? ">" : ",");
                        }

                        longScript = longScript.Replace(oldString, newString);
                    }
                }

                longScript += "    public " + type.Name + "ZSerializer (" + type.FullName + " " + type.Name +
                              "Instance) : base(" +
                              type.Name + "Instance.gameObject, " + type.Name + "Instance ) {\n";

                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(ZSerialize.PropertyIsSuitableForZSerializer))
                {
                    longScript +=
                        $"        " + propertyInfo.Name + " = " + type.Name + "Instance." + propertyInfo.Name + ";\n";
                }

                foreach (var fieldInfo in type
                    .GetFields(BindingFlags.Public | BindingFlags.Instance)
                    .Where(f => f.GetCustomAttribute<ObsoleteAttribute>() == null))
                {
                    longScript +=
                        $"        " + fieldInfo.Name + " = " + type.Name + "Instance." + fieldInfo.Name + ";\n";
                }
                longScript += "    }\n";


                longScript +=
@"    public override void RestoreValues(" + type.FullName + @" component)
    {
";                
                foreach (var propertyInfo in type
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                    .Where(ZSerialize.PropertyIsSuitableForZSerializer))
                {
                    longScript +=
                        $"        component." + propertyInfo.Name + " = " + propertyInfo.Name + ";\n";
                }

                longScript += "    }\n";
                longScript += "}\n";
            }
        }

        if (!Directory.Exists(Application.dataPath + "/ZResources"))
            Directory.CreateDirectory(Application.dataPath + "/ZResources");

        FileStream fs = new FileStream(
            Application.dataPath + "/ZResources/UnityComponentZSerializers.cs",
            FileMode.Create);
        StreamWriter sw = new StreamWriter(fs);

        sw.Write(longScript);
        sw.Close();

        AssetDatabase.Refresh();
        ZSerialize.Log("Unity Component ZSerializers built");
    }
}