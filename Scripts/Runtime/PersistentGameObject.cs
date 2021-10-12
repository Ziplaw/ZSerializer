using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using ZSerializer;

namespace ZSerializer
{
    public enum PersistentType
    {
        Everything,
        Component,
        None
    };

    [Serializable]
    public struct SerializedComponent
    {
        public Type Type => Type.GetType(typeFullName);
        [SerializeField] private string typeFullName;
        public PersistentType persistenceType;
        public int instanceID;

        public SerializedComponent(Component component, PersistentType persistenceType)
        {
            typeFullName = component.GetType().AssemblyQualifiedName;
            instanceID = component.GetInstanceID();
            this.persistenceType = persistenceType;
        }
    }

    [AddComponentMenu("ZSerializer/Persistent GameObject"), DisallowMultipleComponent]
    public sealed class PersistentGameObject : MonoBehaviour, ISaveGroupID
    {
        [NonZSerialized] public bool showSettings;
        [SerializeField] [HideInInspector] private int groupID;
        public List<SerializedComponent> serializedComponents = new List<SerializedComponent>();

        public int GroupID
        {
            get => groupID;
            set => groupID = value;
        }

        public bool AutoSync => false;

#if UNITY_EDITOR
        PersistentGameObject()
        {
            EditorApplication.hierarchyChanged -= ComponentListChanged;
            EditorApplication.hierarchyChanged += ComponentListChanged;
        }


        private static void ComponentListChanged()
        {
            if (ZSerializerSettings.Instance.advancedSerialization && Selection.activeGameObject &&
                !Application.isPlaying)
            {
                PersistentGameObject persistentGameObject =
                    Selection.activeGameObject.GetComponent<PersistentGameObject>();
                if (persistentGameObject)
                {
                    // Debug.Log("componentListChanged");
                    var list = new List<SerializedComponent>();
                    foreach (var serializedComponent in persistentGameObject.serializedComponents)
                    {
                        if (persistentGameObject.GetComponents(serializedComponent.Type)
                            .Any(c => c.GetInstanceID() == serializedComponent.instanceID))
                        {
                            list.Add(serializedComponent);
                        }
                    }

                    foreach (var component in persistentGameObject.GetComponents<Component>().Where(c =>
                        !(c is PersistentGameObject) && ZSerialize.ComponentSerializableTypes.Contains(c.GetType())))
                    {
                        if (persistentGameObject.serializedComponents.All(sc =>
                            sc.instanceID != component.GetInstanceID()))
                        {
                            list.Add(new SerializedComponent(component, PersistentType.Everything));
                        }
                    }

                    persistentGameObject.serializedComponents = list;
                }
            }
        }

#endif

        public void Reset()
        {
            if (ZSerializerSettings.Instance.advancedSerialization)
            {
                foreach (var component in GetComponents<Component>().Where(c =>
                    !(c is PersistentGameObject) && ZSerialize.ComponentSerializableTypes.Contains(c.GetType())))
                {
                    serializedComponents.Add(new SerializedComponent(component, PersistentType.Everything));
                }
            }
        }

        public T AddComponent<T>(PersistentType persistentType) where T : Component
        {
            return (T)AddComponent(typeof(T), persistentType);
        }

        public Component AddComponent(Type type, PersistentType persistentType)
        {
            var c = gameObject.AddComponent(type);
            if (typeof(MonoBehaviour).IsAssignableFrom(type))
                serializedComponents.Add(new SerializedComponent(c, persistentType));
            return c;
        }

        public void RemoveComponent(Component component)
        {
            for (var i = 0; i < serializedComponents.Count; i++)
            {
                if (serializedComponents[i].Type == component.GetType())
                {
                    serializedComponents.RemoveAt(i);
                    break;
                }
            }

            Destroy(component);
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

    [Serializable]
    public struct GameObjectData
    {
        public Vector2Int loadingOrder;
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
            return new GameObject().ApplyValues(this);
        }
    }

    public static class GameObjectExtensions
    {
        public static GameObject ApplyValues(this GameObject o, GameObjectData data)
        {
            o.hideFlags = data.hideFlags;
            o.name = data.name;
            o.SetActive(data.active);
            o.isStatic = data.isStatic;
            o.layer = data.layer;
            o.tag = data.tag;

            o.transform.position = data.position;
            o.transform.rotation = data.rotation;
            o.transform.localScale = data.size;

            o.transform.SetParent(data.parent != null ? data.parent.transform : null);
            o.transform.SetSiblingIndex(data.loadingOrder.y);

            return o;
        }
    }
}