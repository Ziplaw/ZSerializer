using ZSave;

[System.Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public System.Single num1;
    public System.Single num2;
    public UnityEngine.MeshRenderer renderer;
    public UnityEngine.MeshFilter filter;

    public TestingZSaver(Testing TestingInstance) : base(TestingInstance.gameObject, TestingInstance)
    {
         num1 = (System.Single)typeof(Testing).GetField("num1").GetValue(TestingInstance);
         num2 = (System.Single)typeof(Testing).GetField("num2").GetValue(TestingInstance);
         renderer = (UnityEngine.MeshRenderer)typeof(Testing).GetField("renderer").GetValue(TestingInstance);
         filter = (UnityEngine.MeshFilter)typeof(Testing).GetField("filter").GetValue(TestingInstance);
    }
}