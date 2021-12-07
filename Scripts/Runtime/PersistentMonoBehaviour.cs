using System;
using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSerializer
{
    public sealed class NonZSerialized : Attribute
    {
    }

    public sealed class ForceZSerialized : Attribute
    {
    }
    

    public abstract class PersistentMonoBehaviour : MonoBehaviour, IZSerializable
    {
        [Serializable]
        private class ZSBanner
        {
            
        }

        
        /// <summary>
        /// OnPreSave is called right before any Save occurs
        /// </summary>
        public virtual void OnPreSave()
        {
        }

        /// <summary>
        /// OnPostSave is called right after any Save occurs
        /// </summary>
        public virtual void OnPostSave()
        {
        }

        /// <summary>
        /// OnPreLoad is called right before any Load occurs
        /// </summary>
        public virtual void OnPreLoad()
        {
        }

        /// <summary>
        /// OnPostLoad is called right after any Load occurs
        /// </summary>
        public virtual void OnPostLoad()
        {
        }

        [NonZSerialized, SerializeField] private ZSBanner banner;
        [NonZSerialized, HideInInspector] public bool showSettings;
        [NonZSerialized, HideInInspector] public bool isSaving;
        [NonZSerialized, HideInInspector] public bool isLoading;

        [NonZSerialized, HideInInspector, SerializeField]
        internal bool isOn = true;

        [ForceZSerialized, HideInInspector, SerializeField]
        internal int groupID;

        [ForceZSerialized, HideInInspector, SerializeField]
        internal bool autoSync = true;
        [NonZSerialized, SerializeField, HideInInspector] private string _zuid;
        [NonZSerialized, SerializeField, HideInInspector] private string _gozuid;


        public int GroupID
        {
            get => groupID;
            set
            {
                if (AutoSync)
                {
                    ZSerializerSettings.Instance.componentDataDictionary[GetType()].groupID = value;
                    foreach (var o in FindObjectsOfType(GetType()))
                    {
                        if (((PersistentMonoBehaviour)o).AutoSync)
                        {
                            ((PersistentMonoBehaviour)o).groupID = value;
#if UNITY_EDITOR
                            EditorUtility.SetDirty(o);
#endif
                        }
                    }
                }
                else
                    groupID = value;
            }
        }

        public bool AutoSync
        {
            get => autoSync;
            set => autoSync = value;
        }

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
            get => isOn;
            set => isOn = value;
        }

        public virtual void Reset()
        {
            IsOn = ZSerializerSettings.Instance.componentDataDictionary[GetType()].isOn;
            GroupID = ZSerializerSettings.Instance.componentDataDictionary[GetType()].groupID;
            GenerateEditorZUIDs(false);
        }

        public void GenerateRuntimeZUIDs(bool forceGenerateGameObject)
        {
            ZUID = ZSerialize.GetRuntimeSafeZUID(typeof(PersistentGameObject));
            var zs = GetComponents<IZSerializable>().FirstOrDefault(zs => !string.IsNullOrEmpty(zs.GOZUID));
            GOZUID = forceGenerateGameObject ? ZSerialize.GetRuntimeSafeZUID(typeof(GameObject)) : zs != null ? zs.GOZUID : ZSerialize.GetRuntimeSafeZUID(typeof(GameObject));

            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<IZSerializable>().Where(c => !ReferenceEquals(c, this)))
                {
                    monoBehaviour.GenerateRuntimeZUIDs(false);
                }
        }

        public void GenerateEditorZUIDs(bool forceGenerateGameObject)
        {
#if UNITY_EDITOR
            ZUID = GUID.Generate().ToString();
            var zs = GetComponents<IZSerializable>().FirstOrDefault(zs => !string.IsNullOrEmpty(zs.GOZUID));
            GOZUID = forceGenerateGameObject ? GUID.Generate().ToString() : zs != null ? zs.GOZUID : GUID.Generate().ToString();

            EditorUtility.SetDirty(this);
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);

            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<IZSerializable>().Where(c => !ReferenceEquals(c, this)))
                {
                    monoBehaviour.GenerateEditorZUIDs(false);
                }
#endif
        }

        public void AddZUIDsToIDMap()
        {
            ZSerialize.idMap[ZSerialize.CurrentGroupID].TryAddToDictionary(ZUID, this);
            ZSerialize.idMap[ZSerialize.CurrentGroupID].TryAddToDictionary(GOZUID, gameObject);
        }

        // public virtual void OnDestroy()
        // {
        //     Debug.LogError(name + "GETTING DESTROYED");
        // }
    }
}