using System;
using UnityEngine;

namespace ZSave
{
    // [CreateAssetMenu(fileName = "New ZSaver Settings", menuName = "ZSaverSettings", order = 0)]
    public class ZSaverSettings : ScriptableObject
    {
        public static ZSaverSettings instance;
        
        public bool debugMode;

        private void OnEnable()
        {
            instance = instance ? instance : this;
        }
    }
}