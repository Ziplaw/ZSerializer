using System;
using System.Linq;
using UnityEngine;
using ZSave;
using SaveType = ZSave.SaveType;

[Persistent(ExecutionCycle.OnStart)]
public class Testing : MonoBehaviour
{
    public float num1 = 2;
    public float num2 = 56;
    public MeshRenderer renderer;
    public MeshFilter filter;
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