using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

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

    public class PersisterManager
    {
        public Persister[] _persisters;


        public void Save()
        {
            string json = JsonHelper.ToJson(_persisters);
            Debug.Log(json);
            // WriteToFile("save.save", json);
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

        static void ReadFromFile(string fileName)
        {
            FileStream fs = new FileStream(GetFilePath(fileName), FileMode.Open);

        }

        static string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }

    [Serializable]
    public class Persister
    {
        public ZSaver[] objectsToPersist;
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