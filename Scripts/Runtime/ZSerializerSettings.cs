using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.Video;

namespace ZSerializer
{
    public enum SerializationType
    {
        Sync,
        Async
    }

    // [CreateAssetMenu(fileName = "New ZSerializer Settings", menuName = "ZSerializerSettings", order = 0)]
    [Serializable]
    public sealed class UnityComponentData
    {
        [Serializable]
        public struct CustomVariableEntry
        {
            public string variableType;
            public string variableName;

            public CustomVariableEntry(string variableType, string variableName)
            {
                this.variableType = variableType;
                this.variableName = variableName;
            }
        }

        public Type Type => Type.GetType(typeFullName);
        [SerializeField] private string typeFullName;
        public List<string> componentNames = new List<string>();
        public List<CustomVariableEntry> customVariableEntries = new List<CustomVariableEntry>();
        public UnityEvent<ZSerializer.Internal.ZSerializer, Component> OnSerialize;
        public UnityEvent<ZSerializer.Internal.ZSerializer, Component> OnDeserialize;

        public UnityComponentData(Type type, string componentName)
        {
            typeFullName = type.AssemblyQualifiedName;
            componentNames = new List<string> {componentName};
        }
        
        public UnityComponentData(Type type, CustomVariableEntry customVariableEntry)
        {
            typeFullName = type.AssemblyQualifiedName;
            customVariableEntries = new List<CustomVariableEntry> {customVariableEntry};
        }
    }

    [Serializable]
    public sealed class SceneGroup
    {
        public string name;
        public List<string> scenePaths = new List<string>();
    }
    
    public enum DebugMode {Off = 0, Informational = 1, Developer = 2}


    public sealed class ZSerializerSettings : ScriptableObject
    {
        private static ZSerializerSettings instance;

        public static ZSerializerSettings Instance =>
            instance ? instance : Resources.Load<ZSerializerSettings>("ZSerializerSettings");
        

        [HideInInspector] public bool packageInitialized;
        public DebugMode debugMode;
        public bool autoRebuildSerializers;
        public int selectedSaveFile;
        public bool encryptData;
        public bool advancedSerialization;
        public SerializationType serializationType = SerializationType.Sync;
        public int maxBatchCount = 50;
        [HideInInspector] public List<SceneGroup> sceneGroups;
        [HideInInspector] public List<string> unityComponentTypes;

        public void DefaultComponentData()
        {
            unityComponentDataList.Clear();
            unityComponentDataList.SafeAdd(typeof(NavMeshAgent), "path");
            unityComponentDataList.SafeAdd(typeof(ParticleSystem), "randomSeed");
            unityComponentDataList.SafeAdd(typeof(ParticleSystem), "useAutoRandomSeed");
            unityComponentDataList.SafeAdd(typeof(VideoPlayer), "url");
            unityComponentDataList.SafeAdd(typeof(HingeJoint2D), new UnityComponentData.CustomVariableEntry("Vector2","serializableLimits" ));
            unityComponentDataList.SafeAdd(typeof(HingeJoint2D), new UnityComponentData.CustomVariableEntry("Vector2","serializableMotor" ));
        }

        [FormerlySerializedAs("componentBlackList")] [HideInInspector] public List<UnityComponentData> unityComponentDataList = new List<UnityComponentData>();

        [HideInInspector] public List<string> saveGroups = new List<string>
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

        [HideInInspector] public ComponentDataDictionary componentDataDictionary = new ComponentDataDictionary();
        

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
        public static void SafeAdd(this List<UnityComponentData> list, Type componentType,
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
                    new UnityComponentData(componentType, propertyName));
            }
        }

        public static void SafeAdd(this List<UnityComponentData> list, Type componentType,
            UnityComponentData.CustomVariableEntry customVariableEntry)
        {
            var s = list.FirstOrDefault(c => c.Type == componentType);

            if (s != null)
            {
                s.customVariableEntries.Add(customVariableEntry);
            }
            else
            {
                list.Add(new UnityComponentData(componentType, customVariableEntry));
            }
        }

        public static void SafeRemove(this List<UnityComponentData> list, Type componentType,
            string propertyName)
        {
            var s = list.FirstOrDefault(c => c.Type == componentType);

            if (s != null && s.componentNames.Contains(propertyName))
            {
                s.componentNames.Remove(propertyName);

                if (s.componentNames.Count == 0 && s.customVariableEntries.Count == 0)
                {
                    list.Remove(s);
                }
            }
        }
        
        public static void SafeRemove(this List<UnityComponentData> list, Type componentType,
            int customEntryIndex)
        {
            var s = list.FirstOrDefault(c => c.Type == componentType);

            if (s != null && s.customVariableEntries.Count > customEntryIndex)
            {
                s.customVariableEntries.RemoveAt(customEntryIndex);

                if (s.componentNames.Count == 0 && s.customVariableEntries.Count == 0)
                {
                    list.Remove(s);
                }
            }
        }

        public static bool IsInBlackList(this List<UnityComponentData> list, Type componentType,
            string propertyName)
        {
            return list.Any(a => a.Type == componentType && a.componentNames != null && a.componentNames.Contains(propertyName));
        }
    }
}