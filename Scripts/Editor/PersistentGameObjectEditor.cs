using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using ZSerializer;
using ZSerializer.Editor;


[CustomEditor(typeof(PersistentGameObject))]
public class PersistentGameObjectEditor : Editor
{
    private static ZSaverStyler styler;

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
        }

        serializedObject.ApplyModifiedProperties();
    }
}