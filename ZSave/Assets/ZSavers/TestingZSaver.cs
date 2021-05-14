using ZSave;

[System.Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public System.Single num1;
    public System.Single num2;
    public System.Int32 num3;
    public TestingAgain mf;

    public TestingZSaver(Testing TestingInstance) : base(TestingInstance.gameObject, TestingInstance)
    {
         num1 = (System.Single)typeof(Testing).GetField("num1").GetValue(TestingInstance);
         num2 = (System.Single)typeof(Testing).GetField("num2").GetValue(TestingInstance);
         num3 = (System.Int32)typeof(Testing).GetField("num3").GetValue(TestingInstance);
         mf = (TestingAgain)typeof(Testing).GetField("mf").GetValue(TestingInstance);
    }
}
