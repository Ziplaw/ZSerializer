using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSerializer
{

    public class NonZSerialized : Attribute
    {
    }

    
    public interface ISaveGroupID
    {
        public int GroupID { get; }
        public bool AutoSync { get; }
    }
    
    public class PersistentMonoBehaviour : MonoBehaviour, ISaveGroupID
    {
        private void Start()
        {
            Debug.Log(name + " " + groupID + " " + autoSync);
        }

        /// <summary>
        /// OnPreSave is called right before any Save occurs
        /// </summary>
        public virtual void OnPreSave(){}
        /// <summary>
        /// OnPostSave is called right after any Save occurs
        /// </summary>
        public virtual void OnPostSave(){}
        /// <summary>
        /// OnPreLoad is called right before any Load occurs
        /// </summary>
        public virtual void OnPreLoad(){}
        /// <summary>
        /// OnPostLoad is called right after any Load occurs
        /// </summary>
        public virtual void OnPostLoad(){}

        [HideInInspector]public int groupID;
        [HideInInspector]public bool autoSync = true;
        public int GroupID => groupID;
        public bool AutoSync => autoSync;
    }
}