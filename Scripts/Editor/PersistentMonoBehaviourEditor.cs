using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using ZSerializer;
using ZSerializer.Editor;

[CustomEditor(typeof(PersistentMonoBehaviour),true, isFallback = false)]
public class PersistentMonoBehaviourEditor : Editor
{
    public PersistentMonoBehaviour manager;
    public Editor editor;

    public async void OnEnable()
    {
        try
        {
            manager = target as PersistentMonoBehaviour;

            Type type = null;
        
            await Task.Run(() =>
            {
                type = AppDomain.CurrentDomain.GetAssemblies().SelectMany(a => a.GetTypes()).Where(t =>
                {
                    var att = t.GetCustomAttributes<CustomEditor>().FirstOrDefault();
                    if (att == null) return false;
                    var inspectedType = typeof(CustomEditor).GetField("m_InspectedType", BindingFlags.Instance | BindingFlags.NonPublic)?.GetValue(att) as Type;
                    return inspectedType == manager.GetType();
                }).FirstOrDefault();
            });
        
        
            if (type != null)
            {
                editor = CreateEditor(manager, type);
            }
            else
            {
                editor = this;
            }
        }
        catch (Exception e)
        {
            ZSerialize.LogWarning(e, DebugMode.Developer);
        }
    }

    private void OnDisable()
    {
        if(editor == null || editor == this) return;
        
        editor.GetType().GetMethod("OnDisable", BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public)
            ?.Invoke(editor, null);
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

                if(editor == this)
                    try
                    {
                        base.OnInspectorGUI();
                    }
                    catch (Exception)
                    {
                        return;
                    }
                else if(editor != null) editor.OnInspectorGUI();
            }
        }
        else
        {
            if(editor == this) base.OnInspectorGUI();
            else if(editor != null) editor.OnInspectorGUI();
        }

        if (editor == null)
        {
            GUILayout.Label("Loading...");
            Repaint();
        }
    }
}
