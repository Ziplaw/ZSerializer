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
        
        using (new EditorGUILayout.HorizontalScope("helpbox"))
        {
            GUILayout.Label("<color=#29cf42>Persistent GameObject</color>", styler.header, GUILayout.MinHeight(32));
            manager.showSettings = ZSaverEditor.SettingsButton(manager.showSettings, styler, 32);
            PrefabUtility.RecordPrefabInstancePropertyModifications(manager);
        }

        if (manager.showSettings)
        {
            ZSaverEditor.ShowGroupIDSettings(typeof(PersistentGameObject), manager,false);
        }

        serializedObject.ApplyModifiedProperties();
    }
}