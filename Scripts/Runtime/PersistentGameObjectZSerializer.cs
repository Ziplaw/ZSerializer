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
            var PersistentGameObjectInstance = ZSerialize.idMap[ZUID] as PersistentGameObject;
            var _componentParent = ZSerialize.idMap[GOZUID] as GameObject;
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
                position = _componentParent.transform.position,
                rotation = _componentParent.transform.rotation,
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
            gameObject.hideFlags = gameObjectData.hideFlags;
            gameObject.name = gameObjectData.name;
            gameObject.SetActive(gameObjectData.active);
            gameObject.isStatic = gameObjectData.isStatic;
            gameObject.layer = gameObjectData.layer;
            gameObject.tag = gameObjectData.tag;

            gameObject.transform.position = gameObjectData.position;
            gameObject.transform.rotation = gameObjectData.rotation;
            gameObject.transform.localScale = gameObjectData.size;

            gameObject.transform.SetParent(gameObjectData.parent != null ? gameObjectData.parent.transform : null);
            gameObject.transform.SetSiblingIndex(gameObjectData.loadingOrder.y);
        }
    }
}