using UnityEditor;
using ZSerializer.Editor;

[CustomEditor(typeof(BallMover))]
public class BallMoverEditor : PersistentMonoBehaviourEditor<BallMover> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}