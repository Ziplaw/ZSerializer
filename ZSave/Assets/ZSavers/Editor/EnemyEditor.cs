using UnityEditor;
using ZSave.Editor;

[CustomEditor(typeof(Enemy))]
public class EnemyEditor : Editor
{
    private Enemy manager;
    private bool editMode;

    private void OnEnable()
    {
        manager = target as Enemy;
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode);
        base.OnInspectorGUI();
    }
}