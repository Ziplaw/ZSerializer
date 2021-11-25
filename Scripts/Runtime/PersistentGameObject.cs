using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace ZSerializer
{
    public enum PersistentType
    {
        Everything,
        Component,
        None
    };

    [Serializable]
    public class SerializedComponent
    {
        [SerializeField] private string typeFullName;
        public Type Type => Type.GetType(typeFullName);
        public PersistentType persistenceType;
        public Component component;
        public string zuid;

        public SerializedComponent(Component component, string zuid, PersistentType persistenceType)
        {
            this.component = component;
            typeFullName = component.GetType().AssemblyQualifiedName;
            this.zuid = zuid;
            this.persistenceType = persistenceType;
        }
    }

    [AddComponentMenu("ZSerializer/Persistent GameObject"), DisallowMultipleComponent]
    public sealed class PersistentGameObject : MonoBehaviour, IZSerializable
    {
        [NonZSerialized] public bool showSettings;
        [SerializeField, HideInInspector] private int groupID;
        [SerializeField] private string _zuid;
        [SerializeField] private string _gozuid;

        public List<SerializedComponent> serializedComponents = new List<SerializedComponent>();

        public Dictionary<Component, string> ComponentZuidMap =>
            serializedComponents.ToDictionary(s => s.component, s => s.zuid);


        public int GroupID
        {
            get => groupID;
            set => groupID = value;
        }

        public bool AutoSync => false;

        public string ZUID
        {
            get => _zuid;
            set => _zuid = value;
        }

        public string GOZUID
        {
            get => _gozuid;
            set => _gozuid = value;
        }

        public bool IsOn
        {
            get => true;
            set => throw new SerializationException("You can't change a PersistentGameObject's On/Off state");
        }

        void GenerateComponentZUIDs()
        {
            var czlist = new List<SerializedComponent>();

            foreach (var component in GetComponents<Component>().Where(c =>
                !(c is IZSerializable) && ZSerialize.UnitySerializableTypes.Contains(c.GetType())))
            {
                var thisComponentZuid =
                    serializedComponents.FirstOrDefault(cz => cz.component == component);

                if (thisComponentZuid != default) czlist.Add(thisComponentZuid);
                else
                    czlist.Add(
                        new SerializedComponent(component,
                            Application.isPlaying ? ZSerialize.GetRuntimeSafeZUID() : GUID.Generate().ToString(),
                            PersistentType.Everything));
            }
            
#if UNITY_EDITOR
            if (!serializedComponents.SequenceEqual(czlist))
            {
                // PrefabUtility.RecordPrefabInstancePropertyModifications(this);
                EditorUtility.SetDirty(this);
            }
#endif

            serializedComponents = czlist;
            
            
        }

#if UNITY_EDITOR
        PersistentGameObject()
        {
            EditorApplication.hierarchyChanged -= ComponentListChanged;
            EditorApplication.hierarchyChanged += ComponentListChanged;
        }

        private static void ComponentListChanged()
        {
            PersistentGameObject persistentGameObject =
                Selection.activeGameObject?.GetComponent<PersistentGameObject>();

            if (!persistentGameObject || Application.isPlaying) return;

            persistentGameObject.GenerateComponentZUIDs();
        }


        public void Reset()
        {
            GenerateEditorZUIDs(false);
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
            EditorUtility.SetDirty(this);
        }

#endif

        public void GenerateRuntimeZUIDs(bool forceGenerateGameObject)
        {
            ZUID = ZSerialize.GetRuntimeSafeZUID();
            var pg = GetComponent<PersistentGameObject>();
            GOZUID = forceGenerateGameObject ? ZSerialize.GetRuntimeSafeZUID() :
                pg && !string.IsNullOrEmpty(pg.GOZUID) ? pg.GOZUID : ZSerialize.GetRuntimeSafeZUID();
            GenerateComponentZUIDs();

            serializedComponents.ForEach(sc => sc.zuid = ZSerialize.GetRuntimeSafeZUID());


            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<MonoBehaviour>()
                    .Where(c => c != this && c is IZSerializable))
                {
                    (monoBehaviour as IZSerializable).GenerateRuntimeZUIDs(false);
                }
        }

        public void GenerateEditorZUIDs(bool forceGenerateGameObject)
        {
#if UNITY_EDITOR

            ZUID = GUID.Generate().ToString();
            var pm = GetComponent<PersistentMonoBehaviour>();

            GOZUID = forceGenerateGameObject ? GUID.Generate().ToString() :
                pm && !string.IsNullOrEmpty(pm.GOZUID) ? pm.GOZUID : GUID.Generate().ToString();
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
            EditorUtility.SetDirty(this);

            serializedComponents.ForEach(sc => sc.zuid = GUID.Generate().ToString());
            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<MonoBehaviour>()
                    .Where(c => c != this && c is IZSerializable))
                {
                    (monoBehaviour as IZSerializable).GenerateEditorZUIDs(false);
                }
#endif
        }

        public void AddZUIDsToIDMap()
        {
            ZSerialize.idMap[ZSerialize.CurrentGroupID].TryAdd(ZUID, this);
            ZSerialize.idMap[ZSerialize.CurrentGroupID].TryAdd(GOZUID, gameObject);
            foreach (var serializedComponent in serializedComponents)
            {
                ZSerialize.idMap[ZSerialize.CurrentGroupID]
                    .TryAdd(serializedComponent.zuid, serializedComponent.component);
            }
        }

        public T AddComponent<T>(PersistentType persistentType) where T : Component
        {
            return (T)AddComponent(typeof(T), persistentType);
        }

        public Component AddComponent(Type type, PersistentType persistentType = PersistentType.Everything)
        {
            var c = gameObject.AddComponent(type);
            if (ZSerialize.UnitySerializableTypes.Contains(type))
            {
                string zuid = ZSerialize.GetRuntimeSafeZUID();
                serializedComponents.Add(new SerializedComponent(c, zuid,
                    persistentType));
                // ZSerialize.idMap[ZSerialize.CurrentGroupID][zuid] = c;
            }

            return c;
        }

        public void RemoveComponent(Component component)
        {
            // ZSerialize.idMap[ZSerialize.CurrentGroupID].Remove(ComponentZuidMap[component]);
            serializedComponents.Remove(serializedComponents.First(c => c.component == component));
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

        public Vector3 localPosition;
        public Quaternion localRotation;
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

            o.transform.SetParent(data.parent != null ? data.parent.transform : null);
            o.transform.SetSiblingIndex(data.loadingOrder.y);

            o.transform.localPosition = data.localPosition;
            o.transform.localRotation = data.localRotation;
            o.transform.localScale = data.size;


            return o;
        }
    }
}