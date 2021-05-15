using System;
using System.Linq;
using UnityEngine;
using ZSave;
using SaveType = ZSave.SaveType;

[Persistent(SaveType.Component, ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
    public int num3 = 248;

    public GameObject[] allEnemies;
    private void Start()
    {
        // Debug.Log(Type.GetType()  );
        
        // allEnemies = (from i in FindObjectsOfType<Enemy>() select i.gameObject).ToArray();
        // PersistentAttribute.LoadAllObjects(0);
        // PersistentAttribute.SaveAllObjects(0);
    }

    private void OnDestroy()
    {
        // PersistentAttribute.SaveAllObjects(0);
    }
}