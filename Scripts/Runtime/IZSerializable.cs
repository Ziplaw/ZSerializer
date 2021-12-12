using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSerializer
{
    public interface IZSerializable
    {
        public int GroupID { get; set; }
        public bool AutoSync { get; }
        public string ZUID { get; set; }
        public string GOZUID { get; set; }
        public bool IsOn { get; set; }
        public bool IsSaving { get; set; }
        public bool IsLoading { get; set; }

        /// <summary>
        /// OnPreSave is called right before any Save occurs
        /// </summary>
        public void OnPreSave();

        /// <summary>
        /// OnPostSave is called right after any Save occurs
        /// </summary>
        public void OnPostSave();

        /// <summary>
        /// OnPreLoad is called right before any Load occurs
        /// </summary>
        public void OnPreLoad();

        /// <summary>
        /// OnPostLoad is called right after any Load occurs
        /// </summary>
        public void OnPostLoad();
        
        void GenerateRuntimeZUIDs(bool forceGenerateGameObject);
        void GenerateEditorZUIDs(bool forceGenerateGameObject);
        void AddZUIDsToIDMap();
    }
}