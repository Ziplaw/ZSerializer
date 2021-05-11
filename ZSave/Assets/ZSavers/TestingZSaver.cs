using System;
using UnityEngine;
using ZSave;

[Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public System.Single num1;
    public System.Single num2;

    public TestingZSaver(Testing testing) : base(testing.gameObject, testing)
    {
         num1 = testing.num1;
         num2 = testing.num2;
    }
}