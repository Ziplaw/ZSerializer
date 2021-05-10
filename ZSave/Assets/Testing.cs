using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using ZSave;

[Persistent(SaveType.Component,ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
}

[Serializable]
public class TestingZSaver : ZSaver<Testing>
{
    public float num1;
    public float num2;
    public TestingZSaver(Testing testing) : base(testing.gameObject, testing)
    {
        num1 = testing.num1;
        num2 = testing.num2;
    }
}

[Serializable]
public class TestingPersister
{
    public Persister<TestingZSaver> _persister;

    public TestingPersister(Persister<TestingZSaver> persister)
    {
        _persister = persister;
    }
}


