using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ZSaver
{
    // [CreateAssetMenu(fileName = "New ZSerializer Settings", menuName = "ZSaverSettings", order = 0)]
    public class ZSaverSettings : ScriptableObject
    {
        private static ZSaverSettings instance;
        
        public static ZSaverSettings Instance => instance ? instance : Resources.Load<ZSaverSettings>("ZSaverSettings");

        public bool debugMode;
        public bool autoRebuildZSavers;
        public int selectedSaveFile;
        public bool encryptData;
        public string[] addedAssemblyNames = new []{ZSave.mainAssembly};

        public IEnumerable<Assembly> AddedAssemblies => from assemblyName in addedAssemblyNames
            select Assembly.Load(assemblyName);

        

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            instance = Resources.Load<ZSaverSettings>("ZSaverSettings");
        }
    }
}