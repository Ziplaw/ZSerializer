using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using ZSave;

[Persistent]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
}

[Serializable]
public class TestingZSaver
{
    public float num1;
    public float num2;

    public Testing _testing;

    public TestingZSaver(Testing testing)
    {
        num1 = testing.num1;
        num2 = testing.num2;

        _testing = testing;
    }

    public void Load()
    {
        _testing.num1 = num1;
        _testing.num2 = num2;
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


