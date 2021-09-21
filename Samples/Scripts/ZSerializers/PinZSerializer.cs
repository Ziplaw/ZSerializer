[System.Serializable]
public class PinZSerializer : ZSerializer.ZSerializer<Pin>
{
    public System.Int32 hits;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public PinZSerializer(Pin PinInstance) : base(PinInstance.gameObject, PinInstance)
    {
         hits = (System.Int32)typeof(Pin).GetField("hits").GetValue(PinInstance);
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