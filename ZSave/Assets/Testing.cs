using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using ZSave;

public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;


    private void Start()
    {
        
    }

    // private float timePassed = 0;

    // private void Start()
    // {
    //     Thread t = new Thread(Retrieve);
    //     t.Start();
    // }
    //
    // void Retrieve()
    // {
    //     Debug.Log(num2);
    // }
}

[Serializable]
public class TestingZSaver : ZSaver
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
public class ZSaver
{
    public int id;
}


