using UnityEditor;
using ZSave.Editor;

[CustomEditor(typeof(Testing))]
public class TestingEditor : Editor
{
    private Testing manager;
    private bool editMode;

    private void OnEnable()
    {
        manager = target as Testing;
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode);
        base.OnInspectorGUI();
    }
}