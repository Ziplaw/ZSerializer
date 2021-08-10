[System.Serializable]
public class BallMoverZSerializer : ZSerializer.ZSerializer<BallMover>
{
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public BallMoverZSerializer(BallMover BallMoverInstance) : base(BallMoverInstance.gameObject, BallMoverInstance)
    {
         groupID = (System.Int32)typeof(BallMover).GetField("groupID").GetValue(BallMoverInstance);
         autoSync = (System.Boolean)typeof(BallMover).GetField("autoSync").GetValue(BallMoverInstance);
    }
}