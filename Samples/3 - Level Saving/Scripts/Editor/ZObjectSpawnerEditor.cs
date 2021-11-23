using UnityEditor;
using ZSerializer;
using ZSerializer.Editor;

[CustomEditor(typeof(ObjectSpawner))]
public sealed class ObjectSpawnerEditor : PersistentMonoBehaviourEditor<ObjectSpawner> 
{
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }
}