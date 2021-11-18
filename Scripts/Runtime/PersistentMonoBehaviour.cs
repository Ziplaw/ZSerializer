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
            ZUID = ZSerialize.GetRuntimeSafeZUID();
            var pg = GetComponent<PersistentGameObject>();
            GOZUID = forceGenerateGameObject ? ZSerialize.GetRuntimeSafeZUID() : pg && !string.IsNullOrEmpty(pg.GOZUID) ? pg.GOZUID : ZSerialize.GetRuntimeSafeZUID();


            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<MonoBehaviour>().Where(c => c != this && c is IZSerializable))
                {
                    (monoBehaviour as IZSerializable).GenerateRuntimeZUIDs(false);
                }
        }

        public void GenerateEditorZUIDs(bool forceGenerateGameObject)
        {
#if UNITY_EDITOR
            ZUID = GUID.Generate().ToString();
            var pg = GetComponent<PersistentGameObject>();
            GOZUID = forceGenerateGameObject ? GUID.Generate().ToString() : pg && !string.IsNullOrEmpty(pg.GOZUID) ? pg.GOZUID : GUID.Generate().ToString();

            EditorUtility.SetDirty(this);
            PrefabUtility.RecordPrefabInstancePropertyModifications(this);

            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<MonoBehaviour>().Where(c => c != this && c is IZSerializable))
                {
                    (monoBehaviour as IZSerializable).GenerateEditorZUIDs(false);
                }
#endif
        }

        public void AddZUIDsToIDMap()
        {
            ZSerialize.idMap.TryAdd(ZUID, this);
            ZSerialize.idMap.TryAdd(GOZUID, gameObject);
        }

        // public virtual void OnDestroy()
        // {
        //     Debug.LogError(name + "GETTING DESTROYED");
        // }
    }
}