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

        #if ZSAVE_INSTALLED_AS_PACKAGE//
        [HideInInspector] 
        #endif
        public bool packageInitialized;
        public bool debugMode;
        public bool autoRebuildZSavers;
        public int selectedSaveFile;
        public bool encryptData;
        public string[] addedAssemblyNames;

        public IEnumerable<Assembly> AddedAssemblies =>
            new string[] {"com.Ziplaw.ZSaver.Runtime", "Assembly-CSharp", "UnityEngine.CoreModule"}
                .Select(Assembly.Load).Concat(addedAssemblyNames.Select(Assembly.Load)).Distinct();


        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            instance = Resources.Load<ZSaverSettings>("ZSaverSettings");
        }
    }
}