using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;

namespace ZSerializer.Editor
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
        private int selectedMenu;
        private int selectedType;
        private static ZSaverStyler styler;

        private static Class[] classes;

        [MenuItem("Tools/ZSave/ZSerializer Menu")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSaveManagerEditorWindow>();
            window.titleContent = new GUIContent("ZSerializer");
            window.Show();
            Init();
        }

        [DidReloadScripts]
        private static void Init()
        {
            if (ZSaverSettings.Instance && ZSaverSettings.Instance.packageInitialized)
            {
                GetWindow<ZSaveManagerEditorWindow>().minSize = new Vector2(480,400);

                styler = new ZSaverStyler();

                var types = ZSave.GetPersistentTypes().ToArray();

                classes = new Class[types.Length];

                for (int i = 0; i < types.Length; i++)
                {
                    classes[i] = new Class(types[i], ZSaverEditor.GetClassState(types[i]));
                }

                styler.GetEveryResource();
            }
        }

        private bool stylerInitialized;
        private Vector2 scrollPos;

        private void OnGUI()
        {
            if (!ZSaverSettings.Instance.packageInitialized)//
            {
                if (!stylerInitialized)
                {
                    ZSaveManagerEditorWindow w;

                    styler = new ZSaverStyler();
                    styler.GetEveryResource();

                    w = GetWindow<ZSaveManagerEditorWindow>();
                    w.position = new Rect(w.position) {height = 104};
                }

                stylerInitialized = true;

                if (GUILayout.Button("Setup", new GUIStyle("button"){fontSize = 48, font = styler.header.font}, GUILayout.MinHeight(100)))
                {
                    ZSaverSettings.Instance.packageInitialized = true;
                    ZSaverEditor.GenerateUnityComponentClasses();//
                    Init();
                }
            }
            else
            {

                using (new GUILayout.VerticalScope("box"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        if (GUILayout.Button("Refresh", GUILayout.Height(28)))
                        {
                            Init();
                        }

                        editMode = GUILayout.Toggle(editMode, styler.cogWheel, new GUIStyle("button"),
                            GUILayout.Height(28), GUILayout.Width(28));
                    }

                    using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
                    {
                        scrollPos = scrollView.scrollPosition;

                        if (editMode)
                        {
                            ZSaverEditor.BuildSettingsEditor(styler, ref selectedMenu, ref selectedType,
                                position.width);
                        }
                        else if (classes != null)
                        {
                            foreach (var classInstance in classes)
                            {
                                GUILayout.Space(-15);
                                using (new EditorGUILayout.HorizontalScope(ZSaverStyler.window, GUILayout.Height(32)))
                                {
                                    string color = classInstance.state == ClassState.Valid ? "29CF42" :
                                        classInstance.state == ClassState.NeedsRebuilding ? "FFC107" : "FF625A";
                                    EditorGUILayout.LabelField(
                                        $"<color=#{color}>{classInstance.classType.Name}</color>",
                                        new GUIStyle(styler.header)
                                            {alignment = TextAnchor.MiddleCenter, fontSize = fontSize},
                                        GUILayout.Height(classHeight));

                                    ZSaverEditor.BuildButton(classInstance.classType, classHeight, styler);
                                }
                            }

                            GUILayout.Space(5);

                            GUILayout.Space(-15);
                            using (new EditorGUILayout.HorizontalScope(ZSaverStyler.window, GUILayout.Height(32)))
                            {
                                EditorGUILayout.LabelField("ZSerialize All",
                                    new GUIStyle(styler.header)
                                        {alignment = TextAnchor.MiddleCenter, fontSize = fontSize},
                                    GUILayout.Height(classHeight));

                                ZSaverEditor.BuildButtonAll(classes, classHeight, styler);
                            }

                            GUILayout.Space(15);

                        }

                    }
                }
            }
        }
    }
}