using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;
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

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
        [RuntimeInitializeOnLoadMethod]
        public static void SaveAllObjects()
        {
            var types = PersistanceManager.GetTypesWithPersistentAttribute(Assembly.GetAssembly(typeof(Testing)));

            foreach (var type in types)
            {
                var objects = Object.FindObjectsOfType(type);

                var ZSaverType = Type.GetType(type.Name + "ZSaver");
                var ZSaverArrayType = ZSaverType.MakeArrayType();

                var zsaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {objects.Length});

                object[] zsavers = (object[]) zsaversArray;
                
                for (var i = 0; i < zsavers.Length; i++)
                {
                    zsavers[i] = Activator.CreateInstance(ZSaverType, new object[] { objects[i] });
                }

                var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
                var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
                genericSaveMethodInfo.Invoke(null,new object[] {zsavers});
            }
        }
    }
    
    public class PersistanceManager
    {
        public static void Save<T>(T[] objectsToPersist)
        {
            string json = JsonHelper.ToJson(objectsToPersist);
            Debug.Log(json);
            WriteToFile("save.save", json);
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
        
        public static IEnumerable<Type> GetTypesWithPersistentAttribute(Assembly assembly) {
            foreach(Type type in assembly.GetTypes()) {
                if (type.GetCustomAttributes(typeof(PersistentAttribute), true).Length > 0) {
                    yield return type;
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