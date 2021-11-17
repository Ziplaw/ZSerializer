using System;
using System.Reflection;
using UnityEngine;
using ZSerializer;
using Object = UnityEngine.Object;

namespace ZSerializer.Internal
{
    public abstract class ZSerializer
    {
        [NonZSerialized] public string ZUID;
        [NonZSerialized] public string GOZUID;

        public ZSerializer(string ZUID, string GOZUID)
        {
            this.ZUID = ZUID;
            this.GOZUID = GOZUID;
        }

        public abstract void RestoreValues(Component component);
    }
}
