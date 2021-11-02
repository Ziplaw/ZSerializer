using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ZSerializer;

namespace ZSerializer
{
    [System.Serializable]
    public sealed class PersistentGameObjectZSerializer : ZSerializer.Internal.ZSerializer<PersistentGameObject>
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
            this.ZUID = ZUID;
            this.GOZUID = GOZUID;

            gameObjectData = new GameObjectData()
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
                parent = PersistentGameObjectInstance.transform.parent
                    ? _componentParent.gameObject
                    : null
            };
        }

        public override void RestoreValues(PersistentGameObject component)
        {
            component.GroupID = groupID;
            component.ZUID = ZUID;
            component.GOZUID = GOZUID;
            component.enabled = enabled;
            component.hideFlags = hideFlags;
            component.serializedComponents = serializedComponents;

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