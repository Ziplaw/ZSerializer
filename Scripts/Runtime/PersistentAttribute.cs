using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Object = UnityEngine.Object;

namespace ZSaver
{

    public class OmitSerializableCheck : Attribute
    {
    }

    [AttributeUsage(AttributeTargets.Class)]
    public class PersistentAttribute : Attribute
    {
        
    }
}