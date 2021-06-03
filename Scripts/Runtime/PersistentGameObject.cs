using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZSaver;
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

    public List<SerializableComponentData> _componentDatas = new List<SerializableComponentData>();
    public void UpdateSerializableComponents()
    {
        if (!Application.isPlaying)
        {
            var componentTypes = GetComponents<Component>().Select(c => c.GetType()).Where(type =>
                ZSave.ComponentSerializableTypes.Contains(type) &&
                !type.IsSubclassOf(typeof(MonoBehaviour))).ToArray();

            var serializableComponentTypes = _componentDatas.Select(c => Type.GetType(c.typeName)).ToArray();

            for (var i = 0; i < serializableComponentTypes.Length; i++)
            {
                if (!componentTypes.Contains(serializableComponentTypes[i]))
                {
                    _componentDatas.RemoveAt(i);
                }
            }

            for (var i = 0; i < componentTypes.Length; i++)
            {
                if (!serializableComponentTypes.Contains(componentTypes[i]))
                {
                    _componentDatas.Add(new SerializableComponentData(componentTypes[i]));
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