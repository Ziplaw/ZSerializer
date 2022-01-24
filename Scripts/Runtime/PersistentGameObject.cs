using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif
using UnityEngine;
using UnityEngine.Events;

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
        [SerializeField] internal string typeFullName;
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

        public SerializedComponent(SerializedComponent other)
        {
            component = other.component;
            typeFullName = other.component.GetType().AssemblyQualifiedName;
            zuid = other.zuid;
            persistenceType = other.persistenceType;
        }
    }

    [AddComponentMenu("ZSerializer/Persistent GameObject"), DisallowMultipleComponent]
    public class PersistentGameObject : MonoBehaviour, IZSerializable
    {
        [Serializable]
        public class PersistentEvent : UnityEvent
        {
            public List<string> targetZUIDs;
        }

        public PersistentEvent onPreSave;
        public PersistentEvent onPreLoad;
        public PersistentEvent onPostSave;
        public PersistentEvent onPostLoad;


        public void OnPreSave()
        {
            onPreSave?.Invoke();
        }

        public void OnPostSave()
        {
            onPostSave?.Invoke();
        }

        public void OnPreLoad()
        {
            onPreLoad?.Invoke();
        }

        public void OnPostLoad()
        {
            onPostLoad?.Invoke();
        }

        public List<string> GetZUIDList()
        {
            var list = new List<string> { ZUID, GOZUID };
            foreach (var serializedComponent in serializedComponents)
            {
                list.Add(serializedComponent.zuid);
            }

            return list;
        }

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

        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }


        void GenerateComponentZUIDs()
        {
            var czlist = new List<SerializedComponent>();

            foreach (var component in GetComponents<Component>().Where(c =>
                !(c is IZSerializable) && ZSerialize.UnitySerializableTypes.Contains(c.GetType())))
            {
                var thisComponentZuid =
                    serializedComponents.FirstOrDefault(cz => cz.component == component);
                bool isPrefab = false;

#if UNITY_EDITOR
                isPrefab = ZSerialize.IsPrefab(this);
#endif

                if (thisComponentZuid != default && !string.IsNullOrEmpty(thisComponentZuid.zuid))
                    czlist.Add(new SerializedComponent(thisComponentZuid)
                        { zuid = isPrefab ? "" : thisComponentZuid.zuid });
                else
                {
                    string id = "";


#if UNITY_EDITOR
                    id = isPrefab
                        ? ""
                        : Application.isPlaying
                            ? ZSerialize.GetRuntimeSafeZUID(component.GetType())
                            : GUID.Generate().ToString();
#else
                    id = ZSerialize.GetRuntimeSafeZUID(component.GetType());
#endif


                    czlist.Add(
                        new SerializedComponent(component, id,
                            PersistentType.Everything));
                }
            }

#if UNITY_EDITOR
            if (!serializedComponents.SerializedComponentListEquals(czlist))
            {
                EditorUtility.SetDirty(this);
            }
#endif
            serializedComponents = czlist;
        }

        public void UpdateSerializedComponentList()
        {
            UpdateComponentList(this);
        }

        internal static void UpdateGlobalZSerializerList(PersistentGameObject persistentGameObject,
            bool regenerateComponentSerializers = true)
        {
#if UNITY_EDITOR

            var unmanagedTypes = persistentGameObject.serializedComponents.Select(sc => sc.typeFullName)
                .Except(ZSerializerSettings.Instance.unityComponentTypes).ToList();

            if (unmanagedTypes.Count > 0)
            {
                ZSerializerSettings.Instance.unityComponentTypes.AddRange(unmanagedTypes);
                if (regenerateComponentSerializers) ZSerializerEditorRuntime.GenerateUnityComponentClasses();
                EditorUtility.SetDirty(ZSerializerSettings.Instance);
                AssetDatabase.SaveAssets();
            }
#endif
        }

        internal static void UpdateComponentList(PersistentGameObject persistentGameObject)
        {
            if (!persistentGameObject) return;

            persistentGameObject.GenerateComponentZUIDs();

            UpdateGlobalZSerializerList(persistentGameObject);
        }

#if UNITY_EDITOR
        PersistentGameObject()
        {
            EditorApplication.hierarchyChanged -= UpdateComponentList;
            EditorApplication.hierarchyChanged += UpdateComponentList;
        }

        internal static void UpdateComponentList()
        {
            UpdateComponentList(Selection.activeGameObject?.GetComponent<PersistentGameObject>());
        }


        private void Start()
        {
            // UpdateComponentList(this);

            if (onPreSave != null)
                for (int i = 0; i < onPreSave.GetPersistentEventCount(); i++)
                {
                    onPreSave.targetZUIDs.Add(onPreSave.GetPersistentTarget(i).GetZUID());
                }

            if (onPostSave != null)
                for (int i = 0; i < onPostSave.GetPersistentEventCount(); i++)
                {
                    onPostSave.targetZUIDs.Add(onPostSave.GetPersistentTarget(i).GetZUID());
                }

            if (onPreLoad != null)
                for (int i = 0; i < onPreLoad.GetPersistentEventCount(); i++)
                {
                    onPreLoad.targetZUIDs.Add(onPreLoad.GetPersistentTarget(i).GetZUID());
                }

            if (onPostLoad != null)
                for (int i = 0; i < onPostLoad.GetPersistentEventCount(); i++)
                {
                    onPostLoad.targetZUIDs.Add(onPostLoad.GetPersistentTarget(i).GetZUID());
                }
        }


        public void Reset()
        {
            GenerateEditorZUIDs(false);
            UpdateComponentList(this);
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
            EditorUtility.SetDirty(this);
        }

#endif

        public void GenerateRuntimeZUIDs(bool forceGenerateGameObject)
        {
            bool isPrefab = false;
#if UNITY_EDITOR
            isPrefab = ZSerialize.IsPrefab(this);

#endif
            // GenerateComponentZUIDs();
            ZUID = isPrefab ? "" : ZSerialize.GetRuntimeSafeZUID(typeof(PersistentGameObject));
            var zs = GetComponents<IZSerializable>().FirstOrDefault(z => !string.IsNullOrEmpty(z.GOZUID));
            GOZUID = isPrefab ? "" :
                forceGenerateGameObject ? ZSerialize.GetRuntimeSafeZUID(typeof(GameObject)) :
                zs != null ? zs.GOZUID : ZSerialize.GetRuntimeSafeZUID(typeof(GameObject));
            GenerateComponentZUIDs();

            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<IZSerializable>().Where(c => !ReferenceEquals(c, this)))
                {
                    monoBehaviour.GenerateRuntimeZUIDs(false);
                }
        }

        public void GenerateEditorZUIDs(bool forceGenerateGameObject)
        {
#if UNITY_EDITOR

            bool isPrefab = ZSerialize.IsPrefab(this);

            GenerateComponentZUIDs();

            ZUID = isPrefab ? "" : GUID.Generate().ToString();
            var zs = GetComponents<IZSerializable>().FirstOrDefault(z => !string.IsNullOrEmpty(z.GOZUID));
            GOZUID = isPrefab ? "" :
                forceGenerateGameObject ? GUID.Generate().ToString() :
                zs != null ? zs.GOZUID : GUID.Generate().ToString();
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);
            EditorUtility.SetDirty(this);

            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<IZSerializable>()
                    .Where(c => !ReferenceEquals(c, this)))
                {
                    monoBehaviour.GenerateEditorZUIDs(false);
                }
#endif
        }

        public void AddZUIDsToIDMap()
        {
            ZSerialize.idMap[ZSerialize.CurrentGroupID].TryAddToDictionary(ZUID, this);
            ZSerialize.idMap[ZSerialize.CurrentGroupID].TryAddToDictionary(GOZUID, gameObject);
            foreach (var serializedComponent in serializedComponents)
            {
                if (!serializedComponent.component) Debug.LogError("Component is null");
                ZSerialize.idMap[ZSerialize.CurrentGroupID]
                    .TryAddToDictionary(serializedComponent.zuid, serializedComponent.component);
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
                // string zuid = ZSerialize.GetRuntimeSafeZUID(type);
                serializedComponents.Add(new SerializedComponent(c, "" /*zuid*/,
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

    public static class PGExtensions
    {
        public static bool SerializedComponentListEquals(this List<SerializedComponent> l1,
            List<SerializedComponent> l2)
        {
            if (l1.Count != l2.Count) return false;
            for (int i = 0; i < l1.Count; i++)
            {
                if (l1[i].component != l2[i].component) return false;
                if (l1[i].zuid != l2[i].zuid) return false;
                if (l1[i].persistenceType != l2[i].persistenceType) return false;
            }
            return true;
        }
        
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