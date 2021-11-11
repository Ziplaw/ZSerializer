using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZSerializer
{
    public enum SerializationType {Sync, Async}
    
    // [CreateAssetMenu(fileName = "New ZSerializer Settings", menuName = "ZSerializerSettings", order = 0)]
    [Serializable]
    public sealed class SerializableComponentBlackList
    {
        public Type Type => Type.GetType(typeFullName);
        [SerializeField] private string typeFullName;
        public List<string> componentNames;

        public SerializableComponentBlackList(Type type, string componentName)
        {
            typeFullName = type.AssemblyQualifiedName;
            componentNames = new List<string> { componentName };
        }
    }

    [Serializable]
    public sealed class SceneGroup
    {
        public string name;
        public string loadingManagementScenePath;
        public List<string> scenePaths = new List<string>();
    }

    public sealed class ZSerializerSettings : ScriptableObject
    {
        private static ZSerializerSettings instance;
        public static ZSerializerSettings Instance => instance ? instance : Resources.Load<ZSerializerSettings>("ZSerializerSettings");


        [HideInInspector] public bool packageInitialized;
        public bool debugMode;
        public bool autoRebuildZSerializers;
        public int selectedSaveFile;
        public bool encryptData;
        public bool advancedSerialization;
        public SerializationType serializationType = SerializationType.Sync;
        public int maxBatchCount = 50;
        [HideInInspector] public List<SceneGroup> sceneGroups;
        [HideInInspector] public List<SerializableComponentBlackList> componentBlackList;

        [HideInInspector] public List<string> saveGroups = new List<string>()
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

        internal ComponentDataDictionary componentDataDictionary = new ComponentDataDictionary();

        
        [Serializable]
        public sealed class ComponentDataDictionary
        {
            public PersistentComponentTypeDataDictionary typeDatas = new PersistentComponentTypeDataDictionary();
            
            public PersistentComponentTypeData this[Type t] => typeDatas[t];
        }

        [Serializable]
        public sealed class PersistentComponentTypeDataDictionary
        {
            public List<string> keys = new List<string>();
            public List<PersistentComponentTypeData> values = new List<PersistentComponentTypeData>();

            public PersistentComponentTypeData this[Type t]
            {
                get
                {
                    if (keys.Contains(t.AssemblyQualifiedName)) return values[keys.IndexOf(t.AssemblyQualifiedName)];
                    keys.Add(t.AssemblyQualifiedName);
                    values.Add(new PersistentComponentTypeData());
                    return values[keys.IndexOf(t.AssemblyQualifiedName)];
                }
            }
        }

        [Serializable]
        public sealed class PersistentComponentTypeData
        {
            public bool isOn;
            public int groupID;
            
            public PersistentComponentTypeData()
            {
                isOn = true;
                groupID = 0;
            }
        } 
        
        
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            instance = Resources.Load<ZSerializerSettings>("ZSerializerSettings");
        }

        
    }

    public static class Extensions
    {
        public static void SafeAdd(this List<SerializableComponentBlackList> list, Type componentType,
            string propertyName)
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

        public static void SafeRemove(this List<SerializableComponentBlackList> list, Type componentType,
            string propertyName)
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