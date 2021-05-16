using ZSave;

[System.Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public Testing otherTesting;
    public UnityEngine.Rigidbody rb;

    public TestingZSaver(Testing TestingInstance) : base(TestingInstance.gameObject, TestingInstance)
    {
         otherTesting = (Testing)typeof(Testing).GetField("otherTesting").GetValue(TestingInstance);
         rb = (UnityEngine.Rigidbody)typeof(Testing).GetField("rb").GetValue(TestingInstance);
    }
}