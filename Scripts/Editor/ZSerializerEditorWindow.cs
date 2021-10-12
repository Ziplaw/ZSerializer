using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public class ZSerializerEditorWindow : EditorWindow
    {
        private const int fontSize = 20;
        private const int classHeight = 32;
        private bool editMode;
        private bool initiated;
        private int selectedMenu;
        private int selectedType;
        private static ZSerializerStyler styler;
        private int selectedTypeToShowSettings = -1;

        private static Class[] classes;

        [MenuItem("Tools/ZSerializer/ZSerializer Menu")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSerializerEditorWindow>();
            window.titleContent = new GUIContent("ZSerializer");
            window.Show();
            EditorSceneManager.activeSceneChangedInEditMode += (a, b) => Init();
            SceneManager.activeSceneChanged += (a, b) => Init();
            SceneManager.sceneLoaded += (a, b) => Init();
            Init();
        }

        [DidReloadScripts]
        private static void Init()
        {
            if (ZSerializerSettings.Instance && ZSerializerSettings.Instance.packageInitialized && HasOpenInstances<ZSerializerEditorWindow>())//
            {
                GetWindow<ZSerializerEditorWindow>().minSize = new Vector2(480, 400);

                styler = new ZSerializerStyler();

                var types = ZSerialize.GetPersistentTypes().Where(t => FindObjectOfType(t) != null).ToArray();

                classes = new Class[types.Length];

                for (int i = 0; i < types.Length; i++)
                {
                    classes[i] = new Class(types[i], ZSerializerEditor.GetClassState(types[i]));
                }

                styler.GetEveryResource();
            }
        }

        private bool stylerInitialized;
        private Vector2 scrollPos;

        private void OnGUI()
        {
            if (!ZSerializerSettings.Instance.packageInitialized) //
            {
                if (!stylerInitialized)
                {
                    ZSerializerEditorWindow w;

                    styler = new ZSerializerStyler();
                    styler.GetEveryResource();

                    w = GetWindow<ZSerializerEditorWindow>();
                    w.position = new Rect(w.position) { height = 104 };
                }

                stylerInitialized = true;

                if (GUILayout.Button("Setup", new GUIStyle("button") { fontSize = 48, font = styler.header.font },
                    GUILayout.MinHeight(100)))
                {
                    ZSerializerSettings.Instance.packageInitialized = true;
                    ZSerializerEditor.GenerateUnityComponentClasses(); //
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
                            ZSerializerEditor.BuildSettingsEditor(styler, ref selectedMenu, ref selectedType,
                                position.width);
                        }
                        else if (classes != null)
                        {
                            if (classes.Length == 0)
                            {
                                GUILayout.Label(
                                    "You have no present persistent components in the current Scene\nMake one of your components inherit from PersistentMonoBehaviour for it to become persistent!",
                                    new GUIStyle("label")
                                    {
                                        alignment = TextAnchor.MiddleCenter,
                                        wordWrap = true
                                    });
                            }
                            else
                                for (var i = 0; i < classes.Length; i++)
                                {
                                    var classInstance = classes[i];
                                    GUILayout.Space(-15);
                                    using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.window,
                                        GUILayout.Height(32)))
                                    {
                                        string color = classInstance.state == ClassState.Valid
                                            ? ZSerializerSettings.Instance
                                                .componentDataDictionary[classInstance.classType].isOn
                                                ? "29CF42"
                                                : "999999"
                                            : classInstance.state == ClassState.NeedsRebuilding
                                                ? "FFC107"
                                                : "FF625A";


                                        if (GUILayout.Button(
                                            $"<color=#{color}>{classInstance.classType.Name}</color>",
                                            new GUIStyle(styler.header)
                                                { alignment = TextAnchor.MiddleCenter, fontSize = fontSize },
                                            GUILayout.Height(classHeight)))
                                        {
                                            var pathList = Directory.GetFiles("Assets",
                                                $"*{classInstance.classType.Name}*.cs",
                                                SearchOption.AllDirectories)[0];

                                            var path = pathList.Replace('\\', '/');


                                            EditorUtility.FocusProjectWindow();

                                            UnityEngine.Object obj =
                                                AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(path);

                                            Selection.activeObject = obj;

                                            EditorGUIUtility.PingObject(obj);
                                        }

                                        ZSerializerEditor.BuildWindowValidityButton(classInstance.classType, styler);

                                        if (GUILayout.Button(styler.cogWheel,
                                            GUILayout.MaxHeight(32), GUILayout.MaxWidth(32)))
                                        {
                                            selectedTypeToShowSettings = selectedTypeToShowSettings == i ? -1 : i;
                                        }
                                    }

                                    using (new GUILayout.HorizontalScope())
                                    {
                                        if (selectedTypeToShowSettings == i)
                                        {
                                            GUILayout.Label("Save Group", GUILayout.MaxWidth(80));
                                            int newValue = EditorGUILayout.Popup(
                                                ZSerializerSettings.Instance
                                                    .componentDataDictionary[classInstance.classType].groupID,
                                                ZSerializerSettings.Instance.saveGroups
                                                    .Where(s => !string.IsNullOrEmpty(s)).ToArray());
                                            if (newValue != ZSerializerSettings.Instance
                                                .componentDataDictionary[classInstance.classType].groupID)
                                            {
                                                ZSerializerSettings.Instance
                                                        .componentDataDictionary[classInstance.classType].groupID =
                                                    newValue;
                                                foreach (var obj in FindObjectsOfType(classInstance.classType))
                                                {
                                                    var o = (PersistentMonoBehaviour)obj;
                                                    if (o.autoSync)
                                                    {
                                                        o.groupID = newValue;
                                                        EditorUtility.SetDirty(o);
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }

                            GUILayout.Space(5);
                            if (!Application.isPlaying)
                            {
                                GUILayout.Space(-15);
                                using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.window,
                                    GUILayout.Height(32)))
                                {
                                    EditorGUILayout.LabelField("ZSerialize All",
                                        new GUIStyle(styler.header)
                                            { alignment = TextAnchor.MiddleCenter, fontSize = fontSize },
                                        GUILayout.Height(classHeight));

                                    ZSerializerEditor.BuildButtonAll(classes, classHeight, styler);
                                }

                                GUILayout.Space(15);
                            }
                        }
                    }
                }
            }
        }
    }
}