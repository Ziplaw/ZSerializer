using UnityEditor;
using ZSerializer;

namespace ZSerializer.Editor
{
    public abstract class PersistentMonoBehaviourEditor<T> : UnityEditor.Editor where T : PersistentMonoBehaviour
    {
        private T manager;
        private static ZSerializerStyler styler;

        public virtual void OnEnable()
        {
            manager = target as T;
            styler = new ZSerializerStyler();
        }

        public void DrawPersistentMonoBehaviourInspector()
        {
            if (manager.isOn || ZSerializerSettings.Instance.componentDataDictionary[typeof(T)].isOn)
            {
                styler ??= new ZSerializerStyler();
                if (manager is PersistentMonoBehaviour)
                    ZSerializerEditor.BuildPersistentComponentEditor(manager, styler, ref manager.showSettings,
                        ZSerializerEditor.ShowGroupIDSettings);
                if (!manager.showSettings) base.OnInspectorGUI();
            }
            else
            {
                base.OnInspectorGUI();
            }
        }
    }
}