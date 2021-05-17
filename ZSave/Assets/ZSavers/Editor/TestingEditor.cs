using UnityEditor;
using ZSave.Editor;
using UnityEditor.Callbacks;

[CustomEditor(typeof(Testing))]
public class TestingEditor : Editor
{
    private Testing manager;
    private bool editMode;
    private static ZSaverStyler styler;

    private void OnEnable()
    {
        manager = target as Testing;
        styler = new ZSaverStyler();
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        styler = new ZSaverStyler();
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode, styler);
        base.OnInspectorGUI();
    }
}