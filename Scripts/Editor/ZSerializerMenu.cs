using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        private int selectedComponentSettings;
        private string searchText;
        static bool searchProject;

        private static ZSerializerStyler styler;

        private static ZSerializerStyler Styler
        {
            get
            {
                if (styler == null)
                {
                    styler = new ZSerializerStyler();
                    styler.GetEveryResource();
                }
                return styler;
            }
        }

        private int selectedTypeToShowSettings = -1;

        static ZSerializerMenu window;
        private static Class[] classes;


        [MenuItem("Tools/ZSerializer/ZSerializer Menu", priority = 0)]
        internal static void ShowWindow()
        {
            window = GetWindow<ZSerializerMenu>();
            window.Show();
            window.titleContent = new GUIContent("ZSerializer Menu");
            Init();
        }

        void SceneUpdate(Scene prevScene, Scene newScene) => Init();
        void SceneUpdate(Scene prevScene, LoadSceneMode newScene) => Init();

        private void OnEnable()
        {
            EditorSceneManager.activeSceneChangedInEditMode += SceneUpdate;
            SceneManager.activeSceneChanged += SceneUpdate;
            SceneManager.sceneLoaded += SceneUpdate;
        }

        private void OnDisable()
        {
            EditorSceneManager.activeSceneChangedInEditMode -= SceneUpdate;
            SceneManager.activeSceneChanged -= SceneUpdate;
            SceneManager.sceneLoaded -= SceneUpdate;
        }

        static void GetClasses()
        {
#if UNITY_2020_3_OR_NEWER
                        var types =
 ZSerialize.GetPersistentTypes().Where(t => searchProject || FindObjectsOfType(t,true).Length > 0).OrderBy(t => t.Name).ToArray();
#else
            var types = ZSerialize.GetPersistentTypes().Where(t => searchProject || FindObjectsOfType(t).Length > 0)
                .OrderBy(t => t.Name).ToArray();
#endif
            classes = types.Select(t => new Class(t, ZSerializerEditor.GetClassState(t))).ToArray();
        }


        [DidReloadScripts]
        private static void Init()
        {
            if (ZSerializerSettings.Instance && ZSerializerSettings.Instance.packageInitialized &&
                HasOpenInstances<ZSerializerMenu>())
            {
                GetClasses();
            }
        }

        private bool stylerInitialized;
        private Vector2 scrollPos;

        private void OnGUI()
        {
            if (!ZSerializerSettings.Instance.packageInitialized)
            {
                GUILayout.Space(-15);
                using (new GUILayout.VerticalScope(ZSerializerStyler.Window,
                    GUILayout.MaxHeight(1)))
                {
                    GUILayout.Label($"<color=#{ZSerializerStyler.MainHex}>ZSerializer Setup Wizard</color>",
                        new GUIStyle("label")
                        {
                            alignment = TextAnchor.MiddleCenter, fontSize = 32, font = Styler.header.font,
                            richText = true
                        },
                        GUILayout.MaxHeight(100));
                }

                GUILayout.Space(-15);
                using (new GUILayout.VerticalScope(ZSerializerStyler.Window,
                    GUILayout.MaxHeight(1)))
                {
                    Dictionary<bool, List<Texture2D>> icons = new Dictionary<bool, List<Texture2D>>
                    {
                        {
                            true,
                            new List<Texture2D>
                                { Resources.Load<Texture2D>("valid"), Resources.Load<Texture2D>("valid") }
                        }, //repeated to not cause index out of range
                        {
                            false,
                            new List<Texture2D>
                                { Resources.Load<Texture2D>("not_made"), Resources.Load<Texture2D>("needs_rebuilding") }
                        }
                    };

                    // bool hasZSerializers = File.Exists(Path.Combine(Application.dataPath,
                    //     "ZResources/ZSerializer/UnityComponentZSerializers.cs"));
                    bool zuidsAreSetup = !FindObjectsOfType<MonoBehaviour>().Where(m => m is IZSerializable).Any(m =>
                        string.IsNullOrEmpty((m as IZSerializable).ZUID) ||
                        string.IsNullOrEmpty((m as IZSerializable).ZUID));
                    bool areSampleScenesIn = EditorBuildSettings.scenes.Any(s =>
                        s.path.Contains("ZSerializer/Samples/2 - Scene Groups/House/Scenes"));


                    // using (new GUILayout.HorizontalScope())
                    // {
                    //     GUILayout.Label(new GUIContent(
                    //         icons[hasZSerializers][0]),GUILayout.Width(20), GUILayout.Height(20));
                    //     GUILayout.Label("Unity Component ZSerializers Built", GUILayout.Width(200));
                    //
                    //     using (new EditorGUI.DisabledScope(hasZSerializers))
                    //     {
                    //         if (GUILayout.Button("Solve", GUILayout.Width(50)))
                    //         {
                    //             ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                    //         }
                    //     }
                    // }

                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label(new GUIContent(
                            icons[zuidsAreSetup][0]), GUILayout.Width(20), GUILayout.Height(20));
                        GUILayout.Label("ZUIDs Are correctly set ", GUILayout.Width(200));

                        using (new EditorGUI.DisabledScope(zuidsAreSetup))
                        {
                            if (GUILayout.Button("Solve", GUILayout.Width(50)))
                            {
                                ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                            }
                        }
                    }

                    using (new GUILayout.HorizontalScope())
                    {
                        GUILayout.Label(new GUIContent(
                            icons[areSampleScenesIn][1]), GUILayout.Width(20), GUILayout.Height(20));
                        GUILayout.Label("Sample Scenes setup ", GUILayout.Width(200));

                        using (new EditorGUI.DisabledScope(areSampleScenesIn))
                        {
                            if (GUILayout.Button("Solve", GUILayout.Width(50)))
                            {
                                ZSerializerEditor.AddSampleScenesToBuildSettings();
                            }
                        }
                    }

                    //using (new EditorGUI.DisabledScope(!zuidsAreSetup /*|| !hasZSerializers*/))
                    {
                        if (GUILayout.Button("Finish Setup"))
                        {
                            ZSerializerSettings.Instance.packageInitialized = true;
                            EditorUtility.SetDirty(ZSerializerSettings.Instance);
                            AssetDatabase.SaveAssets();
                        }
                    }
                }


                // if (GUILayout.Button($"<color=#{ZSerializerStyler.MainHex}>Setup</color>", new GUIStyle("button") { fontSize = 48, font = Styler.header.font, richText = true},
                //     GUILayout.MinHeight(100)))
                // {
                //     ZSerializerSettings.Instance.packageInitialized = true;
                //     ZSerializerEditor.GenerateUnityComponentClasses();
                //     ZSerializerEditor.RefreshZUIDs();
                //     ZSerializerEditor.AddSampleScenesToBuildSettings();
                // }
            }
            else
            {
                using (new GUILayout.VerticalScope("box"))
                {
                    using (new EditorGUILayout.HorizontalScope())
                    {
                        using (var change = new EditorGUI.ChangeCheckScope())
                        {
                            searchText = GUILayout.TextField(searchText,
                                new GUIStyle(EditorStyles.toolbarSearchField) { fixedHeight = 28 });
                                
                            if (GUILayout.Button(searchProject ? Styler.hierarchyOnly : Styler.projectOnly,
                                GUILayout.Width(28), GUILayout.Height(28)))
                            {
                                searchProject = !searchProject;
                            }
                                
                            editMode = GUILayout.Toggle(editMode, Styler.cogWheel, new GUIStyle("button"),
                                GUILayout.Height(28), GUILayout.Width(28));
                                

                            if (change.changed) GetClasses();
                        }
                        
                        // if (GUILayout.Button("Refresh", GUILayout.Height(28)))
                        // {
                        //     Init();
                        // }
                    }

                    if (!editMode)
                        using (new EditorGUILayout.HorizontalScope())
                        {
                            
                        }


                    using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
                    {
                        scrollPos = scrollView.scrollPosition;

                        if (editMode)
                        {
                            ZSerializerEditor.BuildSettingsEditor(Styler, ref selectedMenu, ref selectedType,
                                ref selectedGroup, ref selectedComponentSettings, ref selectedGroupIndex,
                                position.width);
                        }
                        else
                        {
                            if (classes == null) GetClasses();

                            if (!string.IsNullOrEmpty(searchText))
                                classes = classes.Where(c => c.classType.Name.ToLower().Contains(searchText.ToLower()))
                                    .ToArray();

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
                                    using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.Window,
                                        GUILayout.Height(32),
                                        GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 20)))
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
                                            new GUIStyle(Styler.header)
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

                                        ZSerializerEditor.BuildWindowValidityButton(classInstance.classType, Styler);

                                        if (GUILayout.Button(Styler.cogWheel,
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
                        }
                    }

                    if (!editMode)
                    {
                        GUILayout.Space(5);
                        if (!Application.isPlaying)
                        {
                            GUILayout.Space(-15);
                            using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.Window,
                                GUILayout.Height(32), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 20)))
                            {
                                EditorGUILayout.LabelField("ZSerialize All",
                                    new GUIStyle(Styler.header)
                                        { alignment = TextAnchor.MiddleCenter, fontSize = fontSize },
                                    GUILayout.Height(classHeight));

                                ZSerializerEditor.BuildButtonAll(classes, classHeight, Styler);
                            }
                        }
                    }
                }
            }
        }
    }
}