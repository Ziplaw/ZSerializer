using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ZSerializer
{

    public class NonZSerialized : Attribute
    {
    }

    // [AttributeUsage(AttributeTargets.Class)]
    public class PersistentMonoBehaviour : MonoBehaviour
    {
        public virtual void OnPreSave(){}
        public virtual void OnPostSave(){}
        public virtual void OnPreLoad(){}
        public virtual void OnPostLoad(){}
    }
}