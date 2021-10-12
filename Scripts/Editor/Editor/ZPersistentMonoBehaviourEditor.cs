using UnityEditor;
using ZSerializer;
using ZSerializer;
using ZSerializer.Editor;

[CustomEditor(typeof(PersistentMonoBehaviour))]
public sealed class PersistentMonoBehaviourEditor : PersistentMonoBehaviourEditor<PersistentMonoBehaviour> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}