[System.Serializable]
public sealed class PinZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Int32 hits;
    public int groupID;
    public bool autoSync;

    public PinZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZUID];
         hits = (System.Int32)typeof(Pin).GetField("hits").GetValue(instance);
         groupID = (int)typeof(Pin).GetProperty("GroupID").GetValue(instance);
         autoSync = (bool)typeof(Pin).GetProperty("AutoSync").GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(Pin).GetField("hits").SetValue(component, hits);
         typeof(Pin).GetProperty("GroupID").SetValue(component, groupID);
         typeof(Pin).GetProperty("AutoSync").SetValue(component, autoSync);
    }
}