[System.Serializable]
public sealed class GameManagerZSerializer : ZSerializer.Internal.ZSerializer
{
    public System.Int32 highScore;
    public System.Int32 currentScore;
    public System.String playerName;
    public UnityEngine.Vector3 position;
    public BallMover ballMover;
    public UnityEngine.GameObject canvas;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public GameManagerZSerializer(string ZUID, string GOZUID) : base(ZUID, GOZUID)
    {       var instance = ZSerializer.ZSerialize.idMap[ZSerializer.ZSerialize.CurrentGroupID][ZUID];
         highScore = (System.Int32)typeof(GameManager).GetField("highScore").GetValue(instance);
         currentScore = (System.Int32)typeof(GameManager).GetField("currentScore").GetValue(instance);
         playerName = (System.String)typeof(GameManager).GetField("playerName").GetValue(instance);
         position = (UnityEngine.Vector3)typeof(GameManager).GetField("position").GetValue(instance);
         ballMover = (BallMover)typeof(GameManager).GetField("ballMover").GetValue(instance);
         canvas = (UnityEngine.GameObject)typeof(GameManager).GetField("canvas").GetValue(instance);
         groupID = (System.Int32)typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
         autoSync = (System.Boolean)typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).GetValue(instance);
    }

    public override void RestoreValues(UnityEngine.Component component)
    {
         typeof(GameManager).GetField("highScore").SetValue(component, highScore);
         typeof(GameManager).GetField("currentScore").SetValue(component, currentScore);
         typeof(GameManager).GetField("playerName").SetValue(component, playerName);
         typeof(GameManager).GetField("position").SetValue(component, position);
         typeof(GameManager).GetField("ballMover").SetValue(component, ballMover);
         typeof(GameManager).GetField("canvas").SetValue(component, canvas);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("groupID", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, groupID);
         typeof(ZSerializer.PersistentMonoBehaviour).GetField("autoSync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic).SetValue(component, autoSync);
    }
}