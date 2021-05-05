using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
    public abstract class PersistentMonoBehaviour : MonoBehaviour
    {
        public static Dictionary<PersistentMonoBehaviour, int> persistentObjects = new Dictionary<PersistentMonoBehaviour, int>();

        private void Awake()
        {
            persistentObjects.Add(this, persistentObjects.Count);
        }

        void Save()
        {
            
        }

        void Load()
        {
            
        }

        void WriteToFile(string fileName, string json)
        {
            
        }

        void ReadFromFile(string fileName)
        {
            
        }

        string GetFilePath(string fileName)
        {
            return Application.persistentDataPath + "/" + fileName;
        }
    }
}