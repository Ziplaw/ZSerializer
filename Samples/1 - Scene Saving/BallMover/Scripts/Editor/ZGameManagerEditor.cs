using UnityEditor;
using ZSerializer.Editor;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : PersistentMonoBehaviourEditor<GameManager> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}