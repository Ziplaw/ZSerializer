using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using ZSave;


public class Testing : PersistentMonoBehaviour
{
    private float num1 = 2;
    private float num2 = 56;


    private float timePassed = 0;

    private void Start()
    {
        
    }

    private void Update()
    {
        transform.position = new Vector3(0,Mathf.Sin(Time.time),0);
        
        if (timePassed > -1)
        {
            timePassed += Time.deltaTime;
            if (timePassed > 2)
            {
                Thread t = new Thread(Retrieve);
                t.Start();
                timePassed = -1;
            }
        }
    }

    void Retrieve()
    {
        Debug.Log(num2);
    }
}


