﻿using System;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEngine;
using ZSave.Editor;


[CustomEditor(typeof(PersistentGameObject))]
public class PersistentGameObjectEditor : Editor
{
    private static ZSaverStyler styler;
    private PersistentGameObject manager;

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
        using (new EditorGUILayout.VerticalScope("helpbox"))
            GUILayout.Label("<color=#29cf42>Persistent GameObject</color>", styler.header);

        // base.OnInspectorGUI();
    }
}