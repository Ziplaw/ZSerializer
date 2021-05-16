using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZSave;

[Persistent( ExecutionCycle.None)]
public class Enemy : MonoBehaviour
{
    public Material mat;
    public Mesh mesh;
    void Start()
    {
        // PersistanceManager.SaveAllObjectsAndComponents();
        

        // var objects = Object.FindObjectsOfType(type);
        //
        // var ZSaverType = Type.GetType(type.Name + "ZSaver");
        // if (ZSaverType == null) break;
        // var ZSaverArrayType = ZSaverType.MakeArrayType();
        //
        // var zsaversArray = Activator.CreateInstance(ZSaverArrayType, new object[] {objects.Length});
        //
        // object[] zsavers = (object[]) zsaversArray;
        //
        //
        // for (var i = 0; i < zsavers.Length; i++)
        // {
        //     zsavers[i] = Activator.CreateInstance(ZSaverType, new object[] {objects[i]});
        // }
        //
        //
        // var saveMethodInfo = typeof(PersistanceManager).GetMethod(nameof(PersistanceManager.Save));
        // var genericSaveMethodInfo = saveMethodInfo.MakeGenericMethod(ZSaverType);
        // genericSaveMethodInfo.Invoke(null, new object[] {zsavers, type.Name + ".save"});
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
