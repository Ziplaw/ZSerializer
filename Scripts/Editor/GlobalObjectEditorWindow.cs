using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;
using Assembly = System.Reflection.Assembly;
using Object = UnityEngine.Object;

namespace ZSerializer.Editor
{
    class GlobalObjectEditorData
    {
        public bool active;
        public Type type;
        public GlobalObject instance;

        public GlobalObjectEditorData(Type t)
        {
            type = t;
            active = false;
            instance = GlobalObject.Get(t);
        }
    }

    public class GlobalObjectEditorWindow : EditorWindow
    {
        private string template;
        private string template2;

        private bool isCreatingObject;
        private string newObjectName = String.Empty;

        private List<GlobalObjectEditorData> globalDataTypes = new List<GlobalObjectEditorData>();

        private Vector2 scrollPos;

        private static ZSerializerStyler styler;

        private Dictionary<Type, List<FieldInfo>> fieldMap = new Dictionary<Type, List<FieldInfo>>();
        private string searchText;

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

        [MenuItem("Tools/ZSerializer/Global Object Manager", false, 2)]
        private static void ShowWindow()
        {
            var window = GetWindow<GlobalObjectEditorWindow>();
            window.titleContent = new GUIContent("Global Objects");
            window.Show();
        }

        private void OnEnable()
        {
            template = AssetDatabase
                .LoadAssetAtPath<TextAsset>("Assets/ZSerializer/Scripts/Editor/Templates/NewGlobalObject.cs.txt").text;
            template2 = AssetDatabase
                .LoadAssetAtPath<TextAsset>("Assets/ZSerializer/Scripts/Editor/Templates/NewGlobalObjectImpl.cs.txt")
                .text;

            Init();
        }

        private void OnGUI()
        {
            GUILayout.BeginVertical("box");

            using (var change = new EditorGUI.ChangeCheckScope())
            {
                searchText = GUILayout.TextField(searchText,
                    new GUIStyle(EditorStyles.toolbarSearchField) { fixedHeight = 28 });
                        
                if(change.changed) Init();
            }
            
            if (globalDataTypes.Count > 0)
            {
                using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
                {
                    scrollPos = scrollView.scrollPosition;

                    

                    foreach (var globalDataType in globalDataTypes)
                    {
                        GUILayout.BeginVertical();
                        GUILayout.Space(-15);
                        GUILayout.BeginHorizontal(ZSerializerStyler.Window,
                            GUILayout.Height(32), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 20));

                        string color = ZSerializerStyler.MainHex;

                        if (GUILayout.Button($"<color=#{color}>{globalDataType.type.Name}</color>",
                            new GUIStyle(Styler.header) { alignment = TextAnchor.MiddleCenter }))
                        {
                            globalDataType.active = !globalDataType.active;
                            if (globalDataType.active) Selection.activeObject = GlobalObject.Get(globalDataType.type);
                        }

                        if (GUILayout.Button(Styler.editIcon, GUILayout.MaxWidth(32), GUILayout.MaxHeight(32)))
                        {
                            AssetDatabase.OpenAsset(
                                AssetDatabase.LoadAssetAtPath<TextAsset>(GetScriptPath(globalDataType.type)));
                            Selection.activeObject = GlobalObject.Get(globalDataType.type);
                        }

                        GUILayout.EndHorizontal();

                        if (globalDataType.active)
                            using (new EditorGUI.DisabledScope(true))
                            {
                                UnityEditor.Editor.CreateEditor(GlobalObject.Get(globalDataType.type)).OnInspectorGUI();
                            }


                        // var so = new SerializedObject(GlobalObject.Get(globalDataType));
                        // foreach (var fieldInfo in fieldMap[globalDataType])
                        // {
                        //
                        //     GUILayout.BeginHorizontal();
                        //     GUILayout.Label($"<color=#{ZSerializerStyler.MainHex}>{fieldInfo.Name}</color>", Styler.richText);
                        //     EditorGUILayout.PropertyField(so.FindProperty(fieldInfo.Name),GUIContent.none);
                        //     GUILayout.EndHorizontal();
                        // }
                        // so.ApplyModifiedProperties();

                        GUILayout.EndVertical();
                    }
                }
            }

            if (isCreatingObject)
            {
                GUILayout.BeginHorizontal("box");
                newObjectName = EditorGUILayout.TextField("Name", newObjectName);


                if (GUILayout.Button("✓", GUILayout.MaxWidth(30)) && !string.IsNullOrEmpty(newObjectName))
                {
                    isCreatingObject = false;

                    newObjectName = newObjectName = Regex.Replace(newObjectName, @"[^a-zA-Z0-9_]", String.Empty); // all special caracters

                    GenerateNewObject(newObjectName, template, template2);
                    newObjectName = String.Empty;
                }

                if (GUILayout.Button("✕", GUILayout.MaxWidth(30)))
                {
                    isCreatingObject = false;
                    newObjectName = String.Empty;
                }

                GUILayout.EndHorizontal();
            }
            else
            {
                GUILayout.Space(-15);
                GUILayout.BeginHorizontal(ZSerializerStyler.Window,
                    GUILayout.Height(32), GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth));

                GUILayout.Label("New Global Object",
                    new GUIStyle(Styler.header) { fixedHeight = 32, alignment = TextAnchor.MiddleCenter });
                if (GUILayout.Button("+", GUILayout.MaxWidth(32), GUILayout.MaxHeight(32)))
                {
                    isCreatingObject = true;
                }

                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();
        }

        private void Init()
        {
            globalDataTypes.Clear();
            foreach (var type in Assembly.Load("Assembly-CSharp").GetTypes()
                .Where(t => (string.IsNullOrEmpty(searchText) || t.Name.ToLower().Contains(searchText.ToLower())) && t.IsSubclassOf(typeof(GlobalObject))))
            {
                globalDataTypes.Add(new GlobalObjectEditorData(type));
            }
        }

        [DidReloadScripts]
        private static void CreateInstanceOfGlobalObject()
        {
            if (!HasOpenInstances<GlobalObjectEditorWindow>()) return;

            var window = Resources.FindObjectsOfTypeAll<GlobalObjectEditorWindow>().FirstOrDefault();
            if (window == null) return;

            var globalDataType = window.globalDataTypes.FirstOrDefault(o => o.instance == null);
            if (globalDataType == null) return;

            if (!Directory.Exists(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Resources"))
                Directory.CreateDirectory(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Resources");

            var so = CreateInstance(globalDataType.type);
            AssetDatabase.CreateAsset(so,
                $"Assets/ZResources/ZSerializer/GlobalObjects/Resources/{globalDataType.type.Name}.asset");
            AssetDatabase.Refresh();
        }

        private static void GenerateNewObject(string newObjectName, string template, string template2)
        {
            if (!Directory.Exists(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Source"))
                Directory.CreateDirectory(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Source");

            FileStream fs = new FileStream(
                Application.dataPath + $"/ZResources/ZSerializer/GlobalObjects/Source/{newObjectName}.cs",
                FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(template.Replace("#SCRIPTNAME#", newObjectName));
            sw.Close();

            fs = new FileStream(
                Application.dataPath + $"/ZResources/ZSerializer/GlobalObjects/Source/{newObjectName}Impl.cs",
                FileMode.Create);
            sw = new StreamWriter(fs);

            sw.Write(template2.Replace("#SCRIPTNAME#", newObjectName));
            sw.Close();

            AssetDatabase.Refresh();
        }

        static string GetAssetPath(Type type)
        {
            return $"Assets/ZResources/ZSerializer/GlobalObjects/Instances/{type.Name}.asset";
        }

        static string GetScriptPath(Type type)
        {
            return $"Assets/ZResources/ZSerializer/GlobalObjects/Source/{type.Name}.cs";
        }
    }
}