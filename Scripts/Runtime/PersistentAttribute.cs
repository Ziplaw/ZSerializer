using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    public class OmitSerializableCheck : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
        public readonly SaveType saveType;
        private readonly ExecutionCycle _executionCycle;

        public PersistentAttribute(ExecutionCycle dataRecovery)
        {
            saveType = SaveType.Component;
            _executionCycle = dataRecovery;
        }
    }
}