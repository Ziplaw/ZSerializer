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

    public class PersistentMonoBehaviour : MonoBehaviour, IZSerialize
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

        public void Start()
        {
            GenerateRuntimeZUIDs();

            ZSerialize.idMap.TryAdd(ZUID, this);
            ZSerialize.idMap.TryAdd(GOZUID, gameObject);
        }

        public void GenerateRuntimeZUIDs()
        {
            if (string.IsNullOrEmpty(ZUID)) ZUID = ZSerialize.GetRuntimeSafeZUID();
            var pg = GetComponent<PersistentGameObject>();
            if (string.IsNullOrEmpty(GOZUID))
                GOZUID = pg ? pg.GOZUID : ZSerialize.GetRuntimeSafeZUID();
        }

        public void GenerateEditorZUIDs(bool forceGenerateGameObject)
        {
#if UNITY_EDITOR
            ZUID = GUID.Generate().ToString();

            if (forceGenerateGameObject) GOZUID = GUID.Generate().ToString();
            else
            {
                var pg = GetComponent<PersistentGameObject>();
                if (pg) GOZUID = pg.GOZUID;
            }

            EditorUtility.SetDirty(this);

            if (forceGenerateGameObject)
                foreach (var monoBehaviour in GetComponents<MonoBehaviour>().Where(c => c != this && c is IZSerialize))
                {
                    (monoBehaviour as IZSerialize).GenerateEditorZUIDs(false);
                }
#endif
        }

        // public virtual void OnDestroy()
        // {
        //     Debug.LogError(name + "GETTING DESTROYED");
        // }
    }
}