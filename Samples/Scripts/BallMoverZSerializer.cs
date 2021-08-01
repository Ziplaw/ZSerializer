[System.Serializable]
public class BallMoverZSerializer : ZSerializer.ZSerializer<BallMover>
{

    public BallMoverZSerializer(BallMover BallMoverInstance) : base(BallMoverInstance.gameObject, BallMoverInstance)
    {
    }
}