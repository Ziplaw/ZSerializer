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
    private void Start()
    {
        
        // Light light = gameObject.AddComponent<Light>();
        // light.shadowRadius = 100000000000;
        //
        // Debug.Log(light.shadowRadius);

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