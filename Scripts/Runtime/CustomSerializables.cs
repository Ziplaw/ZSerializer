using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ZSerializer;

public static class CustomSerializables
{
    private static Dictionary<Type, Type> _map;

    public static Dictionary<Type, Type> Map
    {
        get
        {
            if (_map == null)
            {
                _map = new Dictionary<Type, Type>();
                foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(ass => ass.GetTypes()))
                {
                    var _interface = type.GetInterface(typeof(ICustomSerialize<>).FullName);
                    
                    if (_interface != null)
                    {
                        Type genericDefinedType = type.IsGenericType ? type.GetGenericTypeDefinition() : type;
                        _map.Add(_interface.GetGenericArguments()[0].GetGenericTypeDefinition(), genericDefinedType);
                    }
                }
            }

            return _map;
        }
    }

    public static Type ParseFieldType(Type type)
    {
        if (!type.IsGenericType) return type;
        
        var genericTypeDefinition = type.GetGenericTypeDefinition();

        return Map.ContainsKey(genericTypeDefinition) ? Map[genericTypeDefinition].MakeGenericType(type.GetGenericArguments()) : type;
    }
}

namespace ZSerializer
{
    public interface ICustomSerialize<T> { }

    #if UNITY_2021_3_OR_NEWER
    
    [Serializable]
    public class ZDictionary<TK, TV> : ICustomSerialize<Dictionary<TK,TV>>
    {
        public List<TK> keys = new List<TK>();
        public List<TV> values = new List<TV>();

        public static implicit operator Dictionary<TK, TV>(ZDictionary<TK, TV> dictionary)
        {
            var result = new Dictionary<TK, TV>();

            for (var i = 0; i < dictionary.keys.Count; i++)
            {
                result.Add(dictionary.keys[i], dictionary.values[i]);
            }

            return result;
        }

        public static implicit operator ZDictionary<TK, TV>(Dictionary<TK, TV> dictionary)
        {
            var result = new ZDictionary<TK, TV>

            {
                keys = dictionary.Keys.ToList(),
                values = dictionary.Values.ToList()
            };

            return result;
        }
    }

    [Serializable]
    public class ZQueue<T> : ICustomSerialize<Queue<T>>
    {
        public List<T> backingField = new List<T>();

        public static implicit operator Queue<T>(ZQueue<T> _queue)
        {
            var result = new Queue<T>();
            
            foreach (var item in _queue.backingField)
            {
                result.Enqueue(item);
            }

            return result;
        }

        public static implicit operator ZQueue<T>(Queue<T> _queue)
        {
            var result = new ZQueue<T>();
            var copy = new Queue<T>(_queue);

            while (copy.TryDequeue(out var item))
            {
                result.backingField.Add(item);
            }

            return result;
        }
    }

    [Serializable]
    public class ZStack<T> : ICustomSerialize<Stack<T>>
    {
        public List<T> backingField = new List<T>();

        public static implicit operator Stack<T>(ZStack<T> _stack)
        {
            var result = new Stack<T>();

            foreach (var item in _stack.backingField)
            {
                result.Push(item);
            }

            return result;
        }

        public static implicit operator ZStack<T>(Stack<T> _stack)
        {
            var result = new ZStack<T>();
            var copy = new Stack<T>(_stack);

            while (copy.TryPop(out var item))
            {
                result.backingField.Add(item);
            }

            return result;
        }
    }
#endif
}