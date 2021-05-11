using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSave;

[Persistent(SaveType.Component,ExecutionCycle.None)]
public class TestingAgain : MonoBehaviour
{
    public float publicNum;
    public float publicNum1;
    public Vector3 PublicVector3;
    private int privateInt;
}
