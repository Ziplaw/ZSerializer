using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using ZSerializer;
using ZSerializer.Editor;

namespace ZSerializer.Editor
{
    public sealed class ZSerializerDebugger : EditorWindow
    {
        [MenuItem("Tools/ZSerializer/Serialization Debugger")]
        internal static void ShowWindow()
        {
            var window = GetWindow<ZSerializerDebugger>();
            window.titleContent = new GUIContent("ZSerializer Debugger");
            window.Show();
        }

        private int selectedTabIndex;
        private int selectedGroupID;
        private int selectedTypeIndex;
        Vector2 idStorageScrollPos;
        Vector2 jsonScrollPos;
        List<string[]> currentJsons = new List<string[]>();

        private void OnGUI()
        {
            string[] tabs = { "Scene Group Path Map", "Active ZUIDs" };
            selectedTabIndex = GUILayout.Toolbar(selectedTabIndex, tabs);


            switch (selectedTabIndex)
            {
                case 0:
                    using (var scrollView = new GUILayout.ScrollViewScope(idStorageScrollPos))
                    {
                        idStorageScrollPos = scrollView.scrollPosition;
                        foreach (var keyValuePair in ZSerialize.sceneToLoadingSceneMap)
                        {
                            using (new GUILayout.HorizontalScope())
                            {
                                GUILayout.Label(keyValuePair.Key);
                            }
                            
                        }
                    }

                    break;
                case 1:
                    foreach (var keyValuePair in ZSerialize.idMap)
                    {
                        GUILayout.Label($"{keyValuePair.Key}: {keyValuePair.Value}");
                    }
                    break;
            }
        }
    }
}