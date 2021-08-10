using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using ZSerializer;

[AddComponentMenu("ZSaver/Persistent GameObject"), DisallowMultipleComponent]
public class PersistentGameObject : MonoBehaviour, ISaveGroupID
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

    [SerializeField][HideInInspector]private int groupID = -1;
    public int GroupID => groupID;
    public bool AutoSync => false;
}

namespace ZSerializer
{
    [Serializable]
    public struct GameObjectData
    {
        public int loadingOrder;
        public HideFlags hideFlags;
        public string name;
        public bool active;
        public bool isStatic;
        public int layer;
        public string tag;

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;

        public GameObject parent;

        public GameObject MakePerfectlyValidGameObject()
        {
            GameObject o = new GameObject();
            o.hideFlags = hideFlags;
            o.name = name;
            o.SetActive(active);
            o.isStatic = isStatic;
            o.layer = layer;
            o.tag = tag;

            o.transform.position = position;
            o.transform.rotation = rotation;
            o.transform.localScale = size;

            o.transform.SetParent(parent != null ? parent.transform : null);

            return o;
        }
    }
}