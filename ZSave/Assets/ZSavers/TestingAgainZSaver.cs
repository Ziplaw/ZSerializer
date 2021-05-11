using System;
using UnityEngine;
using ZSave;

[Serializable]
public class TestingAgainZSaver : ZSaver<TestingAgain>
{
    public System.Single publicNum;
    public System.Single publicNum1;
    public UnityEngine.Vector3 PublicVector3;

    public TestingAgainZSaver(TestingAgain TestingAgainInstance) : base(TestingAgainInstance.gameObject, TestingAgainInstance)
    {
         publicNum = TestingAgainInstance.publicNum;
         publicNum1 = TestingAgainInstance.publicNum1;
         PublicVector3 = TestingAgainInstance.PublicVector3;
    }
}