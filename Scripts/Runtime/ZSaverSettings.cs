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
        [Serializable]
        public struct SaveGroup
        {
            public int index;
            public string name;

            public SaveGroup(int index, string name)
            {
                this.index = index;
                this.name = name;
            }
        }
        
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
        public bool advancedSerialization;
        [HideInInspector]
        public List<SerializableComponentBlackList> componentBlackList;
        [HideInInspector]public List<string> saveGroups = new List<string>()
        {
            "Main",
            "Settings",
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty,
            string.Empty
        };

        

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            instance = Resources.Load<ZSaverSettings>("ZSaverSettings");
        }
    }

    public static class Extensions
    {
        public static void SafeAdd(this List<SerializableComponentBlackList> list, Type componentType, string propertyName)
        {
            var s = list.FirstOrDefault(c => c.Type == componentType);

            if (s != null)
            {
                if (!s.componentNames.Contains(propertyName))
                {
                    s.componentNames.Add(propertyName);
                }
            }
            else
            {
                list.Add(
                    new SerializableComponentBlackList(componentType, propertyName));
            }
        }
        
        public static void SafeRemove(this List<SerializableComponentBlackList> list, Type componentType, string propertyName)
        {
            var s = list.FirstOrDefault(c => c.Type == componentType);

            if (s != null && s.componentNames.Contains(propertyName))
            {
                s.componentNames.Remove(propertyName);

                if (s.componentNames.Count == 0)
                {
                    list.Remove(s);
                }
            }
        }

        public static bool IsInBlackList(this List<SerializableComponentBlackList> list, Type componentType,
            string propertyName)
        {
            return list.Any(a => a.Type == componentType && a.componentNames.Contains(propertyName));
        }
    }
}