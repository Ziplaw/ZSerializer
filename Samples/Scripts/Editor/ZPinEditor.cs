using UnityEditor;
using UnityEditor.Callbacks;
using ZSerializer;

[CustomEditor(typeof(Pin))]
public class PinEditor : Editor
{
    private Pin manager;
    private static ZSaverStyler styler;

    private void OnEnable()
    {
        manager = target as Pin;
        styler = new ZSaverStyler();
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        if(ZSaverSettings.Instance && ZSaverSettings.Instance.packageInitialized)
        styler = new ZSaverStyler();
    }

    public override void OnInspectorGUI()
    {
        ZSaverEditor.BuildPersistentComponentEditor(manager, styler);
        base.OnInspectorGUI();
    }
}