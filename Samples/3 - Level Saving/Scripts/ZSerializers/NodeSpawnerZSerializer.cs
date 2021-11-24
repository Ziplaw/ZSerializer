[System.Serializable]
public sealed class NodeSpawnerZSerializer : ZSerializer.Internal.ZSerializer
{
    public UnityEngine.GameObject roadPrefab;
    public UnityEngine.GameObject anchorPrefab;
    public UnityEngine.Transform currentInstancedNode;
    public UnityEngine.Transform lastPlacedNode;
    public Anchor lastPlacedAnchor;
    public UIManager uiManager;
    public System.Collections.Generic.List<NodeSpawner.TransformData> initialTransformDatas;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public NodeSpawnerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         roadPrefab = (UnityEngine.GameObject)typeof(NodeSpawner).GetField("roadPrefab").GetValue(instance);
         anchorPrefab = (UnityEngine.GameObject)typeof(NodeSpawner).GetField("anchorPrefab").GetValue(instance);
         currentInstancedNode = (UnityEngine.Transform)typeof(NodeSpawner).GetField("currentInstancedNode").GetValue(instance);
         lastPlacedNode = (UnityEngine.Transform)typeof(NodeSpawner).GetField("lastPlacedNode").GetValue(instance);
         lastPlacedAnchor = (Anchor)typeof(NodeSpawner).GetField("lastPlacedAnchor").GetValue(instance);
         uiManager = (UIManager)typeof(NodeSpawner).GetField("uiManager").GetValue(instance);
         initialTransformDatas = (System.Collections.Generic.List<NodeSpawner.TransformData>)typeof(NodeSpawner).GetField("initialTransformDatas").GetValue(instance);
         groupID = (System.Int32)typeof(NodeSpawner).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(NodeSpawner).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(NodeSpawner).GetField("roadPrefab").SetValue(component, roadPrefab);
         typeof(NodeSpawner).GetField("anchorPrefab").SetValue(component, anchorPrefab);
         typeof(NodeSpawner).GetField("currentInstancedNode").SetValue(component, currentInstancedNode);
         typeof(NodeSpawner).GetField("lastPlacedNode").SetValue(component, lastPlacedNode);
         typeof(NodeSpawner).GetField("lastPlacedAnchor").SetValue(component, lastPlacedAnchor);
         typeof(NodeSpawner).GetField("uiManager").SetValue(component, uiManager);
         typeof(NodeSpawner).GetField("initialTransformDatas").SetValue(component, initialTransformDatas);
         typeof(NodeSpawner).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(NodeSpawner).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}