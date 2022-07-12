using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSerializer
{
    public abstract class GlobalObject : ScriptableObject
    {
        public static T Get<T>() where T : GlobalObject
        {
            return (T)Get(typeof(T));
        }

        public static GlobalObject Get(Type globalDataType)
        {
            return Resources.Load<GlobalObject>(globalDataType.Name);
        }
    }
}
