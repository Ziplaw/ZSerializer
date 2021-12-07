﻿using System;
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
    private static ZSerializerStyler styler;

    private void OnEnable()
    {
        manager = target as PersistentGameObject;
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        styler = new ZSerializerStyler();
    }


    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        using (var check = new EditorGUI.ChangeCheckScope())
        {
            GUILayout.Space(-15);
            using (new EditorGUILayout.HorizontalScope(ZSerializerStyler.window))
            {
                var unmanagedTypes = manager.serializedComponents.Select(sc => sc.typeFullName)
                    .Except(ZSerializerSettings.Instance.unityComponentTypes).ToList();

                string color = unmanagedTypes.Any() ? ZSerializerStyler.YellowHex : ZSerializerStyler.MainHex;

                GUILayout.Label($"<color=#{color}>  Persistent GameObject</color>", styler.header,
                    GUILayout.MinHeight(28));
                if (unmanagedTypes.Any())
                {
                    if (GUILayout.Button(styler.refreshWarningImage, GUILayout.Height(28), GUILayout.Width(28)))
                    {
                        ZSerializerSettings.Instance.unityComponentTypes.AddRange(unmanagedTypes);
                        EditorUtility.SetDirty(ZSerializerSettings.Instance);
                        AssetDatabase.SaveAssets();
                        ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                    }
                }

                manager.showSettings = ZSerializerEditor.SettingsButton(manager.showSettings, styler, 28);
                PrefabUtility.RecordPrefabInstancePropertyModifications(manager);
            }

            if (manager.showSettings)
            {
                ZSerializerEditor.ShowGroupIDSettings(typeof(PersistentGameObject), manager, false);
                if (ZSerializerSettings.Instance.advancedSerialization)
                {
                    GUILayout.Space(-15);
                    using (new GUILayout.VerticalScope(ZSerializerStyler.window))
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
                                        fontColor = new Color(0.61f, 1f, 0.94f);
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
                                    (ZSerializerSettings.Instance.debugMode == DebugMode.Developer
                                        ? $"({component.zuid})"
                                        : ""),
                                    new GUIStyle("helpbox")
                                    {
                                        font = styler.header.font,
                                        normal = new GUIStyleState() { textColor = fontColor },
                                        alignment = TextAnchor.MiddleCenter
                                    },
                                    GUILayout.MaxWidth(ZSerializerSettings.Instance.debugMode == DebugMode.Developer
                                        ? 150
                                        : 100));

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
            else
            {
                if (manager.serializedComponents.Count > 0)
                {
                    GUILayout.Space(-15);

                    using (new GUILayout.HorizontalScope(ZSerializerStyler.window))
                    {
                        for (var i = 0; i < manager.serializedComponents.Count; i++)
                        {
                            var serializedComponent = manager.serializedComponents[i];

                            GUILayout.FlexibleSpace();
                            GUILayout.Box(EditorGUIUtility.ObjectContent(null,
                                    serializedComponent.component.GetType()).image, new GUIStyle("label"),
                                GUILayout.Width(16), GUILayout.Height(16));
                            // GUILayout.Label(serializedComponent.component.GetType().Name,
                            //     new GUIStyle("label") {fontSize = 12});
                            GUILayout.FlexibleSpace();
                            if (i != manager.serializedComponents.Count - 1)
                            {
                                GUILayout.Label("|",
                                    new GUIStyle("label")
                                        { fontSize = 12, normal = { textColor = new Color(0.51f, 0.51f, 0.51f) } });
                            }
                        }
                    }
                }

                if (ZSerializerSettings.Instance.debugMode == DebugMode.Developer)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.TextField("ZUID", manager.ZUID);
                        EditorGUILayout.TextField("GameObject ZUID", manager.GOZUID);
                    }
                }
            }
            
            if(check.changed)
                EditorUtility.SetDirty(manager);

        }

        serializedObject.ApplyModifiedProperties();
    }
}