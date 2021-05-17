using ZSave;

[System.Serializable]
public class TestingAgainZSaver : ZSaver<TestingAgain>
{
    public System.Single publicNum;
    public System.Single publicNum1;
    public UnityEngine.Vector3 PublicVector3;
    public System.Single num4;
    public System.Single num5;

    public TestingAgainZSaver(TestingAgain TestingAgainInstance) : base(TestingAgainInstance.gameObject, TestingAgainInstance)
    {
         publicNum = (System.Single)typeof(TestingAgain).GetField("publicNum").GetValue(TestingAgainInstance);
         publicNum1 = (System.Single)typeof(TestingAgain).GetField("publicNum1").GetValue(TestingAgainInstance);
         PublicVector3 = (UnityEngine.Vector3)typeof(TestingAgain).GetField("PublicVector3").GetValue(TestingAgainInstance);
         num4 = (System.Single)typeof(TestingAgain).GetField("num4").GetValue(TestingAgainInstance);
         num5 = (System.Single)typeof(TestingAgain).GetField("num5").GetValue(TestingAgainInstance);
    }
}