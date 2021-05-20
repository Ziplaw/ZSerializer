using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ZSaver;
using SaveType = ZSaver.SaveType;

[AddComponentMenu("ZSaver/Persistent GameObject")]
public class PersistentGameObject : MonoBehaviour
{
    public Text text;
    private void Start()
    {
        Debug.Log("Created");
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