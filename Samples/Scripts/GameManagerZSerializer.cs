[System.Serializable]
public class GameManagerZSerializer : ZSerializer.ZSerializer<GameManager>
{
    public System.Int32 highScore;
    public System.Int32 currentScore;
    public System.String playerName;
    public UnityEngine.Vector3 position;
    public BallMover ballMover;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public GameManagerZSerializer(GameManager GameManagerInstance) : base(GameManagerInstance.gameObject, GameManagerInstance)
    {
         highScore = (System.Int32)typeof(GameManager).GetField("highScore").GetValue(GameManagerInstance);
         currentScore = (System.Int32)typeof(GameManager).GetField("currentScore").GetValue(GameManagerInstance);
         playerName = (System.String)typeof(GameManager).GetField("playerName").GetValue(GameManagerInstance);
         position = (UnityEngine.Vector3)typeof(GameManager).GetField("position").GetValue(GameManagerInstance);
         ballMover = (BallMover)typeof(GameManager).GetField("ballMover").GetValue(GameManagerInstance);
         groupID = GameManagerInstance.GroupID;
         autoSync = GameManagerInstance.AutoSync;
    }
}