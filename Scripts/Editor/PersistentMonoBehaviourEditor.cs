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

        public override void OnInspectorGUI()
        {
            styler ??= new ZSerializerStyler();
            if (manager is PersistentMonoBehaviour)
                ZSaverEditor.BuildPersistentComponentEditor(manager, styler, ref manager.showSettings,
                    ZSaverEditor.ShowGroupIDSettings);
            if (!manager.showSettings) base.OnInspectorGUI();
        }
    }
}