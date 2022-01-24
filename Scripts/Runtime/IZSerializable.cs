using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSerializer
{
    public interface IZSerializable
    {
        int GroupID { get; set; }
        bool AutoSync { get; }
        string ZUID { get; set; }
        string GOZUID { get; set; }
        bool IsOn { get; set; }
        bool IsSaving { get; set; }
        bool IsLoading { get; set; }

        /// <summary>
        /// OnPreSave is called right before any Save occurs
        /// </summary>
        void OnPreSave();

        /// <summary>
        /// OnPostSave is called right after any Save occurs
        /// </summary>
        void OnPostSave();

        /// <summary>
        /// OnPreLoad is called right before any Load occurs
        /// </summary>
        void OnPreLoad();

        /// <summary>
        /// OnPostLoad is called right after any Load occurs
        /// </summary>
        void OnPostLoad();

        List<string> GetZUIDList();
        void GenerateRuntimeZUIDs(bool forceGenerateGameObject);
        void GenerateEditorZUIDs(bool forceGenerateGameObject);
        void AddZUIDsToIDMap();
    }
}