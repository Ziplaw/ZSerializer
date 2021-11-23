using UnityEditor;
using UnityEngine;
using ZSerializer;

namespace ZSerializer.Editor
{
    public abstract class PersistentMonoBehaviourEditor<T> : UnityEditor.Editor where T : PersistentMonoBehaviour
    {
        public T manager;
        private static ZSerializerStyler styler;

        public virtual void OnEnable()
        {
            manager = target as T;
            styler = new ZSerializerStyler();
        }

        public void DrawPersistentMonoBehaviourInspector()
        {
            if (manager.IsOn || ZSerializerSettings.Instance.componentDataDictionary[typeof(T)].isOn)
            {
                styler ??= new ZSerializerStyler();
                if (manager is PersistentMonoBehaviour)
                    ZSerializerEditor.BuildPersistentComponentEditor(manager, styler, ref manager.showSettings,
                        ZSerializerEditor.ShowGroupIDSettings);
                if (!manager.showSettings)
                {
                    if (ZSerializerSettings.Instance.debugMode)
                    {
                        using (new EditorGUI.DisabledScope(true))
                        {
                            EditorGUILayout.TextField("ZUID", manager.ZUID);
                            EditorGUILayout.TextField("GameObject ZUID", manager.GOZUID);
                        }
                    }

                    base.OnInspectorGUI();
                }
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}