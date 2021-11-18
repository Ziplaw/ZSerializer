using UnityEditor;
using ZSerializer.Editor;

[CustomEditor(typeof(Pin))]
public class PinEditor : PersistentMonoBehaviourEditor<Pin> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}