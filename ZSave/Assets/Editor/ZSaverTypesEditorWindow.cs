using System;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace ZSave.Editor
{
    public class ZSaverTypesEditorWindow : EditorWindow
    {
        private const int fontSize = 20;
        private const int classHeight = 32;

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

        private Class[] classes;
        private Texture2D notMadeImage;
        private Texture2D needsRebuildingImage;
        private Texture2D validImage;

        [MenuItem("Tools/ZSave/Persistent Classes Configurator")]
        private static void ShowWindow()
        {
            var window = GetWindow<ZSaverTypesEditorWindow>();
            window.titleContent = new GUIContent("Persistent Classes");
            window.Show();
        }

        private void Init()
        {
            var types = PersistentAttribute.GetTypesWithPersistentAttribute(AppDomain.CurrentDomain
                .GetAssemblies()).ToArray();

            classes = new Class[types.Length];

            for (int i = 0; i < types.Length; i++)
            {
                ClassState classState = ClassState.Valid;
                Type ZSaverType = types[i].Assembly.GetType(types[i].Name + "ZSaver");
                if (ZSaverType == null) classState = ClassState.NotMade;
                else
                {
                    var fieldsZSaver = ZSaverType.GetFields()
                        .Where(f => f.GetCustomAttribute(typeof(NonPersistent)) == null).ToArray();
                    var fieldsType = types[i].GetFields();

                    if (fieldsZSaver.Length != fieldsType.Length) classState = ClassState.NeedsRebuilding;
                }

                classes[i] = new Class(types[i], classState);
            }

            notMadeImage = Resources.Load<Texture2D>("not_made");
            validImage = Resources.Load<Texture2D>("valid");
            needsRebuildingImage = Resources.Load<Texture2D>("needs_rebuilding");
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Refresh"))
            {
                Init();
            }
            else
            {
                if (classes != null)
                {
                    foreach (var classInstance in classes)
                    {
                        using (new EditorGUILayout.HorizontalScope("helpbox"))
                        {
                            EditorGUILayout.LabelField(classInstance.classType.Name,
                                new GUIStyle("label") {alignment = TextAnchor.MiddleCenter, fontSize = fontSize},
                                GUILayout.Height(classHeight));
                            using (new EditorGUI.DisabledGroupScope(classInstance.state == ClassState.Valid))
                            {
                                Texture2D textureToUse = validImage;

                                if (classInstance.state != ClassState.Valid)
                                {
                                    textureToUse = classInstance.state == ClassState.NeedsRebuilding
                                        ? needsRebuildingImage
                                        : notMadeImage;
                                }

                                if (GUILayout.Button(textureToUse,
                                    GUILayout.Width(classHeight), GUILayout.Height(classHeight)))
                                {
                                    string path;

                                    if (classInstance.state == ClassState.NotMade)
                                    {
                                        path = EditorUtility.SaveFilePanel(
                                            classInstance.classType.Name + "ZSaver.cs Save Location", "Assets",
                                            classInstance.classType.Name + "ZSaver", "cs");
                                    }
                                    else
                                    {
                                        path = Directory.GetFiles("Assets", $"*{classInstance.classType.Name}ZSaver.cs",
                                            SearchOption.AllDirectories)[0];
                                    }

                                    PersistanceManager.CreateZSaver(classInstance.classType, path);
                                    AssetDatabase.Refresh();
                                }
                            }
                        }
                    }
                }
            }
        }
    }
}