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
            string[] tabs = { "ID Storage", "Active ZUIDs", "Restoration Save Groups" };
            selectedTabIndex = GUILayout.Toolbar(selectedTabIndex, tabs);


            switch (selectedTabIndex)
            {
                case 0:
                    using (var scrollView = new GUILayout.ScrollViewScope(idStorageScrollPos))
                    {
                        idStorageScrollPos = scrollView.scrollPosition;
                        GUILayout.Toolbar(-1, new[] { "Original ID", "Temporary ID" });

                        // foreach (var keyValuePair in ZSerialize.idStorage)
                        // {
                        //     using (new GUILayout.HorizontalScope("helpbox"))
                        //     {
                        //         GUILayout.Label(keyValuePair.Key.ToString(),
                        //             new GUIStyle("label") { alignment = TextAnchor.MiddleCenter });
                        //         var color = "FFFFFF";
                        //         if (keyValuePair.Key != keyValuePair.Value) color = "67C795";
                        //         GUILayout.Label($"<color=#{color}>{keyValuePair.Value.ToString()}</color>",
                        //             new GUIStyle("label") { alignment = TextAnchor.MiddleCenter, richText = true });
                        //     }
                        // }
                    }

                    break;
                case 1:
                    foreach (var keyValuePair in ZSerialize.idMap)
                    {
                        GUILayout.Label($"{keyValuePair.Key}: {keyValuePair.Value}");
                    }

                    break;
                case 2:
                    GUILayout.Label("Active Groups for ID Restoration:");
                    ZSerialize.restorationIDList.ForEach(x =>
                        GUILayout.Label(ZSerializerSettings.Instance.saveGroups[x]));

                    break;
            }
        }
    }
}