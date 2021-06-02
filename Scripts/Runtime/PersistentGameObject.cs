using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ZSaver;
using SaveType = ZSaver.SaveType;

[AddComponentMenu("ZSaver/Persistent GameObject"), DisallowMultipleComponent]
public class PersistentGameObject : MonoBehaviour
{
    [Serializable]
    public struct SerializableComponentData
    {
        public string typeName;
        public bool serialize;

        public SerializableComponentData(Type type)
        {
            typeName = type.FullName + ", " + type.Assembly;
            serialize = true;
        }
    }
    
    public SerializableComponentData[] _componentDatas;

    private void Start()
    {
        name = gameObject.GetInstanceID().ToString();
    }
    public static int CountParents(Transform transform)
    {
        int totalParents = 1;
        if (transform.parent != null)
        {
            totalParents += CountParents(transform.parent);
        }

        return totalParents;
    }
}