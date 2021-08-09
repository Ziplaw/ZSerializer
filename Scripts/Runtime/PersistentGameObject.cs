using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZSerializer;
using Component = UnityEngine.Component;

[AddComponentMenu("ZSaver/Persistent GameObject"), DisallowMultipleComponent]
public class PersistentGameObject : MonoBehaviour
{
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