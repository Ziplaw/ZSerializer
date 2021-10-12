[System.Serializable]
public class PinZSerializer : ZSerializer.Internal.ZSerializer<Pin>
{
    public System.Int32 hits;
    public int groupID;
    public bool autoSync;

    public PinZSerializer(Pin PinInstance) : base(PinInstance.gameObject, PinInstance)
    {
         hits = PinInstance.hits;
         groupID = PinInstance.GroupID;
         autoSync = PinInstance.AutoSync;
    }

    public override void RestoreValues(Pin component)
    {
         component.hits = hits;
         component.GroupID = groupID;
         component.AutoSync = autoSync;
    }
}