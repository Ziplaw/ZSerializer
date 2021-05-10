using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Assertions;
using Object = UnityEngine.Object;

namespace ZSave
{
    [Flags]
    public enum ExecutionCycle
    {
        OnStart = 0,
        OnAwake = 1,
        OnApplicationQuit = 2,
        None = 3
    }

    public enum SaveType
    {
        Component,
        GameObject
    }

    public class ZSaver<T>
    {
        public GameObject _componentParent;
        public T _component;

        public ZSaver(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
        }
        public void Load(Type zSaverType, SaveType saveType)
        {
            Debug.Log(typeof(T) + " " + saveType);

            if (!_componentParent)
            {
                if (saveType != SaveType.GameObject)
                {
                    Debug.LogWarning($"GameObject holding {typeof(T)} was destroyed, change the saving type to \"SaveType.GameObject\" to ensure persistance of this Component ");
                    return;
                }
                
                // LoadGameObjectPrefab()
            }
            
            // if()
            

            FieldInfo[] zSaverFields = zSaverType.GetFields();
            FieldInfo[] componentFields = typeof(T).GetFields();

            for (var i = 0; i < componentFields.Length; i++)
            {
                for (var j = 0; j < zSaverFields.Length; j++)
                {
                    if (zSaverFields[j].Name == componentFields[i].Name)
                    {
                        // object obj = GetType().GetField("_" + typeof(T).Name.ToLower()).GetValue(this);
                        componentFields[i].SetValue(_component,zSaverFields[j].GetValue(this));
                    }
                }
            }
        }
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
        private readonly SaveType _saveType;
        private readonly ExecutionCycle _executionCycle;

        public PersistentAttribute(SaveType saveType, ExecutionCycle dataRecovery)
        {
            _saveType = saveType;
            _executionCycle = dataRecovery;
        }

        public static void SaveAllObjects()
        {
            var types = PersistanceManager.GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);

                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                var ZSaverArrayType = ZSaverType.MakeArrayType();

                var zsaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {objects.Length});

                object[] zsavers = (object[]) zsaversArray;

                for (var i = 0; i < zsavers.Length; i++)
                {
                    zsavers[i] = Activator.CreateInstance(ZSaverType, new object[] {objects[i]});
                }

                var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
                var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
                genericSaveMethodInfo.Invoke(null, new object[] {zsavers, type.Name + ".save"});
            }

            LoadAllObjects();
        }

        [RuntimeInitializeOnLoadMethod]
        public static void LoadAllObjects()
        {
            var types = PersistanceManager.GetTypesWithPersistentAttribute(AppDomain.CurrentDomain.GetAssemblies());

            foreach (var type in types)
            {
                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                MethodInfo fromJsonMethod = typeof(JsonHelper).GetMethod(nameof(JsonHelper.FromJson))
                    .MakeGenericMethod(ZSaverType);

                object[] FromJSONdObjects = (object[]) fromJsonMethod.Invoke(null,
                    new object[] {PersistanceManager.ReadFromFile(type.Name + ".save")});

                for (var i = 0; i < FromJSONdObjects.Length; i++)
                {
                    PersistentAttribute attr = (PersistentAttribute)FromJSONdObjects[i].GetType().GetField("_component").GetValue(FromJSONdObjects[i]).GetType().GetCustomAttributes(typeof(PersistentAttribute)).FirstOrDefault(); 
                    Debug.Log(attr);
                    
                    MethodInfo LoadMethod = ZSaverType.GetMethod("Load");
                    LoadMethod.Invoke(FromJSONdObjects[i], new object[] { ZSaverType, attr._saveType });
                }
            }
        }
    }

    public class PersistanceManager
    {
        public static void Save<T>(T[] objectsToPersist, string fileName)
        {
            string json = JsonHelper.ToJson(objectsToPersist);
            Debug.Log(json);
            WriteToFile(fileName, json);
        }

        public static void Load()
        {
        }

        static void WriteToFile(string fileName, string json)
        {
            // FileStream fs = new FileStream(GetFilePath(fileName),FileMode.OpenOrCreate);
            StreamWriter writer = new StreamWriter(GetFilePath(fileName));
            writer.WriteLine(json);
            writer.Close();
            // fs.Close();
        }

        public static string ReadFromFile(string fileName)
        {
            // FileStream fs = new FileStream(GetFilePath(fileName), FileMode.Open);
            StreamReader reader = new StreamReader(GetFilePath(fileName));
            string json = reader.ReadLine();
            reader.Close();
            return json;
        }

        static string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }

        public static IEnumerable<Type> GetTypesWithPersistentAttribute(Assembly[] assemblies)
        {
            foreach (var assembly in assemblies)
            {
                foreach (Type type in assembly.GetTypes())
                {
                    if (type.GetCustomAttributes(typeof(PersistentAttribute), true).Length > 0)
                    {
                        yield return type;
                    }
                }
            }
        }
    }

    [Serializable]
    public class Persister<T>
    {
        public T[] objectsToPersist;

        public Persister(T[] objectsToPersist)
        {
            this.objectsToPersist = objectsToPersist;
        }
    }

    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
            return wrapper.Items;
        }

        public static string ToJson<T>(T[] array, bool prettyPrint = false)
        {
            Wrapper<T> wrapper = new Wrapper<T>();
            wrapper.Items = array;
            return JsonUtility.ToJson(wrapper, prettyPrint);
        }

        [Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}