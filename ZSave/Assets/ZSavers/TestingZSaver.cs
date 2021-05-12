using System;
using UnityEngine;
using ZSave;

[Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public System.Single num1;
    public System.Single num2;
    public UnityEngine.Rigidbody rb;

    public TestingZSaver(Testing TestingInstance) : base(TestingInstance.gameObject, TestingInstance)
    {
         num1 = TestingInstance.num1;
         num2 = TestingInstance.num2;
         rb = TestingInstance.rb;
    }
}