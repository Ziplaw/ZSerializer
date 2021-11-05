[System.Serializable]
public sealed class GameManagerZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Int32 highScore;
    public System.Int32 currentScore;
    public System.String playerName;
    public UnityEngine.Vector3 position;
    public BallMover ballMover;
    public UnityEngine.GameObject canvas;
    public int groupID;
    public bool autoSync;

    public GameManagerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZUID];
         highScore = (System.Int32)typeof(GameManager).GetField("highScore").GetValue(instance);
         currentScore = (System.Int32)typeof(GameManager).GetField("currentScore").GetValue(instance);
         playerName = (System.String)typeof(GameManager).GetField("playerName").GetValue(instance);
         position = (UnityEngine.Vector3)typeof(GameManager).GetField("position").GetValue(instance);
         ballMover = (BallMover)typeof(GameManager).GetField("ballMover").GetValue(instance);
         canvas = (UnityEngine.GameObject)typeof(GameManager).GetField("canvas").GetValue(instance);
         groupID = (int)typeof(GameManager).GetProperty("GroupID").GetValue(instance);
         autoSync = (bool)typeof(GameManager).GetProperty("AutoSync").GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(GameManager).GetField("highScore").SetValue(component, highScore);
         typeof(GameManager).GetField("currentScore").SetValue(component, currentScore);
         typeof(GameManager).GetField("playerName").SetValue(component, playerName);
         typeof(GameManager).GetField("position").SetValue(component, position);
         typeof(GameManager).GetField("ballMover").SetValue(component, ballMover);
         typeof(GameManager).GetField("canvas").SetValue(component, canvas);
         typeof(GameManager).GetProperty("GroupID").SetValue(component, groupID);
         typeof(GameManager).GetProperty("AutoSync").SetValue(component, autoSync);
    }
}