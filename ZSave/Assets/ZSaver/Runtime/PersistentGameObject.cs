using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ZSave;
using SaveType = ZSave.SaveType;

[Persistent(SaveType.GameObject, ExecutionCycle.None)]
public class PersistentGameObject : MonoBehaviour
{
    public MeshRenderer Mrenderer;

    private void Start()
    {
        
    }
}

[CustomEditor(typeof(PersistentGameObject))]
public class PersistentGameObjectEditor : Editor
{
    private PersistentGameObject manager;
    private void OnEnable()
    {
        manager = target as PersistentGameObject;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        GUILayout.Space(60);

        MeshRenderer renderer = manager.Mrenderer;
        
        SerializedObject obj = new SerializedObject(renderer);

        foreach (var propertyInfo in typeof(MeshRenderer).GetProperties(BindingFlags.Public | BindingFlags.Instance).Where(r =>
            r.GetCustomAttributes<ObsoleteAttribute>() == null && r.CanRead && r.CanWrite))
        {
            EditorGUILayout.PropertyField(obj.FindProperty(propertyInfo.Name));
        }
    }
}
