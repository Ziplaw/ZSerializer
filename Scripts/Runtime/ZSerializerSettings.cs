using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ZSerializer
{
    // [CreateAssetMenu(fileName = "New ZSerializer Settings", menuName = "ZSerializerSettings", order = 0)]
    [Serializable]
    public class SerializableComponentBlackList
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

    public class ZSerializerSettings : ScriptableObject
    {
        private static ZSerializerSettings instance;
        public static ZSerializerSettings Instance => instance ? instance : Resources.Load<ZSerializerSettings>("ZSerializerSettings");


        [HideInInspector] public bool packageInitialized;
        public bool debugMode;
        public bool autoRebuildZSerializers;
        public int selectedSaveFile;
        public bool encryptData;
        public bool stableSave;
        public bool advancedSerialization;
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

        [HideInInspector]public SerializableDictionary defaultOnDictionary = new SerializableDictionary();

        public bool GetDefaultOnValue<T>() where T : PersistentMonoBehaviour
        {
            if(!defaultOnDictionary.ContainsKey(typeof(T))) defaultOnDictionary.Add(typeof(T),true);
            return defaultOnDictionary.GetElementAt(typeof(T));
        }
        
        public bool GetDefaultOnValue(Type type)
        {
            if(!defaultOnDictionary.ContainsKey(type)) defaultOnDictionary.Add(type,true);
            return defaultOnDictionary.GetElementAt(type);
        }
        
        public void SetDefaultOnValue<T>(bool value) where T : PersistentMonoBehaviour
        {
            if (!defaultOnDictionary.ContainsKey(typeof(T))) defaultOnDictionary.Add(typeof(T), value);
            else defaultOnDictionary.SetElementAt(typeof(T), value);
        }
        
        public void SetDefaultOnValue(Type type, bool value)
        {
            if(!defaultOnDictionary.ContainsKey(type)) defaultOnDictionary.Add(type,value);
            else defaultOnDictionary.SetElementAt(type, value);
        }

        [Serializable]
        public class SerializableDictionary
        {
            public List<string> keyList = new List<string>();
            public List<bool> valueList = new List<bool>();

            public bool ContainsKey(Type type)
            {
                return keyList.Contains(type.AssemblyQualifiedName);
            }

            public void Add(Type key, bool value)
            {
                keyList.Add(key.AssemblyQualifiedName);
                valueList.Add(value);
            }

            public bool GetElementAt(Type key)
            {
                return valueList[keyList.IndexOf(key.AssemblyQualifiedName)];
            }
            
            public bool SetElementAt(Type key, bool value)
            {
                return valueList[keyList.IndexOf(key.AssemblyQualifiedName)] = value;
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