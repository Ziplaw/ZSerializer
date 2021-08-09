[System.Serializable]
public class PersistentGameObjectZSerializer : ZSerializer.ZSerializer<PersistentGameObject>
{

    public PersistentGameObjectZSerializer(PersistentGameObject PersistentGameObjectInstance) : base(PersistentGameObjectInstance.gameObject, PersistentGameObjectInstance)
    {
    }
}