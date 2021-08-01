using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ZSerializer
{
    // [CreateAssetMenu(fileName = "New ZSerializer Settings", menuName = "ZSaverSettings", order = 0)]
    [Serializable]
    public class SerializableComponentBlackList
    {
        public Type Type => Type.GetType(typeFullName);
        [SerializeField] private string typeFullName;
        public List<string> componentNames;

        public SerializableComponentBlackList(Type type, string componentName)
        {
            typeFullName = type.AssemblyQualifiedName;
            componentNames = new List<string>{componentName};
        }
    }
    public class ZSaverSettings : ScriptableObject
    {
        private static ZSaverSettings instance;

        public static ZSaverSettings Instance => instance ? instance : Resources.Load<ZSaverSettings>("ZSaverSettings");

        
        // TODO: uncomment this before every commit
        [HideInInspector]
        public bool packageInitialized;
        public bool debugMode;
        public bool autoRebuildZSerializers;
        public int selectedSaveFile;
        public bool encryptData;
        public bool stableSave;
        [SerializeField][HideInInspector]public List<SerializableComponentBlackList> componentBlackList;
        

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            instance = Resources.Load<ZSaverSettings>("ZSaverSettings");
        }
    }
}