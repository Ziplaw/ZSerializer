using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using ZSerializer;
using ZSerializer.Editor;

[CustomEditor(typeof(PersistentMonoBehaviour),true)]
public class PersistentMonoBehaviourEditor : Editor
{
    public PersistentMonoBehaviour manager;
    public Editor editor;

    public virtual void OnEnable()
    {
        manager = target as PersistentMonoBehaviour;
        
        var type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t =>
        {
            var att = t.GetCustomAttribute<CustomEditor>();
            if (att == null) return false;
            var type = typeof(CustomEditor).GetField("m_InspectedType", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(att) as Type;
            return type == manager.GetType();
        }).FirstOrDefault();
        if (type != null)
        {
            editor = CreateEditor(manager, type);
        }
        else
        {
            editor = this;
        }
        
    }
    
    public override void OnInspectorGUI()
    {
        DrawPersistentMonoBehaviourInspector();
    }

    public void DrawPersistentMonoBehaviourInspector()
    {
        if (manager.IsOn || ZSerializerSettings.Instance.componentDataDictionary[typeof(PersistentMonoBehaviour)].isOn)
        {
            if (manager is PersistentMonoBehaviour)
                ZSerializerEditor.BuildPersistentComponentEditor(manager, ZSerializerStyler.Instance, ref manager.showSettings,
                    ZSerializerEditor.ShowGroupIDSettings);
            if (!manager.showSettings)
            {
                if (ZSerializerSettings.Instance.debugMode == DebugMode.Developer)
                {
                    using (new EditorGUI.DisabledScope(true))
                    {
                        EditorGUILayout.TextField("ZUID", manager.ZUID);
                        EditorGUILayout.TextField("GameObject ZUID", manager.GOZUID);
                    }
                }

                if(editor == this) base.OnInspectorGUI();
                else editor.OnInspectorGUI();
            }
        }
        else
        {
            if(editor == this) base.OnInspectorGUI();
            else editor.OnInspectorGUI();
        }
    }
}
