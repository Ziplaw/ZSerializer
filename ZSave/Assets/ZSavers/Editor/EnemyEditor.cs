using UnityEditor;
using ZSave.Editor;
using UnityEditor.Callbacks;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private Enemy manager;
    private bool editMode;
    private static ZSaverStyler styler;

    private void OnEnable()
    {
        manager = target as Enemy;
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