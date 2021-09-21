using System;
using System.Reflection;
using UnityEngine;
using ZSerializer;
using Object = UnityEngine.Object;

namespace ZSerializer
{
    public abstract class ZSerializer<T> where T : Component
    {
        [NonZSerialized] public int gameObjectInstanceID;
        [NonZSerialized] public int componentinstanceID;
        [NonZSerialized] public GameObject _componentParent;
        [NonZSerialized] public T _component;

        public ZSerializer(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
            gameObjectInstanceID = componentParent.GetInstanceID();
            componentinstanceID = component.GetInstanceID();
        }

        public abstract void RestoreValues(T component);
    }
}
