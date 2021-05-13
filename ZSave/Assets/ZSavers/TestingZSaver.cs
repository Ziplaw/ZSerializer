using ZSave;

[System.Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public System.Single num1;
    public System.Single num2;
    public System.Single num3;
    public UnityEngine.Rigidbody rb;

    public TestingZSaver(Testing TestingInstance) : base(TestingInstance.gameObject, TestingInstance)
    {
         num1 = (System.Single)typeof(Testing).GetField("num1").GetValue(TestingInstance);
         num2 = (System.Single)typeof(Testing).GetField("num2").GetValue(TestingInstance);
         num3 = (System.Single)typeof(Testing).GetField("num3").GetValue(TestingInstance);
         rb = (UnityEngine.Rigidbody)typeof(Testing).GetField("rb").GetValue(TestingInstance);
    }
}