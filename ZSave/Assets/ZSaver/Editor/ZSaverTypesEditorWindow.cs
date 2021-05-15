using System;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ZSave.Editor
{
    public enum ClassState
    {
        NotMade,
        NeedsRebuilding,
        Valid
    }

    struct Class
    {
        public Class(Type classType, ClassState state)
        {
            this.classType = classType;
            this.state = state;
        }

        public ClassState state;
        public Type classType;
    }

    public class ZSaverTypesEditorWindow : EditorWindow
    {
        private const int fontSize = 20;
        private const int classHeight = 32;
        private bool editMode;
        private bool initiated;

        private static Class[] classes;

        [MenuItem("Tools/ZSave/Persistent Classes Configurator")]
        private static void ShowWindow()
        {
            var window = GetWindow<ZSaverTypesEditorWindow>();
            window.titleContent = new GUIContent("Persistent Classes");
            window.Show();
            Init();
        }
        private static void Init()
        {
            var types = PersistentAttribute.GetTypesWithPersistentAttribute(AppDomain.CurrentDomain
                .GetAssemblies()).ToArray();

            classes = new Class[types.Length];

            for (int i = 0; i < types.Length; i++)
            {
                classes[i] = new Class(types[i], ZSaverEditor.GetClassState(types[i]));
            }

            ZSaverEditor.GetImages();
        }

        private void OnGUI()
        {
            using (new EditorGUILayout.HorizontalScope())
            {
                if (GUILayout.Button("Refresh", GUILayout.MaxHeight(28)))
                {
                    Init();
                }

                editMode = GUILayout.Toggle(editMode, ZSaverEditor.Cogwheel, new GUIStyle("button"),
                    GUILayout.MaxHeight(28), GUILayout.MaxWidth(28));
            }

            if (editMode)
            {
                ZSaverEditor.BuildSettingsEditor();
            }


            if (classes != null && !editMode)
            {
                foreach (var classInstance in classes)
                {
                    using (new EditorGUILayout.HorizontalScope("helpbox"))
                    {
                        EditorGUILayout.LabelField(classInstance.classType.Name,
                            new GUIStyle("label") {alignment = TextAnchor.MiddleCenter, fontSize = fontSize},
                            GUILayout.Height(classHeight));

                        ZSaverEditor.BuildButton(classInstance.classType, classHeight);
                    }
                }
            }
        }
    }

    public static class ZSaverEditor
    {
        private static Texture2D notMadeImage;
        private static Texture2D needsRebuildingImage;
        private static Texture2D validImage;
        private static Texture2D cogWheel;
        private static ZSaverSettings settings;

        public static Texture2D Cogwheel
        {
            get
            {
                if (cogWheel == null) GetImages();
                return cogWheel;
            }
        }

        public static ZSaverSettings Settings => settings ? settings : Resources.Load<ZSaverSettings>("ZSaverSettings");

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

        public static void BuildButton(Type type, int width)
        {
            ClassState state = GetClassState(type);
            if (validImage == null) GetImages();

            using (new EditorGUI.DisabledGroupScope(state == ClassState.Valid))
            {
                Texture2D textureToUse = validImage;

                if (state != ClassState.Valid)
                {
                    textureToUse = state == ClassState.NeedsRebuilding
                        ? needsRebuildingImage
                        : notMadeImage;
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

                    PersistanceManager.CreateZSaver(type, path);
                    if (state == ClassState.NotMade)
                        PersistanceManager.CreateEditorScript(type, path);
                    AssetDatabase.Refresh();
                }
            }
        }

        [DidReloadScripts]
        public static void GetImages()
        {
            notMadeImage = Resources.Load<Texture2D>("not_made");
            validImage = Resources.Load<Texture2D>("valid");
            needsRebuildingImage = Resources.Load<Texture2D>("needs_rebuilding");
            cogWheel = Resources.Load<Texture2D>("cog");
        }

        public static void BuildPersistentComponentEditor<T>(T manager, ref bool editMode)
        {
            Texture2D cogwheel = Resources.Load<Texture2D>("cog");

            using (new GUILayout.HorizontalScope())
            {
                GUILayout.Label("Persistent " + manager.GetType().GetCustomAttribute<PersistentAttribute>().saveType,
                    new GUIStyle("helpbox")
                    {
                        alignment = TextAnchor.MiddleCenter,
                        fontSize = 14
                    }, GUILayout.Height(28));
                using (new EditorGUI.DisabledScope(GetClassState(manager.GetType()) != ClassState.Valid))
                    editMode = GUILayout.Toggle(editMode, cogwheel, new GUIStyle("button"), GUILayout.MaxWidth(28),
                        GUILayout.Height(28));
                BuildButton(manager.GetType(), 28);
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

        public static void BuildSettingsEditor()
        {
            FieldInfo[] fieldInfos = typeof(ZSaverSettings).GetFields(BindingFlags.Instance | BindingFlags.Public);
            SerializedObject serializedObject = new SerializedObject(Settings);

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
}