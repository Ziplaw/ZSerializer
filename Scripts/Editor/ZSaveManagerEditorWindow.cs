using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ZSaver.Editor
{
    public enum ClassState
    {
        NotMade,
        NeedsRebuilding,
        Valid
    }

    public struct Class
    {
        public Class(Type classType, ClassState state)
        {
            this.classType = classType;
            this.state = state;
        }

        public ClassState state;
        public Type classType;
    }

    public class ZSaveManagerEditorWindow : EditorWindow
    {
        private const int fontSize = 20;
        private const int classHeight = 32;
        private bool editMode;
        private bool initiated;
        private static ZSaverStyler styler;

        private static Class[] classes;

        [MenuItem("Tools/ZSave/ZSaver Menu")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSaveManagerEditorWindow>();
            window.titleContent = new GUIContent("ZSaver");
            window.Show();
            Init();
        }

        [DidReloadScripts]
        private static void Init()
        {
            if (ZSaverSettings.Instance && ZSaverSettings.Instance.packageInitialized)
            {

                styler = new ZSaverStyler();

                var types = ZSave.GetTypesWithPersistentAttribute().ToArray();

                classes = new Class[types.Length];

                for (int i = 0; i < types.Length; i++)
                {
                    classes[i] = new Class(types[i], ZSaverEditor.GetClassState(types[i]));
                }

                styler.GetEveryResource();
            }
        }

        private bool stylerInitialized;

        private void OnGUI()
        {
            if (!ZSaverSettings.Instance.packageInitialized)//
            {
                ZSaveManagerEditorWindow w;
                
                styler = new ZSaverStyler();
                styler.GetEveryResource();

                w = GetWindow<ZSaveManagerEditorWindow>();
                w.position = new Rect(w.position){height = 104};

                if (GUILayout.Button("Setup", new GUIStyle("button"){fontSize = 48, font = styler.header.font}, GUILayout.MinHeight(100)))
                {
                    ZSaverSettings.Instance.packageInitialized = true;
                    ZSaverEditor.GenerateUnityComponentClasses();//
                    Init();
                    w.position = new Rect(w.position){height = 300};
                }
            }
            else
            {

                using (new EditorGUILayout.HorizontalScope())
                {
                    if (GUILayout.Button("Refresh", GUILayout.MaxHeight(28)))
                    {
                        Init();
                    }

                    editMode = GUILayout.Toggle(editMode, styler.cogWheel, new GUIStyle("button"),
                        GUILayout.MaxHeight(28), GUILayout.MaxWidth(28));
                }

                if (editMode)
                {
                    ZSaverEditor.BuildSettingsEditor(styler);
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

                            ZSaverEditor.BuildButton(classInstance.classType, classHeight, styler);
                        }
                    }

                    GUILayout.Space(5);

                    using (new EditorGUILayout.HorizontalScope("helpbox"))
                    {
                        EditorGUILayout.LabelField("Save All",
                            new GUIStyle("label") {alignment = TextAnchor.MiddleCenter, fontSize = fontSize},
                            GUILayout.Height(classHeight));

                        ZSaverEditor.BuildButtonAll(classes, classHeight, styler);
                    }
                }
            }
        }
    }
}