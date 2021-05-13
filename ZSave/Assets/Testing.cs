using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using ZSave;

[Persistent(SaveType.GameObject,ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
    public float num3 = 248;
    public Rigidbody rb;

    private void Start()
    {
        // foreach (var fieldInfo in typeof(MeshFilter).GetFields(BindingFlags.Public | BindingFlags.Instance))
        // {
        //     Debug.Log(fieldInfo.Name + " "+ fieldInfo.IsDefined(typeof(ObsoleteAttribute)));
        // }
        //
        // Debug.Log("----------------------------------------");
        //
        //
        // foreach (var fieldInfo in typeof(MeshFilter).GetProperties(BindingFlags.Public | BindingFlags.Instance))
        // {
        //     Debug.Log(fieldInfo.Name+ " " + fieldInfo.IsDefined(typeof(ObsoleteAttribute))  + " " + fieldInfo.SetMethod == null);
        // }
    }
}


