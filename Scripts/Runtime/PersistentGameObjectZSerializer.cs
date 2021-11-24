using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
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

        public PersistentGameObjectZSerializer(string ZUID, string GOZUID) : base(
            ZUID, GOZUID)
        {
            var PersistentGameObjectInstance = ZSerialize.idMap[ZSerialize.CurrentGroupID][ZUID] as PersistentGameObject;
            var _componentParent = ZSerialize.idMap[ZSerialize.CurrentGroupID][GOZUID] as GameObject;
            enabled = PersistentGameObjectInstance.enabled;
            hideFlags = PersistentGameObjectInstance.hideFlags;
            serializedComponents = PersistentGameObjectInstance.serializedComponents;
            groupID = PersistentGameObjectInstance.GroupID;

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

        public override void RestoreValues(Component component)
        {
            var persistentComponent = component as PersistentGameObject;
            persistentComponent.GroupID = groupID;
            persistentComponent.ZUID = ZUID;
            persistentComponent.GOZUID = GOZUID;
            persistentComponent.enabled = enabled;
            persistentComponent.hideFlags = hideFlags;
            persistentComponent.serializedComponents = serializedComponents;

            var gameObject = component.gameObject;
            gameObject.ApplyValues(gameObjectData);
            
            foreach (var pgSerializedComponent in new List<SerializedComponent>(serializedComponents))
            {
                if (pgSerializedComponent.component == null &&
                    (pgSerializedComponent.persistenceType != PersistentType.None ||
                     !ZSerializerSettings.Instance.advancedSerialization))
                {
                    var addedComponent = gameObject.AddComponent(pgSerializedComponent.Type);
                    serializedComponents[serializedComponents.IndexOf(pgSerializedComponent)].component =
                        addedComponent;
                    ZSerialize.idMap[ZSerialize.CurrentGroupID][pgSerializedComponent.zuid] = addedComponent;
                }
            }
        }
    }
}