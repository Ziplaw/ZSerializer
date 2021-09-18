using UnityEditor;
using UnityEditor.Callbacks;
using ZSerializer;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    private GameManager manager;
    private static ZSerializerStyler styler;

    private void OnEnable()
    {
        manager = target as GameManager;
        styler = new ZSerializerStyler();
    }

    [DidReloadScripts]
    static void OnDatabaseReload()
    {
        if(ZSaverSettings.Instance && ZSaverSettings.Instance.packageInitialized)
        styler = new ZSerializerStyler();
    }

    public override void OnInspectorGUI()
    {
        if(manager is PersistentMonoBehaviour)
            ZSaverEditor.BuildPersistentComponentEditor(manager, styler, ref manager.showSettings, ZSaverEditor.ShowGroupIDSettings);
        if(!manager.showSettings) base.OnInspectorGUI();
    }
}