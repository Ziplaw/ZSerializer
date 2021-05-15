using UnityEditor;
using ZSave.Editor;

[CustomEditor(typeof(TestingAgain))]
public class TestingAgainEditor : Editor
{
    private TestingAgain manager;
    private bool editMode;

    private void OnEnable()
    {
        manager = target as TestingAgain;
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode);
        base.OnInspectorGUI();
    }
}