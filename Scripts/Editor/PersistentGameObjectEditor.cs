using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using ZSaver;
using ZSaver.Editor;


[CustomEditor(typeof(PersistentGameObject))]
public class PersistentGameObjectEditor : Editor
{
    private static ZSaverStyler styler;
    private PersistentGameObject manager;
    private IEnumerable<Type> serializableTypes;

    private void OnEnable()
    {
        serializableTypes = ZSave.ComponentSerializableTypes;
        manager = target as PersistentGameObject;
        // if (!Application.isPlaying && manager._componentDatas == null || manager._componentDatas.Count == 0)
        // {
        //     manager.UpdateSerializableComponents();
        //     // manager._componentDatas =
        //     //     (from c in manager.GetComponents<Component>().Where(c =>
        //     //             ZSave.ComponentSerializableTypes.Contains(c.GetType()) &&
        //     //             !c.GetType().IsSubclassOf(typeof(MonoBehaviour)))
        //     //         select new PersistentGameObject.SerializableComponentData(c.GetType())).ToArray();
        // }
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        styler = new ZSaverStyler();
    }

    private bool showSettings;

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        // base.OnInspectorGUI();
        using (new EditorGUILayout.HorizontalScope("helpbox"))
        {
            GUILayout.Label("<color=#29cf42>Persistent GameObject</color>", styler.header, GUILayout.MinHeight(32));
            showSettings = GUILayout.Toggle(showSettings, styler.cogWheel, new GUIStyle("button"),
                GUILayout.MaxHeight(32), GUILayout.MaxWidth(32));
            

            if (manager.GetComponents<Component>().Count(c => !c.GetType().IsSubclassOf(typeof(MonoBehaviour)) && c.GetType() != typeof(Transform)) !=
                manager._componentDatas.Count)
            {
                manager.UpdateSerializableComponents(serializableTypes);
                return;
            }
        }

        if (showSettings)
        {
            var managerComponentDatas = manager._componentDatas;
            using (new GUILayout.VerticalScope("helpbox"))
            {
                using (new GUILayout.VerticalScope("box"))
                {
                    GUILayout.Label("Components to Serialize",
                        new GUIStyle("label") {alignment = TextAnchor.MiddleCenter});
                    // if(GUILayout.Button())
                }

                for (var i = 0; i < managerComponentDatas.Count; i++)
                {
                    using (new GUILayout.HorizontalScope())
                    {
                        EditorGUILayout.PropertyField(
                            serializedObject.FindProperty(nameof(manager._componentDatas)).GetArrayElementAtIndex(i)
                                .FindPropertyRelative("serialize"), GUIContent.none, GUILayout.MaxWidth(20));
                        GUILayout.Label(Type.GetType(managerComponentDatas[i].typeName).ToString().Split('.').Last());
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}