using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using ZSave;

public class Testing : MonoBehaviour
{
    [Save(0, SaveCycle.None), RetrieveOn(RetrieveCycle.OnStart)] private float num1 = 2;
    [Save(0, SaveCycle.None), RetrieveOn(RetrieveCycle.OnStart)] private float num2 = 56;

    private void Start()
    {
        Debug.Log(num2);
        CycleSaver.RetrieveValue(ref num2, "num2", this);
        Debug.Log(num2);
    }
}


