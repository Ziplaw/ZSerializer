using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

namespace ZSave
{
    [Flags]
    public enum SaveCycle
    {
        OnStart = 0,
        OnAwake = 1,
        OnApplicationQuit = 2,
        None = 3
    }
    
    [Flags]
    public enum RetrieveCycle
    {
        OnStart = 0,
        OnAwake = 1,
        None = 3
    }

    public struct SaveAttributeInfo
    {
        public MemberInfo memberInfo;
        public SaveAttribute saveAttribute;
        public object instance;

        public SaveAttributeInfo(MemberInfo memberInfo, SaveAttribute saveAttribute, object instance)
        {
            this.memberInfo = memberInfo;
            this.saveAttribute = saveAttribute;
            this.instance = instance;
        }
    }


    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class SaveAttribute : Attribute
    {
        public int _saveID;
        public SaveCycle _saveCycle;
        public object _parent;

        [RuntimeInitializeOnLoadMethod]
        void Init()
        {
            
        }
        public SaveAttribute(int saveID, SaveCycle saveCycle)
        {
            _saveID = saveID;
            _saveCycle = saveCycle;
        }
    }
    
    class RetrieveOnAttribute : Attribute
    {
        // public RetrieveOnAttribute(object onStart)
        // {
        //     throw new NotImplementedException();
        // }
    }

    public class CycleSaver
    {
        private List<SaveAttributeInfo> _saveAttributeInfos;
        
        [RuntimeInitializeOnLoadMethod]
        public static void Init()
        {
            // Debug.Log(GetAllMarkedVariables().Count);

            // foreach (var attrInfo in _saveAttributeInfos)
            // {
            //     switch (attrInfo.memberInfo.MemberType)
            //     {
            //         case MemberTypes.Field:
            //             FieldInfo field = (FieldInfo) attrInfo.memberInfo;
            //             break;
            //     }
            //     
            // }
        }

        public static void RetrieveValue<T,T1>(ref T value, string name, T1 instance)
        {
            // List<SaveAttributeInfo> markedFields = new List<SaveAttributeInfo>();
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (var i = 0; i < assemblies.Length; i++)
            {
                foreach (var type in assemblies[i].GetTypes())
                {
                    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    MemberInfo[] members = type.GetMembers(flags);
                    
                    foreach (var member in members)
                    {
                        if (member.CustomAttributes.ToArray().Length > 0)
                        {
                            SaveAttribute attr = member.GetCustomAttribute<SaveAttribute>();
                            if (attr != null && member.Name == name)
                            {
                                // markedFields.Add(new SaveAttributeInfo(member,attr,null));
                                FieldInfo fieldInfo = (FieldInfo) member;
                                value = (T) fieldInfo.GetValue(instance);
                            }
                        }
                    }
                }
            }   
        }
        
        public static List<SaveAttributeInfo> GetAllMarkedVariables()
        {
            List<SaveAttributeInfo> markedFields = new List<SaveAttributeInfo>();
            
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();

            for (var i = 0; i < assemblies.Length; i++)
            {
                foreach (var type in assemblies[i].GetTypes())
                {
                    BindingFlags flags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
                    MemberInfo[] members = type.GetMembers(flags);
                    
                    foreach (var member in members)
                    {
                        if (member.CustomAttributes.ToArray().Length > 0)
                        {
                            SaveAttribute attr = member.GetCustomAttribute<SaveAttribute>();
                            if (attr != null)
                            {
                                markedFields.Add(new SaveAttributeInfo(member,attr,null));
                            }
                        }
                    }
                }
            }

            return markedFields;
        }
    }
}