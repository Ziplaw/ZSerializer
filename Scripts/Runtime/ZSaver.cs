using System;
using System.Reflection;
using UnityEngine;
using ZSaver;
using Object = UnityEngine.Object;

namespace ZSaver
{
    [Serializable]
    public struct GameObjectData
    {
        public int loadingOrder;
        public HideFlags hideFlags;
        public string name;
        public bool active;
        public bool isStatic;
        public int layer;
        public string tag;

        public Vector3 position;
        public Quaternion rotation;
        public Vector3 size;

        public GameObject parent;

        public GameObject MakePerfectlyValidGameObject()
        {
            GameObject o = new GameObject();
            o.hideFlags = hideFlags;
            o.name = name;
            o.SetActive(active);
            o.isStatic = isStatic;
            o.layer = layer;
            o.tag = tag;

            o.transform.position = position;
            o.transform.rotation = rotation;
            o.transform.localScale = size;
            
            o.transform.SetParent(parent != null ? parent.transform : null);

            return o;
        }
    }
    public abstract class ZSaver<T> where T : Component
    {
        [OmitSerializableCheck] public int gameObjectInstanceID;
        [OmitSerializableCheck] public int componentinstanceID;
        [OmitSerializableCheck] public GameObject _componentParent;
        [OmitSerializableCheck] public T _component;

        public ZSaver(GameObject componentParent, T component)
        {
            _componentParent = componentParent;
            _component = component;
            gameObjectInstanceID = componentParent.GetInstanceID();
            componentinstanceID = component.GetInstanceID();
        }

        // public void LoadComponent(Type zSaverType)
        // {
        //     if (_component == null)
        //     {
        //         string prevCOMPInstanceID = componentinstanceID.ToString();
        //         string COMPInstanceIDToReplaceString = $"instanceID\":{prevCOMPInstanceID}";
        //
        //         if (_componentParent == null)
        //         {
        //             if (ZSaverSettings.instance.debugMode)
        //                 Debug.LogWarning(
        //                     $"GameObject holding {typeof(T)} was destroyed, add the Persistent GameObject component to said GameObject if persistence was intended");
        //             return;
        //         }
        //         
        //         _component = (T) _componentParent.AddComponent(typeof(T));
        //         componentinstanceID = _component.GetInstanceID();
        //         
        //         string newCOMPInstanceIDToReplaceString = "instanceID\":" + componentinstanceID;
        //         
        //         ZSave.UpdateAllJSONFiles(
        //             new[]
        //             {
        //                 COMPInstanceIDToReplaceString
        //             },
        //             new[]
        //             {
        //                 newCOMPInstanceIDToReplaceString
        //             });
        //     }
        //
        //
        //     FieldInfo[] zSaverFields = zSaverType.GetFields();
        //     FieldInfo[] componentFields = typeof(T).GetFields();
        //
        //     for (var i = 0; i < componentFields.Length; i++)
        //     {
        //         for (var j = 0; j < zSaverFields.Length; j++)
        //         {
        //             if (zSaverFields[j].Name == componentFields[i].Name)
        //             {
        //                 componentFields[i].SetValue(_component, zSaverFields[j].GetValue(this));
        //             }
        //         }
        //     }
        // }
    }
}
