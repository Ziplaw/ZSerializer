using System;

namespace ZSerializer
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SerializeGlobalDataAttribute : Attribute
    {
        public readonly GlobalDataType serializationType;

        public SerializeGlobalDataAttribute(GlobalDataType serializationType)
        {
            this.serializationType = serializationType;
        }
    }

    public enum GlobalDataType
    {
        PerSaveFile,
        Globally
    }
}
