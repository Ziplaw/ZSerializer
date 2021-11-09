[System.Serializable]
public sealed class PinZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Int32 hits;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public PinZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZUID];
         hits = (System.Int32)typeof(Pin).GetField("hits").GetValue(instance);
         groupID = (System.Int32)typeof(Pin).GetField("groupID").GetValue(instance);
         autoSync = (System.Boolean)typeof(Pin).GetField("autoSync").GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(Pin).GetField("hits").SetValue(component, hits);
         typeof(Pin).GetField("groupID").SetValue(component, groupID);
         typeof(Pin).GetField("autoSync").SetValue(component, autoSync);
    }
}