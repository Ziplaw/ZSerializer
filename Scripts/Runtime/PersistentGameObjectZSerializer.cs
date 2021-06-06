[System.Serializable]
public class PersistentGameObjectZSerializer : ZSaver.ZSerializer<PersistentGameObject> {
    public System.Boolean enabled;
    public UnityEngine.HideFlags hideFlags;
    public System.Collections.Generic.List<PersistentGameObject.SerializableComponentData> _componentDatas;
    public ZSaver.GameObjectData gameObjectData;
    public PersistentGameObjectZSerializer (PersistentGameObject PersistentGameObjectInstance) : base(PersistentGameObjectInstance.gameObject, PersistentGameObjectInstance ) {
        enabled = PersistentGameObjectInstance.enabled;
        hideFlags = PersistentGameObjectInstance.hideFlags;
        _componentDatas = PersistentGameObjectInstance._componentDatas;
        gameObjectData =new ZSaver.GameObjectData()
        {
            loadingOrder = PersistentGameObject.CountParents(PersistentGameObjectInstance.transform),
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