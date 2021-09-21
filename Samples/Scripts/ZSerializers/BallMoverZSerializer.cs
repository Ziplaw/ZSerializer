[System.Serializable]
public class BallMoverZSerializer : ZSerializer.ZSerializer<BallMover>
{
    public System.Int32 groupID;
    public System.Boolean autoSync;

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