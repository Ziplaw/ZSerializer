using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZSerializer;
using Component = UnityEngine.Component;

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

    [NonZSerialized] public List<SerializableComponentData> _componentDatas = new List<SerializableComponentData>();
    public void UpdateSerializableComponents(IEnumerable<Type> serializableTypes)
    {
        // if (!Application.isPlaying)
        {
            var componentTypes = GetComponents<Component>().Where(c => !c.GetType().IsSubclassOf(typeof(MonoBehaviour)) && c.GetType() != typeof(Transform)).Select(c => c.GetType());

            var serializableComponentTypes = _componentDatas.Select(c => Type.GetType(c.typeName));

            for (var i = 0; i < serializableComponentTypes.Count(); i++)
            {
                if (!componentTypes.Contains(serializableComponentTypes.ElementAt(i)) && i < _componentDatas.Count)
                {
                    _componentDatas.RemoveAt(i);
                }
            }

            for (var i = 0; i < componentTypes.Count(); i++)
            {
                if (!serializableComponentTypes.Contains(componentTypes.ElementAt(i)))
                {
                    _componentDatas.Add(new SerializableComponentData(componentTypes.ElementAt(i)));
                }
            }
        }
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