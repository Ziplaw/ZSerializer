[System.Serializable]
public class GameManagerZSerializer : ZSerializer.ZSerializer<GameManager>
{
    public System.Int32 highScore;
    public System.Int32 currentScore;
    public System.String playerName;
    public UnityEngine.Vector3 position;
    public BallMover ballMover;
    public int groupID;
    public bool autoSync;

    public GameManagerZSerializer(GameManager GameManagerInstance) : base(GameManagerInstance.gameObject, GameManagerInstance)
    {
         highScore = GameManagerInstance.highScore;
         currentScore = GameManagerInstance.currentScore;
         playerName = GameManagerInstance.playerName;
         position = GameManagerInstance.position;
         ballMover = GameManagerInstance.ballMover;
         groupID = GameManagerInstance.GroupID;
         autoSync = GameManagerInstance.AutoSync;
    }

    public override void RestoreValues(GameManager component)
    {
         component.highScore = highScore;
         component.currentScore = currentScore;
         component.playerName = playerName;
         component.position = position;
         component.ballMover = ballMover;
         component.GroupID = groupID;
         component.AutoSync = autoSync;
    }
}