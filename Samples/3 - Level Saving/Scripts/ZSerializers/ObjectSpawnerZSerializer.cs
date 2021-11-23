[System.Serializable]
public sealed class ObjectSpawnerZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Collections.Generic.List<UnityEngine.GameObject> spawnedObjects;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public ObjectSpawnerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         spawnedObjects = (System.Collections.Generic.List<UnityEngine.GameObject>)typeof(ObjectSpawner).GetField("spawnedObjects").GetValue(instance);
         groupID = (System.Int32)typeof(ObjectSpawner).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ObjectSpawner).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(ObjectSpawner).GetField("spawnedObjects").SetValue(component, spawnedObjects);
         typeof(ObjectSpawner).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ObjectSpawner).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}