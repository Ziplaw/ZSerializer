[System.Serializable]
public sealed class BallMoverZSerializer : ZSerializer.Internal.ZSerializer<BallMover>
{
    public int groupID;
    public bool autoSync;

    public BallMoverZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZUID];
         groupID = (int)typeof(BallMover).GetProperty("GroupID").GetValue(instance);
         autoSync = (bool)typeof(BallMover).GetProperty("AutoSync").GetValue(instance);
    }

    public override void RestoreValues(BallMover component)
    {
         typeof(BallMover).GetProperty("GroupID").SetValue(component, groupID);
         typeof(BallMover).GetProperty("AutoSync").SetValue(component, autoSync);
    }
}