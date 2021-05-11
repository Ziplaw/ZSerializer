using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ZSave.Editor
{
    public class ZSaverTypesEditorWindow : EditorWindow
    {
        private const int fontSize = 20;
        private const int classHeight = 32;
        
        struct Class
        {
            public Class(Type classType, bool hasZSaver)
            {
                this.classType = classType;
                this.hasZSaver = hasZSaver;
            }

            public Type classType;
            public bool hasZSaver;
        }
        
        private Class[] classes;
        private Texture2D refreshImage;
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
                classes[i] = new Class(types[i],types[i].Assembly.GetType(types[i].Name + "ZSaver") != null);
            }
            refreshImage = Resources.Load<Texture2D>("not_made");
            validImage = Resources.Load<Texture2D>("valid");
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
                    foreach (var classInstance in classes)
                    {
                        using (new EditorGUILayout.HorizontalScope("helpbox"))
                        {
                            EditorGUILayout.LabelField(classInstance.classType.Name, new GUIStyle("label"){alignment = TextAnchor.MiddleCenter, fontSize = fontSize},GUILayout.Height(classHeight));
                            using (new EditorGUI.DisabledGroupScope(classInstance.hasZSaver))
                                if (GUILayout.Button(classInstance.hasZSaver ? validImage : refreshImage,
                                    GUILayout.Width(classHeight), GUILayout.Height(classHeight)))
                                {
                                    string path = EditorUtility.SaveFilePanel(
                                        classInstance.classType.Name + "ZSaver.cs Save Location", "Assets",
                                        classInstance.classType.Name + "ZSaver","cs");
                                    
                                    PersistanceManager.CreateZSaver(classInstance.classType, path);
                                }
                        }
                    }
            }
        }

        
    }
}