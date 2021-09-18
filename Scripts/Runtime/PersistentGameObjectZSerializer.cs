using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

[System.Serializable]
public class PersistentGameObjectZSerializer : ZSerializer.ZSerializer<PersistentGameObject>
{
    public int groupID;
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public ZSerializer.GameObjectData gameObjectData;
    public List<SerializedComponent> serializedComponents;
    public PersistentGameObjectZSerializer (PersistentGameObject PersistentGameObjectInstance) : base(PersistentGameObjectInstance.gameObject, PersistentGameObjectInstance ) {
        enabled = PersistentGameObjectInstance.enabled;
        hideFlags = PersistentGameObjectInstance.hideFlags;
        serializedComponents = PersistentGameObjectInstance.serializedComponents;
        groupID = (int) typeof(PersistentGameObject).GetField("groupID", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(PersistentGameObjectInstance);
        
        gameObjectData =new ZSerializer.GameObjectData()
        {
            loadingOrder = new Vector2Int(PersistentGameObject.CountParents(PersistentGameObjectInstance.transform),PersistentGameObjectInstance.transform.GetSiblingIndex()),
            active = _componentParent.activeSelf,
            hideFlags = _componentParent.hideFlags,
            isStatic = _componentParent.isStatic,
            layer = PersistentGameObjectInstance.gameObject.layer,
            name = _componentParent.name,
            position = _componentParent.transform.position,
            rotation = _componentParent.transform.rotation,
            size = _componentParent.transform.localScale,
            tag = PersistentGameObjectInstance.gameObject.tag,
            parent = PersistentGameObjectInstance.transform.parent ? PersistentGameObjectInstance.transform.parent.gameObject : null
        };
    }
}