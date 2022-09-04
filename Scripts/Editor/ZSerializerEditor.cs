using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEditor.Experimental.SceneManagement;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.Experimental.Rendering;
using UnityEngine.SceneManagement;
using ZSerializer;
using ZSerializer.Editor;
using Debug = UnityEngine.Debug;
using Object = UnityEngine.Object;

[assembly: InternalsVisibleTo("ZSerializer.Runtime")]

namespace ZSerializer.Editor
{
    public static class ZSerializerEditor
    {
        [DidReloadScripts]
        static void InitializePackage()
        {
            if (!ZSerializerSettings.Instance ||
                ZSerializerSettings.Instance && !ZSerializerSettings.Instance.packageInitialized)
            {
                ZSerializerMenu.ShowWindow();
            }
        }

        [DidReloadScripts]
        static void TryRebuildZSerializers()
        {
            if (ZSerializerSettings.Instance && ZSerializerSettings.Instance.packageInitialized)
            {
                ZSerializerStyler styler = new ZSerializerStyler();
                if (styler.settings.autoRebuildSerializers)
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
            string newPath = new string(new string(path.Reverse().ToArray()).Substring(path.Split('/').Last().Length)
                .Reverse().ToArray());
            string relativePath = "Assets" + newPath.Substring(Application.dataPath.Length);

            var ns = type.Namespace;


            if (!AssetDatabase.IsValidFolder(relativePath + "ZSerializers"))
            {
                Directory.CreateDirectory(newPath + "ZSerializers");
            }


            string newNewPath = newPath + "ZSerializers/" + type.Name + "ZSerializer.cs";

            FileStream fileStream = new FileStream(newNewPath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fileStream);


            string script =
                $"{(string.IsNullOrEmpty(ns) ? "" : $"namespace {ns} " + "{\n")}[System.Serializable]\n" +
                $"public sealed class {type.Name}ZSerializer : ZSerializer.Internal.ZSerializer\n" +
                "{\n";

            var fieldInfos = GetFieldsThatShouldBeSerialized(type);

            var currentType = type;

            // while (type.BaseType != typeof(MonoBehaviour))
            // {
            //     fieldInfos.AddRange(type.BaseType.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
            //         .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList());
            //     type = type.BaseType;
            // }

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
                    $"    public {CustomSerializables.ParseFieldType(fieldInfo.FieldType).ToString().Replace('+', '.')} {fieldInfo.Name};\n";

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

                    script = script.Replace(oldString, newString);
                }
            }

//             script += @"    public int groupID;
//     public bool autoSync;
// ";

            script +=
                $"\n    public {type.Name}ZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)\n" +
                "    {" +
                "       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];\n";

            foreach (var fieldInfo in fieldInfos)
            {
                // var fieldType = fieldInfo.FieldType;

                // if (fieldInfo.FieldType.IsArray)
                // {
                //     fieldType = fieldInfo.FieldType.GetElementType();
                // }


                // int genericParameterAmount = fieldType.GenericTypeArguments.Length;

                script +=
                    $"         {fieldInfo.Name} = ({fieldInfo.FieldType.MakeGenericStringInterpretation()})typeof({fieldInfo.DeclaringType.FullName}).GetField(\"{fieldInfo.Name}\"{(!fieldInfo.IsPublic ? ", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic" : "")}).GetValue(instance);\n";

                // if (genericParameterAmount > 0)
                // {
                //     string oldString = $"`{genericParameterAmount}[";
                //     string newString = "<";
                //
                //     var genericArguments = fieldType.GenericTypeArguments;
                //
                //     for (var i = 0; i < genericArguments.Length; i++)
                //     {
                //         oldString += genericArguments[i].ToString().Replace('+', '.') +
                //                      (i == genericArguments.Length - 1 ? "]" : ",");
                //         newString += genericArguments[i].ToString().Replace('+', '.') +
                //                      (i == genericArguments.Length - 1 ? ">" : ",");
                //     }
                //
                //     script = script.Replace(oldString, newString);
                // }
            }


            // script += $"         groupID = (int)typeof({type.FullName}).GetProperty(\"GroupID\").GetValue(instance);\n" +
            //           $"         autoSync = (bool)typeof({type.FullName}).GetProperty(\"AutoSync\").GetValue(instance);\n" +
            //           "    }";

            script += "    }";

            script += "\n\n    public override void RestoreValues(UnityEngine.Component component)\n    {\n";

            foreach (var fieldInfo in fieldInfos)
            {
                var parsedFieldType = CustomSerializables.ParseFieldType(fieldInfo.FieldType);

                script +=
                    $"         typeof({fieldInfo.DeclaringType.FullName}).GetField(\"{fieldInfo.Name}\"{(!fieldInfo.IsPublic ? ", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic" : "")}).SetValue(component, {(parsedFieldType != fieldInfo.FieldType ? $"({fieldInfo.FieldType.MakeGenericStringInterpretation()})" : "")}{fieldInfo.Name});\n";
            }

            // script += $"         typeof({type.FullName}).GetProperty(\"GroupID\").SetValue(component, groupID);\n" +
            //           $"         typeof({type.FullName}).GetProperty(\"AutoSync\").SetValue(component, autoSync);\n" +
            //           "    }";
            script += "    }";

            script += "\n}";

            if (!string.IsNullOrEmpty(ns)) script += "\n}";

            ZSerialize.Log("ZSerializer script being created at " + newNewPath, DebugMode.Off);

            sw.Write(script);

            sw.Close();

            foreach (var persistentGameObject in Object.FindObjectsOfType<PersistentMonoBehaviour>())
            {
                persistentGameObject.GenerateEditorZUIDs(false);
            }
        }

        public static string MakeGenericStringInterpretation(this Type t)
        {
            if (!t.IsGenericType) return t.FullName;
            
            int genericParameterAmount = t.GenericTypeArguments.Length;
            var s = $"{t.GetGenericTypeDefinition().FullName.Replace('+', '.').Replace($"`{genericParameterAmount}","<")}";
            if (genericParameterAmount > 0)
            {
                var genericArguments = t.GenericTypeArguments;
            
                for (var i = 0; i < genericArguments.Length; i++)
                {
                    s += MakeGenericStringInterpretation(genericArguments[i]) +
                         (i == genericArguments.Length - 1 ? ">" : ",");
                }
            }

            return s;

            // script +=
            //     $"         {fieldInfo.Name} = ({fieldInfo.FieldType.ToString().Replace('+', '.')})typeof({fieldInfo.DeclaringType.FullName}).GetField(\"{fieldInfo.Name}\"{(!fieldInfo.IsPublic ? ", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic" : "")}).GetValue(instance);\n";
            //
            // if (genericParameterAmount > 0)
            // {
            //     string oldString = $"`{genericParameterAmount}[";
            //     string newString = "<";
            //
            //     var genericArguments = fieldType.GenericTypeArguments;
            //
            //     for (var i = 0; i < genericArguments.Length; i++)
            //     {
            //         oldString += genericArguments[i].ToString().Replace('+', '.') +
            //                      (i == genericArguments.Length - 1 ? "]" : ",");
            //         newString += genericArguments[i].ToString().Replace('+', '.') +
            //                      (i == genericArguments.Length - 1 ? ">" : ",");
            //     }
            //
            //     script = script.Replace(oldString, newString);
        }


        static Type[] typesImplementingCustomEditor = AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass =>
            ass.GetTypes()
                .Where(t => t.GetCustomAttributes(typeof(CustomEditor)).Any() && !t.FullName.Contains("UnityEngine.") &&
                            !t.FullName.Contains("UnityEditor.")).Select(t =>
                    t.GetCustomAttribute<CustomEditor>().GetType()
                        .GetField("m_InspectedType", BindingFlags.NonPublic | BindingFlags.Instance)
                        ?.GetValue(t.GetCustomAttribute<CustomEditor>()) as Type)
                .Where(t => t.IsSubclassOf(typeof(MonoBehaviour)))).ToArray();


        static List<FieldInfo> GetFieldsThatShouldBeSerialized(Type type)
        {
            //keep an eye on this, seems fishy

            var fieldInfos = type.GetFields()
                .Where(f =>
                {
                    return (f.GetCustomAttribute(typeof(NonZSerialized)) == null ||
                            f.GetCustomAttribute<ForceZSerialized>() != null) &&
                           (f.FieldType.IsSerializable || typeof(Object).IsAssignableFrom(f.FieldType) ||
                            (f.FieldType.FullName ?? f.FieldType.Name).StartsWith("UnityEngine.")) /*&&
                           (!f.FieldType.IsGenericType || f.FieldType.GetGenericTypeDefinition() != typeof(Dictionary<,>))*/
                        ;
                }).ToList();

            while (type != typeof(MonoBehaviour))
            {
                fieldInfos.AddRange(type.GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                    .Where(f =>
                    {
                        return !f.IsFamily &&
                               f.DeclaringType == type &&
                               f.GetCustomAttribute<SerializeField>() != null &&
                               f.GetCustomAttribute<NonZSerialized>() == null &&
                               (f.FieldType.IsSerializable ||
                                typeof(Object).IsAssignableFrom(f.FieldType) ||
                                (f.FieldType.FullName ?? f.FieldType.Name).StartsWith("UnityEngine.")) &&
                               (f.FieldType.IsGenericType
                                   ? f.FieldType.GetGenericTypeDefinition() != typeof(Dictionary<,>)
                                   : true);
                    }));
                type = type.BaseType;
            }

            return fieldInfos;
        }


        public static ClassState GetClassState(Type type)
        {
            Type ZSerializerType = type.Assembly.GetType(type.FullName + "ZSerializer");
            if (ZSerializerType == null) return ClassState.NotMade;

            var fieldsZSerializer = ZSerializerType.GetFields()
                .Where(f => f.GetCustomAttribute(typeof(NonZSerialized)) == null).ToList();

            var fieldTypes = GetFieldsThatShouldBeSerialized(type);

            new Color(0, 0, 0, 1);
            if (fieldsZSerializer.Count == fieldTypes.Count)
            {
                for (int j = 0; j < fieldsZSerializer.Count; j++)
                {
                    if (fieldsZSerializer[j].Name != fieldTypes[j].Name ||
                        fieldsZSerializer[j].FieldType != CustomSerializables.ParseFieldType(fieldTypes[j].FieldType))
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
                bool defaultOnValue = ZSerializerSettings.Instance.componentDataDictionary[componentType].isOn;
                textureToUse = defaultOnValue ? textureToUse : styler.offImage;
            }

            if (!Application.isPlaying)
            {
                if (GUILayout.Button(textureToUse,
                        GUILayout.MaxWidth(width), GUILayout.Height(width)))
                {
                    if (state == ClassState.Valid)
                    {
                        bool newOnValue = !ZSerializerSettings.Instance.componentDataDictionary[componentType].isOn;

                        if (!EditorUtility.DisplayDialog($"Turning {(newOnValue ? "on" : "off")} {componentType.Name}",
                                $"This will turn {(newOnValue ? "on" : "off")} all other synced versions of this component, even in other Scenes and in Prefabs. \n Are you sure you want to do it?",
                                "Yes", "No")) return;

                        var eligibleTypes = AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass => ass.GetTypes())
                            .Where(t => t.IsSubclassOf(componentType)).ToList();

                        bool alsoDoChildClasses = false;

                        if (eligibleTypes.Count > 0)
                            alsoDoChildClasses = EditorUtility.DisplayDialog(
                                $"Also turn {(newOnValue ? "on" : "off")} child classes?",
                                $"This class has {eligibleTypes.Count} classes inheriting from it \n Would you like to turn those {(newOnValue ? "on" : "off")} as well?",
                                "Yes", "No");
                        if (!alsoDoChildClasses) eligibleTypes.Clear();

                        eligibleTypes.Add(componentType);

                        ExecuteOnAllSerializablesFromScenes(zs =>
                        {
                            if (eligibleTypes.Contains(zs.GetType()) && zs.AutoSync)
                            {
                                zs.IsOn = newOnValue;
                                EditorUtility.SetDirty(zs as Object);
                            }
                        }, "Turning " + componentType.Name + " " + (newOnValue ? "off" : "on"));

                        ExecuteOnAllSerializablesFromPrefabs(zs =>
                        {
                            if (eligibleTypes.Contains(zs.GetType()) && zs.AutoSync)
                            {
                                zs.IsOn = newOnValue;
                                EditorUtility.SetDirty(zs as Object);
                            }
                        }, "Turning " + componentType.Name + " " + (newOnValue ? "off" : "on"));

                        eligibleTypes.ForEach(t =>
                            ZSerializerSettings.Instance.componentDataDictionary[t].isOn = newOnValue);


                        EditorUtility.SetDirty(ZSerializerSettings.Instance);
                        AssetDatabase.SaveAssets();
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
            ClassState state = GetClassState(component.GetType());

            var textureToUse = GetTextureToUse(state, styler);

            if (state == ClassState.Valid && component)
            {
                textureToUse = component.IsOn ? styler.validImage : styler.offImage;
            }

            if (!Application.isPlaying)
            {
                if (GUILayout.Button(textureToUse,
                        GUILayout.MaxWidth(width), GUILayout.Height(width)))
                {
                    if (state != ClassState.Valid)
                        GenerateZSerializer(component.GetType(), state);
                    else
                    {
                        bool componentIsOn = component.IsOn;

                        if (component.AutoSync)
                        {
                            if (!EditorUtility.DisplayDialog(
                                    $"Turning {(componentIsOn ? "off" : "on")} {component.GetType().Name}",
                                    $"This will turn {(componentIsOn ? "off" : "on")} all other synced versions of this component, even in other Scenes and in Prefabs. \n Are you sure you want to do it?",
                                    "Yes", "No")) return;

                            var eligibleTypes = AppDomain.CurrentDomain.GetAssemblies()
                                .SelectMany(ass => ass.GetTypes())
                                .Where(t => t.IsSubclassOf(component.GetType())).ToList();

                            bool alsoDoChildClasses = false;

                            if (eligibleTypes.Count > 0)
                                alsoDoChildClasses = !EditorUtility.DisplayDialog(
                                    $"Also turn {(!componentIsOn ? "off" : "on")} child classes?",
                                    $"This class has {eligibleTypes.Count} classes inheriting from it \n Would you like to turn those {(!componentIsOn ? "off" : "on")} as well?",
                                    "Yes", "No");
                            if (!alsoDoChildClasses) eligibleTypes.Clear();

                            eligibleTypes.Add(component.GetType());


                            ExecuteOnAllSerializablesFromScenes(zs =>
                            {
                                if (eligibleTypes.Contains(zs.GetType()) && zs.AutoSync)
                                {
                                    zs.IsOn = !componentIsOn;
                                    EditorUtility.SetDirty(zs as Object);
                                }
                            }, "Turning " + component.GetType().Name + " " + (componentIsOn ? "off" : "on"));

                            ExecuteOnAllSerializablesFromPrefabs(zs =>
                            {
                                if (eligibleTypes.Contains(zs.GetType()) && zs.AutoSync)
                                {
                                    zs.IsOn = !componentIsOn;
                                    EditorUtility.SetDirty(zs as Object);
                                }
                            }, "Turning " + component.GetType().Name + " " + (componentIsOn ? "off" : "on"));

                            eligibleTypes.ForEach(t =>
                                ZSerializerSettings.Instance.componentDataDictionary[t].isOn = !componentIsOn);

                            component.IsOn = !componentIsOn;
                        }
                        else
                        {
                            component.IsOn = !componentIsOn;
                        }


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
            var pathList = Directory.GetFiles("Assets", $"*{componentType.Name}*.cs",
                SearchOption.AllDirectories)[0].Split('.').ToList();
            pathList.RemoveAt(pathList.Count - 1);
            var path = String.Join(".", pathList) + "ZSerializer.cs";

            path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
                   path.Replace('\\', '/');


            CreateZSerializer(componentType, path);
            AssetDatabase.Refresh();
        }

        public static void BuildButtonAll(Class[] classes, int width, ZSerializerStyler styler)
        {
            Texture2D textureToUse = styler.refreshImage;

            if (GUILayout.Button(textureToUse,
                    GUILayout.MaxWidth(width), GUILayout.Height(width)))
            {
                string path;

                foreach (var c in classes)
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


        internal static void BuildPersistentComponentEditor<T>(T manager, ZSerializerStyler styler,
            ref bool showSettings,
            Action<Type, IZSerializable, bool> toggleOn) where T : PersistentMonoBehaviour
        {
            // Texture2D cogwheel = styler.cogWheel;

            GUILayout.Space(-15);
            using (new GUILayout.HorizontalScope(ZSerializerStyler.Window))
            {
                var state = GetClassState(manager.GetType());
                string color = state == ClassState.Valid
                    ? manager.IsOn ? ZSerializerStyler.MainHex : ZSerializerStyler.OffHex
                    : state == ClassState.NeedsRebuilding
                        ? ZSerializerStyler.YellowHex
                        : ZSerializerStyler.RedHex;

                GUILayout.Label($"<color=#{color}>  Persistent Component</color>",
                    styler.header, GUILayout.Height(28));
                // using (new EditorGUI.DisabledScope(GetClassState(manager.GetType()) != ClassState.Valid))
                //     editMode = GUILayout.Toggle(editMode, cogwheel, new GUIStyle("button"), GUILayout.MaxWidth(28),
                //         GUILayout.Height(28));

                BuildInspectorValidityButton(manager, styler);
                showSettings = SettingsButton(showSettings, styler, 28);
                if (manager) PrefabUtility.RecordPrefabInstancePropertyModifications(manager);
            }

            if (showSettings)
            {
                toggleOn?.Invoke(manager.GetType(), manager, true);

                if (ZSerializerSettings.Instance.debugMode == DebugMode.Developer)
                {
                    GUILayout.Space(-15);
                    using (new GUILayout.VerticalScope(ZSerializerStyler.Window))
                    {
                        if (GUILayout.Button("Reset ZUIDs"))
                        {
                            manager.GenerateEditorZUIDs(false);
                        }
                    }
                }

                SerializedObject serializedObject = new SerializedObject(manager);
                serializedObject.Update();

                var fields = GetFieldsThatShouldBeSerialized(manager.GetType())
                    .Where(f => f.DeclaringType != typeof(PersistentMonoBehaviour)).ToList();

                fields.AddRange(manager.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).Where(f =>
                    f.GetCustomAttribute<NonZSerialized>() != null &&
                    f.DeclaringType != typeof(PersistentMonoBehaviour)));

                foreach (var field in fields)
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        var image = field.GetCustomAttribute<NonZSerialized>() == null && manager.IsOn
                            ? ZSerializerStyler.Instance.validImage
                            : ZSerializerStyler.Instance.offImage;

                        GUILayout.Label(image, GUILayout.Width(30), GUILayout.Height(20));
                        // GUILayout.Label($"<color=#{color}>{field.Name.FieldNameToInspectorName()}</color>",
                        //     new GUIStyle("label") { richText = true },
                        //     GUILayout.Width(EditorGUIUtility.currentViewWidth / 3f));

                        EditorGUILayout.PropertyField(serializedObject.FindProperty(field.Name));
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

        internal static void ShowGroupIDSettings(Type type, IZSerializable data, bool canAutoSync)
        {
            GUILayout.Space(-15);
            using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.Window))
            {
                GUILayout.Label("Save Group", GUILayout.MaxWidth(80));
                int newValue = EditorGUILayout.Popup(data.GroupID,
                    ZSerializerSettings.Instance.saveGroups.Where(s => !string.IsNullOrEmpty(s)).ToArray());
                if (newValue != data.GroupID)
                {
                    data.GroupID = newValue;
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

        public static void BuildSettingsEditor(ZSerializerStyler styler, ref int selectedMenu, ref int selectedType,
            ref int selectedGroup, ref int selectedComponentSettings, ref int selectedGroupIndex,
            float width)
        {
            IEnumerable<FieldInfo> fieldInfos = typeof(ZSerializerSettings)
                .GetFields(BindingFlags.Instance | BindingFlags.Public)
                .Where(f => f.GetCustomAttribute<HideInInspector>() == null);
            SerializedObject serializedObject = new SerializedObject(styler.settings);

            string[] toolbarNames = { "Settings", "Groups", "Component Settings" };

            using (new GUILayout.VerticalScope("box"))
            {
                selectedMenu = GUILayout.Toolbar(selectedMenu, toolbarNames);

                switch (selectedMenu)
                {
                    case 0:

                        GUILayout.Space(-15);
                        using (new GUILayout.VerticalScope(ZSerializerStyler.Window, GUILayout.Height(1)))
                        {
                            serializedObject.Update();
                            GUILayout.Space(-15);
                            using (new GUILayout.VerticalScope(ZSerializerStyler.Window, GUILayout.Height(1)))
                            {
                                ZSerializerStyler.BigLabel("Settings", ZSerializerStyler.MainColor);

                                foreach (var fieldInfo in fieldInfos)
                                {
                                    EditorGUI.BeginChangeCheck();

                                    EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldInfo.Name));
                                }

                                serializedObject.ApplyModifiedProperties();
#if UNITY_EDITOR_WIN
                                if (GUILayout.Button("Open Save file Directory"))
                                {
                                    Process process = new Process();
                                    ProcessStartInfo startInfo = new ProcessStartInfo();
                                    startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                                    startInfo.FileName = "cmd.exe";
                                    string _path = Application.persistentDataPath;
                                    startInfo.Arguments = $"/C \"start {_path}\"";
                                    process.StartInfo = startInfo;
                                    process.Start();
                                }
                            }
#endif
                            GUILayout.Space(10);
                            // ZSerializerStyler.DangerzoneColor

                            GUILayout.Space(-15);

                            using (new GUILayout.VerticalScope(ZSerializerStyler.Window, GUILayout.Height(1)))
                            {
                                ZSerializerStyler.BigLabel("Danger Zone", ZSerializerStyler.Red);
                                if (GUILayout.Button("Delete Current Save File"))
                                {
                                    if (ZSerialize.DeleteSaveFile(ZSerializerSettings.Instance.selectedSaveFile))
                                    {
                                        var path = ZSerialize.GetSaveFileDataPath(ZSerializerSettings.Instance
                                            .selectedSaveFile);
                                        ZSerialize.Log(
                                            $"Save file {ZSerializerSettings.Instance.selectedSaveFile} deleted. ({path})");
                                    }
                                }

                                if (GUILayout.Button("Delete All Save Files"))
                                {
                                    ZSerialize.DeleteAllSaveFiles();
                                    ZSerialize.Log("All save files have been deleted.");
                                }
                            }
                        }

                        break;
                    case 1:


                        GUILayout.Space(-15);
                        using (new GUILayout.VerticalScope(ZSerializerStyler.Window, GUILayout.Height(1)))
                        {
                            var groupsNames = new[] { "Serialization Groups", "Scene Groups" };
                            selectedGroup = GUILayout.Toolbar(selectedGroup, groupsNames);

                            serializedObject.Update();
                            switch (selectedGroup)
                            {
                                case 0:

                                    for (int i = 0; i < 16; i++)
                                    {
                                        using (new EditorGUI.DisabledScope(i < 2))
                                        {
                                            var prop = serializedObject.FindProperty("saveGroups")
                                                .GetArrayElementAtIndex(i);
                                            prop.stringValue = EditorGUILayout.TextArea(prop.stringValue,
                                                new GUIStyle("textField") { alignment = TextAnchor.MiddleCenter });
                                        }
                                    }

                                    if (GUILayout.Button("Reset all Group IDs from Scene"))
                                    {
                                        ZSerialize.Log("<color=cyan>Resetting All Group IDs</color>", DebugMode.Off);

                                        foreach (var monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>()
                                                     .Where(o => o is IZSerializable))
                                        {
                                            var serialize = monoBehaviour as IZSerializable;
                                            serialize.GroupID = 0;
                                            EditorUtility.SetDirty(monoBehaviour);

                                            // monoBehaviour.GetType().GetField("groupID",
                                            //         BindingFlags.NonPublic | BindingFlags.Instance)
                                            //     ?.SetValue(monoBehaviour, 0);
                                            // monoBehaviour.GetType().BaseType
                                            //     ?.GetField("groupID", BindingFlags.Instance | BindingFlags.NonPublic)
                                            //     ?.SetValue(monoBehaviour, 0);
                                        }
                                    }

                                    break;
                                case 1:

                                    // ZSerializerSettings.Instance.sceneGroups.Count;
                                    var groupsProp = serializedObject.FindProperty("sceneGroups");
                                    for (int i = 0; i < ZSerializerSettings.Instance.sceneGroups.Count; i++)
                                    {
                                        if (selectedGroupIndex == -1)
                                        {
                                            using (new EditorGUILayout.HorizontalScope())
                                            {
                                                var togglePrev = selectedGroupIndex == i;
                                                var toggle = GUILayout.Toggle(selectedGroupIndex == i, groupsProp
                                                    .GetArrayElementAtIndex(i)
                                                    .FindPropertyRelative("name").stringValue, new GUIStyle("button"));
                                                if (togglePrev != toggle)
                                                    selectedGroupIndex = toggle ? i : -1;


                                                if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                                                {
                                                    ZSerializerSettings.Instance.sceneGroups.RemoveAt(i);
                                                    EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                                    AssetDatabase.SaveAssets();
                                                    return;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (selectedGroupIndex == i)
                                            {
                                                var togglePrev = selectedGroupIndex == i;
                                                var toggle = GUILayout.Toggle(selectedGroupIndex == i, groupsProp
                                                    .GetArrayElementAtIndex(i)
                                                    .FindPropertyRelative("name").stringValue, new GUIStyle("button"));
                                                if (togglePrev != toggle)
                                                    selectedGroupIndex = toggle ? i : -1;

                                                if (toggle)
                                                {
                                                    EditorGUILayout.PropertyField(groupsProp.GetArrayElementAtIndex(i)
                                                        .FindPropertyRelative("name"));


                                                    var sceneAssetList = ZSerializerSettings.Instance.sceneGroups[i]
                                                        .scenePaths.Select(s =>
                                                            string.IsNullOrEmpty(s)
                                                                ? null
                                                                : AssetDatabase.LoadAssetAtPath<SceneAsset>(
                                                                    Path.Combine("Assets", s + ".unity"))).ToList();


                                                    using (new EditorGUILayout.VerticalScope("box"))
                                                    {
                                                        GUILayout.Label("Scenes",
                                                            new GUIStyle("label")
                                                                { alignment = TextAnchor.MiddleCenter });

                                                        for (var j = 0; j < sceneAssetList.Count; j++)
                                                        {
                                                            var sceneAsset = sceneAssetList[j];
                                                            using (new GUILayout.HorizontalScope())
                                                            {
                                                                if (!EditorBuildSettings.scenes
                                                                        .Select(s =>
                                                                            s.path.Length > 7
                                                                                ? s.path.Substring(7,
                                                                                    s.path.Length - 13)
                                                                                : null).Contains(ZSerializerSettings
                                                                            .Instance.sceneGroups[i].scenePaths[j]) &&
                                                                    sceneAsset != null)
                                                                    if (GUILayout.Button(
                                                                            new GUIContent(
                                                                                Resources.Load<Texture2D>("warning"),
                                                                                "Scene not present in Build Settings"),
                                                                            new GUIStyle("label"), GUILayout.Width(20),
                                                                            GUILayout.Height(20)))
                                                                        EditorWindow.GetWindow(
                                                                            Type.GetType(
                                                                                "UnityEditor.BuildPlayerWindow,UnityEditor"));

                                                                EditorGUI.BeginChangeCheck();
                                                                var newSceneAsset = EditorGUILayout.ObjectField(
                                                                    sceneAsset,
                                                                    typeof(SceneAsset), false) as SceneAsset;
                                                                if (EditorGUI.EndChangeCheck())
                                                                {
                                                                    var newPath =
                                                                        AssetDatabase.GetAssetPath(newSceneAsset);
                                                                    var scenePathProperty = groupsProp
                                                                        .GetArrayElementAtIndex(i)
                                                                        .FindPropertyRelative("scenePaths");
                                                                    scenePathProperty.GetArrayElementAtIndex(
                                                                            sceneAssetList
                                                                                .IndexOf(sceneAsset)).stringValue =
                                                                        newPath.Substring(7, newPath.Length - 13);

                                                                    EditorUtility.SetDirty(ZSerializerSettings
                                                                        .Instance);
                                                                    AssetDatabase.SaveAssets();
                                                                }

                                                                if (GUILayout.Button("-", GUILayout.MaxWidth(20)))
                                                                {
                                                                    ZSerializerSettings.Instance.sceneGroups[i]
                                                                        .scenePaths.RemoveAt(j);
                                                                    EditorUtility.SetDirty(ZSerializerSettings
                                                                        .Instance);
                                                                    AssetDatabase.SaveAssets();
                                                                    return;
                                                                }
                                                            }
                                                        }

                                                        if (GUILayout.Button("+"))
                                                        {
                                                            ZSerializerSettings.Instance.sceneGroups[i].scenePaths
                                                                .Add("");
                                                            EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                                            AssetDatabase.SaveAssets();
                                                        }
                                                    }

                                                    // EditorGUILayout.PropertyField(groupsProp.GetArrayElementAtIndex(i)
                                                    //     .FindPropertyRelative("loadingManagementScene"));
                                                }
                                            }
                                        }
                                    }


                                    if (selectedGroupIndex == -1 || ZSerializerSettings.Instance.sceneGroups.Count == 0)
                                        if (GUILayout.Button("+"))
                                        {
                                            selectedGroupIndex = -1;
                                            ZSerializerSettings.Instance.sceneGroups.Add(new SceneGroup
                                                { name = "New Scene Group" });
                                            EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                            AssetDatabase.SaveAssets();
                                        }

                                    break;
                            }

                            serializedObject.ApplyModifiedProperties();
                        }

                        break;
                    case 2:

                        var componentSettings = new[] { "Blacklist", "Whitelist" };
                        if (GUILayout.Button("Build ZSerializers"))
                        {
                            ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                        }

                        selectedComponentSettings = GUILayout.Toolbar(selectedComponentSettings, componentSettings);

                        switch (selectedComponentSettings)
                        {
                            case 0:
                                if (ZSerializerSettings.Instance
                                        .unityComponentDataList.Count > 0)
                                {
                                    using (new GUILayout.HorizontalScope(GUILayout.Width(1)))
                                    {
                                        using (new EditorGUILayout.VerticalScope())
                                        {
                                            GUILayout.Space(-15);
                                            using (new EditorGUILayout.VerticalScope(ZSerializerStyler.Window,
                                                       GUILayout.Height(1),
                                                       GUILayout.Height(Mathf.Max(88,
                                                           20.6f * ZSerializerSettings.Instance.unityComponentDataList
                                                               .Count))))
                                            {
                                                foreach (var serializableComponentBlackList in ZSerializerSettings
                                                             .Instance
                                                             .unityComponentDataList
                                                             .Where(data => data.componentNames.Count > 0))
                                                {
                                                    if (GUILayout.Button(serializableComponentBlackList.Type.Name,
                                                            GUILayout.Width(150)))
                                                    {
                                                        selectedType =
                                                            ZSerializerSettings.Instance.unityComponentDataList.IndexOf(
                                                                serializableComponentBlackList);
                                                    }
                                                }
                                            }
                                        }


                                        using (new EditorGUILayout.VerticalScope())
                                        {
                                            GUILayout.Space(-15);
                                            using (new EditorGUILayout.VerticalScope(ZSerializerStyler.Window,
                                                       GUILayout.Height(1)))
                                            {
                                                using (var scrollView =
                                                       new GUILayout.ScrollViewScope(scrollPos, new GUIStyle(),
                                                           GUILayout.Width(width - 196),
                                                           GUILayout.Height(Mathf.Max(61.8f,
                                                               20.6f * ZSerializerSettings.Instance
                                                                   .unityComponentDataList
                                                                   .Count(data => data.componentNames.Count > 0)))))
                                                {
                                                    scrollPos = scrollView.scrollPosition;

                                                    if (selectedType < ZSerializerSettings.Instance
                                                            .unityComponentDataList.Count && ZSerializerSettings
                                                            .Instance
                                                            .unityComponentDataList[selectedType].componentNames !=
                                                        null)

                                                        foreach (var componentName in ZSerializerSettings.Instance
                                                                     .unityComponentDataList[selectedType]
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
                                        ZSerializerSettings.Instance.DefaultComponentData();
                                        EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                        AssetDatabase.SaveAssets();
                                        selectedType = 0;
                                        ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                                    }

                                    if (GUILayout.Button("Open ZSerializer Configurator"))
                                    {
                                        ZSerializerConfigurator.ShowWindow();
                                    }
                                }
                                else
                                {
                                    GUILayout.Label("The Component Blacklist is Empty.",
                                        new GUIStyle("label") { alignment = TextAnchor.MiddleCenter });
                                    if (GUILayout.Button("Open Fine Tuner"))
                                    {
                                        ZSerializerConfigurator.ShowWindow();
                                    }
                                }

                                break;
                            case 1:

                                var list = ZSerializerSettings.Instance.unityComponentTypes
                                    .Select(t => Type.GetType(t).Name).ToList();

                                GUILayout.Space(-15);
                                using (new GUILayout.VerticalScope(ZSerializerStyler.Window))
                                {
                                    for (var i = 0; i < list.Count; i++)
                                    {
                                        var typeName = list[i];
                                        using (new EditorGUILayout.HorizontalScope())
                                        {
                                            GUILayout.Label(typeName,
                                                new GUIStyle("box")
                                                    { alignment = TextAnchor.MiddleCenter, stretchWidth = true });
                                            if (GUILayout.Button("-", GUILayout.Width(20)))
                                            {
                                                ZSerializerSettings.Instance.unityComponentTypes.RemoveAt(i);
                                                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                                AssetDatabase.SaveAssets();
                                            }
                                        }
                                    }
                                }

                                if (GUILayout.Button("Delete All"))
                                {
                                    ZSerializerSettings.Instance.unityComponentTypes.Clear();
                                    EditorUtility.SetDirty(ZSerializerSettings.Instance);
                                    AssetDatabase.SaveAssets();
                                }

                                break;
                        }


                        break;
                    // case 3:
                    //     for (var i = 0; i < ZSerializerSettings.Instance.defaultOnDictionary.typeNames.Count; i++)
                    //     {
                    //         using (new GUILayout.HorizontalScope())
                    //         {
                    //             GUILayout.Label(ZSerializerSettings.Instance.defaultOnDictionary.typeNames[i], GUILayout.Width(200));//
                    //             GUILayout.Label(ZSerializerSettings.Instance.defaultOnDictionary.valueList[i].ToString());
                    //         }    
                    //     }
                    //     break;
                }
            }
        }

        static void ExecuteOnAllSerializablesFromPrefabs(Action<IZSerializable> action, string title)
        {
            var prefabs = AssetDatabase.GetAllAssetPaths().Where(s => s.EndsWith(".prefab")).ToList();
            var originalScenePath = SceneManager.GetActiveScene().path;

            foreach (var prefab in prefabs)
            {
                try
                {
#if UNITY_2021_2_OR_NEWER
                    foreach (var zs in PrefabStageUtility.OpenPrefab(prefab).prefabContentsRoot
                                 .GetComponentsInChildren<IZSerializable>())
                    {
                        EditorUtility.DisplayProgressBar(title, $"{zs}",
                            prefabs.IndexOf(prefab) / (float)prefabs.Count);
                        action(zs);
                    }
#else
                    break;
#endif
                }
                catch (Exception)
                {
                    ZSerialize.LogError(
                        $"An exception was thrown while trying to open a prefab. {prefab} hasn't performed operation",
                        DebugMode.Off);
                }
            }


            EditorUtility.ClearProgressBar();
            EditorSceneManager.OpenScene(originalScenePath);
        }

        static void ExecuteOnAllSerializablesFromScenes(Action<IZSerializable> action, string title)
        {
            var originalScenePath = SceneManager.GetActiveScene().path;
            var paths = AssetDatabase.GetAllAssetPaths().Where(s => s.StartsWith("Assets") && s.EndsWith(".unity"))
                .ToList();

            foreach (var path in paths)
            {
                try
                {
                    var split = path.Split('/').Last();

                    AssetDatabase.OpenAsset(AssetDatabase.LoadAssetAtPath<SceneAsset>(path));
                    foreach (var zs in Object.FindObjectsOfType<MonoBehaviour>().Where(m => m is IZSerializable))
                    {
                        EditorUtility.DisplayProgressBar(title, $"{zs}, {split}",
                            paths.IndexOf(path) / (float)paths.Count);
                        action(zs as IZSerializable);
                    }

                    EditorSceneManager.SaveOpenScenes();
                }
                catch (Exception)
                {
                    ZSerialize.LogError(
                        $"An exception was thrown while trying to open a scene. {path} hasn't performed the operation",
                        DebugMode.Off);
                }
            }


            EditorUtility.ClearProgressBar();
            EditorSceneManager.OpenScene(originalScenePath);
        }

        [MenuItem("Tools/ZSerializer/Setup Project ZSerializers", priority = 21)]
        public static void SetupUnityZSerializers()
        {
            ZSerializerSettings.Instance.unityComponentTypes.Clear();

            ExecuteOnAllSerializablesFromScenes(serializable =>
            {
                if (serializable is PersistentGameObject pg)
                    PersistentGameObject.UpdateGlobalZSerializerList(pg, false);
            }, "Setting up Project ZSerializers");

            ExecuteOnAllSerializablesFromPrefabs(serializable =>
            {
                if (serializable is PersistentGameObject pg)
                    PersistentGameObject.UpdateGlobalZSerializerList(pg, false);
            }, "Setting up Project ZSerializers");

            ZSerializerEditorRuntime.GenerateUnityComponentClasses();
        }


        [MenuItem("Tools/ZSerializer/Reset Project ZUIDs", priority = 21)]
        public static void RefreshZUIDs()
        {
            var updateNonEmptyZUIDs = EditorUtility.DisplayDialog("Reset ZUIDs",
                "Would you like to reset your current ZUIDs? This will cause player save files to be unusable", "Yes",
                "No");

            int numberOfUpdatedZUIDs = 0;
            ExecuteOnAllSerializablesFromScenes(serializable =>
            {
                if (string.IsNullOrEmpty(serializable.ZUID) || updateNonEmptyZUIDs)
                {
                    if (serializable is PersistentGameObject pg) pg.Reset();
                    else serializable.GenerateEditorZUIDs(false);

                    numberOfUpdatedZUIDs++;
                }
            }, "Refreshing Project ZUIDs");

            ExecuteOnAllSerializablesFromPrefabs(serializable =>
            {
                if (serializable is PersistentGameObject pg) pg.Reset();
                else serializable.GenerateEditorZUIDs(false);

                numberOfUpdatedZUIDs++;
            }, "Refreshing Project ZUIDs");

            Debug.Log($"<color=cyan>{numberOfUpdatedZUIDs} ZUIDs have been reset!</color>");
        }


        [DidReloadScripts]
        static void OnReload()
        {
            EditorApplication.hierarchyChanged += OnHierarchyChanged;

            foreach (var monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>().Where(m => m is IZSerializable))
            {
                var serialize = monoBehaviour as IZSerializable;
                if (string.IsNullOrEmpty(serialize.ZUID) || string.IsNullOrEmpty(serialize.GOZUID))
                {
                    serialize.GenerateEditorZUIDs(true);
                    EditorUtility.SetDirty(monoBehaviour);
                    PrefabUtility.RecordPrefabInstancePropertyModifications(monoBehaviour);
                }

                if (string.IsNullOrEmpty(ZSerializerSettings.Instance.saveGroups[serialize.GroupID]))
                    Debug.LogError(
                        $"{monoBehaviour}'s Save Group is empty and thus will not get Serialized, change the Save Group of {monoBehaviour} or reimplement Save Group number {serialize.GroupID + 1}");
            }
        }

        private static void OnHierarchyChanged()
        {
            if (!Application.isPlaying)
            {
                Dictionary<string, Object> map = new Dictionary<string, Object>();

                foreach (var monoBehaviour in Object.FindObjectsOfType<MonoBehaviour>().Where(m => m is IZSerializable)
                             .Reverse())
                {
                    var serializable = monoBehaviour as IZSerializable;
                    if (!string.IsNullOrEmpty(serializable.ZUID))
                    {
                        if (map.TryGetValue(serializable.ZUID, out _))
                        {
                            serializable.GenerateEditorZUIDs(map.TryGetValue(serializable.GOZUID, out var go) &&
                                                             go != monoBehaviour.gameObject);

                            // if (serializable is PersistentGameObject pg)
                            // {
                            //     foreach (var pgSerializedComponent in pg.serializedComponents)
                            //     {
                            //         ZSerialize.idMap[ZSerialize.currentGroupID].TryAdd(pgSerializedComponent.zuid,
                            //             pgSerializedComponent.component);
                            //     }
                            // }
                        }

                        if (serializable as Object)
                            map[serializable.ZUID] = serializable as Object;
                        if (monoBehaviour && monoBehaviour.gameObject)
                            map[serializable.GOZUID] = monoBehaviour.gameObject;
                    }
                    else
                    {
                        serializable.GenerateEditorZUIDs(false);
                    }
                }
            }
        }

        public static void AddSampleScenesToBuildSettings()
        {
            var bedroom = "Assets/ZSerializer/Samples/2 - Scene Groups/House/Scenes/Bedroom.unity";
            var kitchen = "Assets/ZSerializer/Samples/2 - Scene Groups/House/Scenes/Kitchen.unity";

            var scenes = EditorBuildSettings.scenes.ToList();
            scenes.Add(new EditorBuildSettingsScene(bedroom, true));
            scenes.Add(new EditorBuildSettingsScene(kitchen, true));

            EditorBuildSettings.scenes = scenes.ToArray();
        }
    }
}