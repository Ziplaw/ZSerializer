using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using ZSerializer;
using ZSerializer.Editor;


[CustomEditor(typeof(PersistentGameObject))]
public class PersistentGameObjectEditor : Editor
{
    private PersistentGameObject manager;
    private static ZSaverStyler styler;

    private void OnEnable()
    {
        manager = target as PersistentGameObject;
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        styler = new ZSaverStyler();
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        GUILayout.Space(-15);
        using (new EditorGUILayout.HorizontalScope(styler.window))
        {
            GUILayout.Label("<color=#29cf42>  Persistent GameObject</color>", styler.header, GUILayout.MinHeight(32));
            manager.showSettings = ZSaverEditor.SettingsButton(manager.showSettings, styler, 32);
            PrefabUtility.RecordPrefabInstancePropertyModifications(manager);
        }

        if (manager.showSettings)
        {
            ZSaverEditor.ShowGroupIDSettings(typeof(PersistentGameObject), manager, false);
            if (ZSaverSettings.Instance.advancedSerialization)
            {
                using (new GUILayout.VerticalScope("helpbox"))
                {
                    GUILayout.Label("Serialized Components:");
                    if (manager.serializedComponents.Count == 0) GUILayout.Label("None");
                    for (var i = 0; i < manager.serializedComponents.Count; i++)
                    {
                        using (new GUILayout.HorizontalScope())
                        {
                            serializedObject.Update();

                            var component = manager.serializedComponents[i];
                            Color fontColor = Color.cyan;
                            switch (component.persistenceType)
                            {
                                case PersistentType.Everything:
                                    fontColor = new Color(41 / 255f, 207 / 255f, 66 / 255f);
                                    break;
                                case PersistentType.Component:
                                    fontColor = new Color(1f, 0.79f, 0.47f);
                                    break;
                                case PersistentType.None:
                                    fontColor = new Color(1f, 0.56f, 0.54f);
                                    break;
                            }

                            GUILayout.Label(
                                component.Type.Name +
                                (ZSaverSettings.Instance.debugMode ? $"({component.instanceID})" : ""),
                                new GUIStyle("helpbox")
                                {
                                    font = styler.header.font, normal = new GUIStyleState() {textColor = fontColor},
                                    alignment = TextAnchor.MiddleCenter
                                }, GUILayout.MaxWidth(ZSaverSettings.Instance.debugMode ? 150 : 100));

                            EditorGUILayout.PropertyField(serializedObject
                                .FindProperty(nameof(manager.serializedComponents)).GetArrayElementAtIndex(i)
                                .FindPropertyRelative("persistenceType"), GUIContent.none);

                            serializedObject.ApplyModifiedProperties();
                        }
                    }

                    if (GUILayout.Button("Reset"))
                    {
                        manager.serializedComponents.Clear();
                        manager.Reset();
                    }
                }
            }
        }

        serializedObject.ApplyModifiedProperties();
    }
}