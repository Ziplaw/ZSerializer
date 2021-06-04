using System;
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

        

        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            instance = Resources.Load<ZSaverSettings>("ZSaverSettings");
        }
    }
}