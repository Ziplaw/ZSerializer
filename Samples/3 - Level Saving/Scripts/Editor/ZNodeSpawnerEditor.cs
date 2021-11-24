using UnityEditor;
using ZSerializer;
using ZSerializer.Editor;

[CustomEditor(typeof(NodeSpawner))]
public sealed class NodeSpawnerEditor : PersistentMonoBehaviourEditor<NodeSpawner> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}