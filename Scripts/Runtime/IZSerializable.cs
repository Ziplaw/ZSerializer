using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZSerializer
{
    internal interface IZSerializable
    {
        public int GroupID { get; set; }
        public bool AutoSync { get; }
        public string ZUID { get; set; }
        public string GOZUID { get; set; }
        public bool IsOn { get; set; }
        void GenerateRuntimeZUIDs();
        void GenerateEditorZUIDs(bool forceGenerateGameObject);
        void AddZUIDsToIDMap();
    }
}