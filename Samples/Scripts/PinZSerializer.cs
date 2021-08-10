[System.Serializable]
public class PinZSerializer : ZSerializer.ZSerializer<Pin>
{
    public System.Int32 hits;
    public System.Int32 groupID;
    public System.Boolean autoSync;

    public PinZSerializer(Pin PinInstance) : base(PinInstance.gameObject, PinInstance)
    {
         hits = (System.Int32)typeof(Pin).GetField("hits").GetValue(PinInstance);
         groupID = (System.Int32)typeof(Pin).GetField("groupID").GetValue(PinInstance);
         autoSync = (System.Boolean)typeof(Pin).GetField("autoSync").GetValue(PinInstance);
    }
}