using UnityEditor;
using ZSerializer;
using ZSerializer.Editor;

[CustomEditor(typeof(Anchor))]
public sealed class AnchorEditor : PersistentMonoBehaviourEditor<Anchor> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}