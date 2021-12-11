using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using UnityEngine.Events;
using ZSerializer;

namespace ZSerializer
{
    [System.Serializable]
    public sealed class PersistentGameObjectZSerializer : Internal.ZSerializer
    {
        public int groupID;
        public System.Boolean enabled;
        public HideFlags hideFlags;
        public GameObjectData gameObjectData;
        public List<SerializedComponent> serializedComponents;

        public PersistentGameObject.PersistentEvent onPreSave;
        public PersistentGameObject.PersistentEvent onPostSave;
        public PersistentGameObject.PersistentEvent onPreLoad;
        public PersistentGameObject.PersistentEvent onPostLoad;

        public PersistentGameObjectZSerializer(string ZUID, string GOZUID) : base(
            ZUID, GOZUID)
        {
            var PersistentGameObjectInstance =
                ZSerialize.idMap[ZSerialize.CurrentGroupID][ZUID] as PersistentGameObject;
            var _componentParent = ZSerialize.idMap[ZSerialize.CurrentGroupID][GOZUID] as GameObject;
            enabled = PersistentGameObjectInstance.enabled;
            hideFlags = PersistentGameObjectInstance.hideFlags;
            serializedComponents = PersistentGameObjectInstance.serializedComponents;
            groupID = PersistentGameObjectInstance.GroupID;
            onPreSave = PersistentGameObjectInstance.onPreSave;
            onPostSave = PersistentGameObjectInstance.onPostSave;
            onPreLoad = PersistentGameObjectInstance.onPreLoad;
            onPostLoad = PersistentGameObjectInstance.onPostLoad;

            var parent = PersistentGameObjectInstance.transform.parent;
            gameObjectData = new GameObjectData
            {
                loadingOrder = new Vector2Int(PersistentGameObject.CountParents(PersistentGameObjectInstance.transform),
                    PersistentGameObjectInstance.transform.GetSiblingIndex()),
                active = _componentParent.activeSelf,
                hideFlags = _componentParent.hideFlags,
                isStatic = _componentParent.isStatic,
                layer = PersistentGameObjectInstance.gameObject.layer,
                name = _componentParent.name,
                localPosition = _componentParent.transform.localPosition,
                localRotation = _componentParent.transform.localRotation,
                size = _componentParent.transform.localScale,
                tag = _componentParent.tag,
                parent = parent
                    ? parent.gameObject
                    : null
            };
        }
        
        // static readonly FieldInfo persistentCallsFieldInfo = typeof(UnityEventBase).GetField("m_PersistentCalls", (BindingFlags)(-1));
        // static readonly Type persistentCallGroupType = Type.GetType(
        //     "UnityEngine.Events.PersistentCallGroup, UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        // static readonly Type persistentCallType = Type.GetType(
        //     "UnityEngine.Events.PersistentCall, UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
        // static readonly FieldInfo callsFieldInfo = persistentCallGroupType.GetField("m_Calls", (BindingFlags)(-1));
        // static readonly FieldInfo targetFieldInfo = persistentCallType.GetField("m_Target", (BindingFlags)(-1));
        

        public override void RestoreValues(Component component)
        {
            var persistentComponent = component as PersistentGameObject;
            persistentComponent.GroupID = groupID;
            persistentComponent.ZUID = ZUID;
            persistentComponent.GOZUID = GOZUID;
            persistentComponent.enabled = enabled;
            persistentComponent.hideFlags = hideFlags;
            // persistentComponent.serializedComponents = serializedComponents;
            

            var gameObject = component.gameObject;
            gameObject.ApplyValues(gameObjectData);

            // foreach (var pgSerializedComponent in new List<SerializedComponent>(serializedComponents))
            // {
            //     if (pgSerializedComponent.component == null &&
            //         (pgSerializedComponent.persistenceType != PersistentType.None ||
            //          !ZSerializerSettings.Instance.advancedSerialization))
            //     {
            //         Component addedComponent;
            //         if (pgSerializedComponent.Type == typeof(Transform)) addedComponent = gameObject.transform;
            //         else addedComponent = gameObject.AddComponent(pgSerializedComponent.Type);
            //
            //         serializedComponents[serializedComponents.IndexOf(pgSerializedComponent)].component =
            //             addedComponent;
            //         ZSerialize.idMap[ZSerialize.CurrentGroupID][pgSerializedComponent.zuid] = addedComponent;
            //     }
            // }

            // for (int i = 0; i < onPreSave.GetPersistentEventCount(); i++)
            // {
            //     foreach (var persistentCall in callsFieldInfo.GetValue(persistentCallsFieldInfo.GetValue(onPreSave)) as IEnumerable)
            //     {
            //         targetFieldInfo.SetValue(persistentCall, ZSerialize.idMap[ZSerialize.CurrentGroupID][onPreSave.targetZUIDs[i]]);
            //     }
            // }
            //
            // for (int i = 0; i < onPostSave.GetPersistentEventCount(); i++)
            // {
            //     foreach (var persistentCall in callsFieldInfo.GetValue(persistentCallsFieldInfo.GetValue(onPostSave)) as IEnumerable)
            //     {
            //         targetFieldInfo.SetValue(persistentCall, ZSerialize.idMap[ZSerialize.CurrentGroupID][onPostSave.targetZUIDs[i]]);
            //     }
            // }
            //
            // for (int i = 0; i < onPreLoad.GetPersistentEventCount(); i++)
            // {
            //     foreach (var persistentCall in callsFieldInfo.GetValue(persistentCallsFieldInfo.GetValue(onPreLoad)) as IEnumerable)
            //     {
            //         targetFieldInfo.SetValue(persistentCall, ZSerialize.idMap[ZSerialize.CurrentGroupID][onPreLoad.targetZUIDs[i]]);
            //     }
            // }
            //
            // for (int i = 0; i < onPostLoad.GetPersistentEventCount(); i++)
            // {
            //     foreach (var persistentCall in callsFieldInfo.GetValue(persistentCallsFieldInfo.GetValue(onPostLoad)) as IEnumerable)
            //     {
            //         targetFieldInfo.SetValue(persistentCall, ZSerialize.idMap[ZSerialize.CurrentGroupID][onPostLoad.targetZUIDs[i]]);
            //     }
            // }
            persistentComponent.onPreSave = onPreSave;
            persistentComponent.onPostSave = onPostSave;
            persistentComponent.onPreLoad = onPreLoad;
            persistentComponent.onPostLoad = onPostLoad;
        }
    }
}