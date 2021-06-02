using System;
using UnityEngine;

namespace ZSaver
{
    // [CreateAssetMenu(fileName = "New ZSaver Settings", menuName = "ZSaverSettings", order = 0)]
    public class ZSaverSettings : ScriptableObject
    {
        public static ZSaverSettings instance;
        
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