[System.Serializable]
public class GameManagerZSerializer : ZSerializer.ZSerializer<GameManager>
{
    public BallMover ballMover;

    public GameManagerZSerializer(GameManager GameManagerInstance) : base(GameManagerInstance.gameObject, GameManagerInstance)
    {
         ballMover = (BallMover)typeof(GameManager).GetField("ballMover").GetValue(GameManagerInstance);
    }
}