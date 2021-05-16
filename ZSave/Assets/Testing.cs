using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using ZSave;
using SaveType = ZSave.SaveType;

[Persistent(ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public Testing otherTesting;
    public Rigidbody rb;
}