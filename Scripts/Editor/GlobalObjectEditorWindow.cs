using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Compilation;
using UnityEngine;
using Assembly = System.Reflection.Assembly;

namespace ZSerializer.Editor
{
    public class GlobalObjectEditorWindow : EditorWindow
    {
        private string template;
        
        private bool isCreatingObject;
        private string newObjectName;

        private Type[] globalDataTypes;
        
        private Vector2 scrollPos;

        private string missingInstanceTypeName;

        private static Assembly _baseAssembly;

        private static Assembly BaseAssembly => _baseAssembly ??= Assembly.Load("Assembly-CSharp");

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

            globalDataTypes = Assembly.Load("Assembly-CSharp").GetTypes().Where(t => t.IsSubclassOf(typeof(GlobalObject))).ToArray();
        }
        private void OnGUI()
        {
            if (globalDataTypes.Length > 0)
            {
                using (var scrollView = new EditorGUILayout.ScrollViewScope(scrollPos))
                {
                    scrollPos = scrollView.scrollPosition;

                    foreach (var globalDataType in globalDataTypes)
                    {
                        GUILayout.Space(-15);
                        using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.window,
                            GUILayout.Height(32),
                            GUILayout.MaxWidth(EditorGUIUtility.currentViewWidth - 20)))
                        {
                            string color = globalDataType.Name == missingInstanceTypeName
                                ? ZSerializerStyler.RedHex
                                : ZSerializerStyler.MainHex;
                            
                            GUILayout.Label($"<color=#{color}>{globalDataType.Name}</color>", new GUIStyle(Styler.header){ alignment = TextAnchor.MiddleCenter });
                        }
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
                    GenerateNewObject(newObjectName, template);
                    missingInstanceTypeName = newObjectName;
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
                if (GUILayout.Button("Create new Global Object"))
                {
                    isCreatingObject = true;
                }
        }
        [DidReloadScripts]
        private static void CreateInstanceOfGlobalObject()
        {
            var window = GetWindow<GlobalObjectEditorWindow>();
            if (window.missingInstanceTypeName == String.Empty) return;
            
            var type = BaseAssembly.GetType(window.missingInstanceTypeName);//
            
            if (!Directory.Exists(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Instances"))
                Directory.CreateDirectory(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Instances");
            
            var so = CreateInstance(type);
            AssetDatabase.CreateAsset(so, $"Assets/ZResources/ZSerializer/GlobalObjects/Instances/{type.Name}.asset");
            
            window.missingInstanceTypeName = String.Empty;
            
            AssetDatabase.Refresh();
        }
        
        private static void GenerateNewObject(string newObjectName, string template)
        {
            if (!Directory.Exists(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Source"))
                Directory.CreateDirectory(Application.dataPath + "/ZResources/ZSerializer/GlobalObjects/Source");

            FileStream fs = new FileStream(
                Application.dataPath + $"/ZResources/ZSerializer/GlobalObjects/Source/{newObjectName}.cs",
                FileMode.Create);
            StreamWriter sw = new StreamWriter(fs);

            sw.Write(template.Replace("#SCRIPTNAME#", newObjectName));
            sw.Close();

            AssetDatabase.Refresh();

        }
    }
}