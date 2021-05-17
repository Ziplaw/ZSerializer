using ZSave;

[System.Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public System.Single num1;
    public System.Single num2;

    public TestingZSaver(Testing TestingInstance) : base(TestingInstance.gameObject, TestingInstance)
    {
         num1 = (System.Single)typeof(Testing).GetField("num1").GetValue(TestingInstance);
         num2 = (System.Single)typeof(Testing).GetField("num2").GetValue(TestingInstance);
    }
}