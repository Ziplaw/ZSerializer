using System.Reflection;
using UnityEditor;
using ZSave.Editor;

[CustomEditor(typeof(Testing))]
public class TestingEditor : Editor
{
    private Testing manager;
    private bool editMode;
    private bool[] persistentFields;

    private void OnEnable()
    {
        manager = target as Testing;
        persistentFields = new bool[manager.GetType().GetFields(BindingFlags.Public | BindingFlags.Instance).Length];
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, ref editMode);
        ZSaverEditor.BuildEditModeEditor(serializedObject, manager, editMode, ref persistentFields,
            base.OnInspectorGUI);
    }
}