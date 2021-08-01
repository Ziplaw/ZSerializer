[System.Serializable]
public class PinZSerializer : ZSerializer.ZSerializer<Pin>
{
    public System.Int32 hits;

    public PinZSerializer(Pin PinInstance) : base(PinInstance.gameObject, PinInstance)
    {
         hits = (System.Int32)typeof(Pin).GetField("hits").GetValue(PinInstance);
    }
}