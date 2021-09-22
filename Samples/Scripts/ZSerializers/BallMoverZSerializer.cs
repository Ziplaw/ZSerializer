[System.Serializable]
public class BallMoverZSerializer : ZSerializer.ZSerializer<BallMover>
{
    int groupID;
    bool autoSync;

    public BallMoverZSerializer(BallMover BallMoverInstance) : base(BallMoverInstance.gameObject, BallMoverInstance)
    {
         groupID = BallMoverInstance.GroupID;
         autoSync = BallMoverInstance.AutoSync;
    }

    public override void RestoreValues(BallMover component)
    {
         component.GroupID = groupID;
         component.AutoSync = autoSync;
    }
}