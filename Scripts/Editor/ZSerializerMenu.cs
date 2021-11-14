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

    public class ZSerializerMenu : EditorWindow
    {
        private const int fontSize = 20;
        private const int classHeight = 32;
        private bool editMode;
        private bool initiated;
        private int selectedMenu;
        private int selectedType;
        private int selectedGroup;
        private int selectedGroupIndex = -1;
        private static ZSerializerStyler styler;
        private int selectedTypeToShowSettings = -1;

        private static Class[] classes;

        [MenuItem("Tools/ZSerializer/ZSerializer Menu")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSerializerMenu>();
            window.titleContent = new GUIContent("ZSerializer");
            window.Show();
            EditorSceneManager.activeSceneChangedInEditMode += (a, b) => Init();
            SceneManager.activeSceneChanged += (a, b) => Init();
            SceneManager.sceneLoaded += (a, b) => Init();
            Init();
        }

        static void GetClasses()
        {
            var types = ZSerialize.GetPersistentTypes().Where(t => FindObjectOfType(t) != null).ToArray();
            classes = types.Select(t => new Class(t, ZSerializerEditor.GetClassState(t))).ToArray();
        }

        static void GenerateStyler()
        {
            styler = new ZSerializerStyler();
            styler.GetEveryResource();
        }

        [DidReloadScripts]
        private static void Init()
        {
            if (ZSerializerSettings.Instance && ZSerializerSettings.Instance.packageInitialized && HasOpenInstances<ZSerializerMenu>())
            {
                GetWindow<ZSerializerMenu>().minSize = new Vector2(480, 400);
                GetWindow<ZSerializerMenu>().maxSize = new Vector2(480, 1200);

                GenerateStyler();

                GetClasses();
            }
        }

        private bool stylerInitialized;
        private Vector2 scrollPos;

        private void OnGUI()
        {
            if (!ZSerializerSettings.Instance.packageInitialized)
            {
                if (!stylerInitialized)
                {
                    ZSerializerMenu w;

                    GenerateStyler();

                    w = GetWindow<ZSerializerMenu>();
                    w.position = new Rect(w.position) { height = 104 };
                }

                stylerInitialized = true;

                if (GUILayout.Button($"<color=#{ZSerializerStyler.MainHex}>Setup</color>", new GUIStyle("button") { fontSize = 48, font = styler.header.font, richText = true},
                    GUILayout.MinHeight(100)))
                {
                    ZSerializerSettings.Instance.packageInitialized = true;
                    ZSerializerEditor.GenerateUnityComponentClasses();
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

                        if (styler == null) GenerateStyler();
                        
                        editMode = GUILayout.Toggle(editMode, styler.cogWheel, new GUIStyle("button"),
                            GUILayout.Height(28), GUILayout.Width(28));
                    }

                    using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
                    {
                        scrollPos = scrollView.scrollPosition;

                        if (editMode)
                        {
                            ZSerializerEditor.BuildSettingsEditor(styler, ref selectedMenu, ref selectedType, ref selectedGroup,ref selectedGroupIndex,
                                position.width);
                        }
                        else
                        {
                            if (classes == null) GetClasses();
                            
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
                                        GUILayout.Height(32),GUILayout.MaxWidth(464)))
                                    {
                                        string color = classInstance.state == ClassState.Valid
                                            ? ZSerializerSettings.Instance
                                                .componentDataDictionary[classInstance.classType].isOn
                                                ? ZSerializerStyler.MainHex
                                                : ZSerializerStyler.OffHex
                                            : classInstance.state == ClassState.NeedsRebuilding
                                                ? ZSerializerStyler.YellowHex
                                                : ZSerializerStyler.RedHex;


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
                                    GUILayout.Height(32),GUILayout.MaxWidth(464)))
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