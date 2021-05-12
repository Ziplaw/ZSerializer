using System;
using UnityEngine;
using ZSave;

[Persistent(SaveType.GameObject,ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
    public Rigidbody rb;
}


