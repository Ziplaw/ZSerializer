using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ZSave;
using ZSave.Editor;

public static class ZSaverEditor
{
    public static void CreateZSaver(Type type, string path)
    {
        FileStream fileStream = new FileStream(path, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fileStream);

        string script =
            "using ZSave;\n" +
            "\n" +
            "[System.Serializable]\n" +
            $"public class {type.Name}ZSaver : ZSaver<{type.Name}>\n" +
            "{\n";

        foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance)
            .Where(f => f.GetCustomAttribute(typeof(OmitSerializableCheck)) == null))
        {
            int genericParameterAmount = fieldInfo.FieldType.GenericTypeArguments.Length;

            script +=
                $"    public {fieldInfo.FieldType} {fieldInfo.Name};\n".Replace('+', '.').Replace('[', '<')
                    .Replace(']', '>').Replace($"`{genericParameterAmount}", "");
        }

        string className = type.Name + "Instance";

        script +=
            $"\n    public {type.Name}ZSaver({type.Name} {className}) : base({className}.gameObject, {className})\n" +
            "    {\n";

        foreach (var fieldInfo in type.GetFields(BindingFlags.Public | BindingFlags.Instance))
        {
            int genericParameterAmount = fieldInfo.FieldType.GenericTypeArguments.Length;

            script +=
                $"         {fieldInfo.Name} = ({fieldInfo.FieldType})typeof({type.Name}).GetField(\"{fieldInfo.Name}\").GetValue({className});\n".Replace('+', '.').Replace('[', '<')
                    .Replace(']', '>').Replace($"`{genericParameterAmount}", "");
        }

        Debug.Log("ZSaver script being created at " + path);

        script += "    }\n}";

        sw.Write(script);

        sw.Close();
    }

    public static void CreateEditorScript(Type type, string path)
    {
        string editorScript =
            @"using UnityEditor;
using ZSave.Editor;
using UnityEditor.Callbacks;

[CustomEditor(typeof(" + type.Name + @"))]
public class " + type.Name + @"Editor : Editor
{
    private " + type.Name + @" manager;
    private bool editMode;
    private static ZSaverStyler styler;

    private void OnEnable()
    {
        manager = target as " + type.Name + @";
        styler = new ZSaverStyler();
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        styler = new ZSaverStyler();
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode, styler);
        base.OnInspectorGUI();
    }
}";


        string newPath = new string((new string(path.Reverse().ToArray())).Substring(path.Split('/').Last().Length)
            .Reverse().ToArray());
        Debug.Log("Editor script being created at " + newPath + "Editor");
        string relativePath = "Assets" + newPath.Substring(Application.dataPath.Length);


        if (!AssetDatabase.IsValidFolder(relativePath + "Editor"))
        {
            Directory.CreateDirectory(newPath + "Editor");
        }


        string newNewPath = newPath + "Editor/" + type.Name + "Editor.cs";
        FileStream fileStream = new FileStream(newNewPath, FileMode.Create, FileAccess.Write);
        StreamWriter sw = new StreamWriter(fileStream);
        sw.Write(editorScript);
        sw.Close();

        AssetDatabase.Refresh();
    }


    public static ClassState GetClassState(Type type)
    {
        Type ZSaverType = type.Assembly.GetType(type.Name + "ZSaver");
        if (ZSaverType == null) return ClassState.NotMade;

        var fieldsZSaver = ZSaverType.GetFields()
            .Where(f => f.GetCustomAttribute(typeof(OmitSerializableCheck)) == null).ToArray();
        var fieldsType = type.GetFields();

        if (fieldsZSaver.Length == fieldsType.Length)
        {
            for (int j = 0; j < fieldsZSaver.Length; j++)
            {
                if (fieldsZSaver[j].Name != fieldsType[j].Name ||
                    fieldsZSaver[j].FieldType != fieldsType[j].FieldType)
                {
                    return ClassState.NeedsRebuilding;
                }
            }

            return ClassState.Valid;
        }

        return ClassState.NeedsRebuilding;
    }

    public static void BuildButton(Type type, int width, ZSaverStyler styler)
    {
        ClassState state = GetClassState(type);
        if (styler.validImage == null) styler.GetEveryResource();

        using (new EditorGUI.DisabledGroupScope(state == ClassState.Valid))
        {
            Texture2D textureToUse = styler.validImage;

            if (state != ClassState.Valid)
            {
                textureToUse = state == ClassState.NeedsRebuilding
                    ? styler.needsRebuildingImage
                    : styler.notMadeImage;
            }

            if (GUILayout.Button(textureToUse,
                GUILayout.MaxWidth(width), GUILayout.Height(width)))
            {
                string path;


                if (state == ClassState.NotMade)
                {
                    path = EditorUtility.SaveFilePanel(
                        type.Name + "ZSaver.cs Save Location", "Assets",
                        type.Name + "ZSaver", "cs");
                }
                else
                {
                    path = Directory.GetFiles("Assets", $"{type.Name}ZSaver.cs",
                        SearchOption.AllDirectories)[0];
                    path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
                           path.Replace('\\', '/');
                }

                CreateZSaver(type, path);
                if (state == ClassState.NotMade)
                    CreateEditorScript(type, path);
                AssetDatabase.Refresh();
            }
        }
    }

    public static void BuildButtonAll(Class[] classes, int width, ZSaverStyler styler)
    {
        Texture2D textureToUse = styler.refreshImage;

        if (GUILayout.Button(textureToUse,
            GUILayout.MaxWidth(width), GUILayout.Height(width)))
        {
            string path;

            string folderPath = EditorUtility.SaveFolderPanel("ZSaver.cs Save Locations", "Assets", "");

            foreach (var c in classes)
            {
                ClassState state = c.state;

                path = folderPath + $"/{c.classType.Name}ZSaver.cs";

                if (state != ClassState.NotMade)
                {
                    path = Directory.GetFiles("Assets", $"{c.classType.Name}ZSaver.cs",
                        SearchOption.AllDirectories)[0];
                    path = Application.dataPath.Substring(0, Application.dataPath.Length - 6) +
                           path.Replace('\\', '/');
                }

                CreateZSaver(c.classType, path);
                CreateEditorScript(c.classType, path);
                AssetDatabase.Refresh();
            }
        }
    }


    public static void BuildPersistentComponentEditor<T>(T manager, ref bool editMode, ZSaverStyler styler)
    {
        Texture2D cogwheel = Resources.Load<Texture2D>("cog");

        using (new GUILayout.HorizontalScope("helpbox"))
        {
            GUILayout.Label("Persistent " + manager.GetType().GetCustomAttribute<PersistentAttribute>().saveType,
                styler.header, GUILayout.Height(28));
            using (new EditorGUI.DisabledScope(GetClassState(manager.GetType()) != ClassState.Valid))
                editMode = GUILayout.Toggle(editMode, cogwheel, new GUIStyle("button"), GUILayout.MaxWidth(28),
                    GUILayout.Height(28));

            BuildButton(manager.GetType(), 28, styler);
        }
    }

    public static void BuildEditModeEditor<T>(SerializedObject serializedObject, T manager, bool editMode,
        ref bool[] persistentFields, Action OnInspectorGUI)
    {
        if (editMode)
        {
            GUILayout.Label("Select fields to serialize",
                new GUIStyle("helpbox") {alignment = TextAnchor.MiddleCenter}, GUILayout.Height(18));
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

    public static void BuildSettingsEditor(ZSaverStyler styler)
    {
        FieldInfo[] fieldInfos = typeof(ZSaverSettings).GetFields(BindingFlags.Instance | BindingFlags.Public);
        SerializedObject serializedObject = new SerializedObject(styler.settings);

        using (new GUILayout.VerticalScope("helpbox"))
        {
            foreach (var fieldInfo in fieldInfos)
            {
                serializedObject.Update();
                EditorGUILayout.PropertyField(serializedObject.FindProperty(fieldInfo.Name));
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}