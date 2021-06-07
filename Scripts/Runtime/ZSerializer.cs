using System;
using System.Reflection;
using UnityEngine;
using ZSaver;
using Object = UnityEngine.Object;

namespace ZSaver
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
    public abstract class ZSerializer<T> where T : Component
    {
        [OmitSerializableCheck] public int gameObjectInstanceID;
        [OmitSerializableCheck] public int componentinstanceID;
        [OmitSerializableCheck] public GameObject _componentParent;
        [OmitSerializableCheck] public T _component;

        public ZSerializer(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
            gameObjectInstanceID = componentParent.GetInstanceID();
            componentinstanceID = component.GetInstanceID();
        }
    }
}
