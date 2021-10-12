using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
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


    public interface ISaveGroupID
    {
        public int GroupID { get; set; }
        public bool AutoSync { get; }
    }

    public class PersistentMonoBehaviour : MonoBehaviour, ISaveGroupID
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

        [NonZSerialized, HideInInspector, SerializeField]
        internal bool isOn = true;

        [ForceZSerialized, HideInInspector, SerializeField]
        internal int groupID;

        [ForceZSerialized, HideInInspector, SerializeField]
        internal bool autoSync = true;

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

        public virtual void Reset()
        {
            isOn = ZSerializerSettings.Instance.componentDataDictionary[GetType()].isOn;
            GroupID = ZSerializerSettings.Instance.componentDataDictionary[GetType()].groupID;
        }
    }
}