[System.Serializable]
public sealed class BallMoverZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public BallMoverZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZUID];
         groupID = (System.Int32)typeof(BallMover).GetField("groupID").GetValue(instance);
         autoSync = (System.Boolean)typeof(BallMover).GetField("autoSync").GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(BallMover).GetField("groupID").SetValue(component, groupID);
         typeof(BallMover).GetField("autoSync").SetValue(component, autoSync);
    }
}