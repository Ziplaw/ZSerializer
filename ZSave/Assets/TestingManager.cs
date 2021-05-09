using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using ZSave;

public class TestingManager : MonoBehaviour
{
    private Testing t;

    void Start()
    {
        t = FindObjectOfType<Testing>();
        TestingZSaver[] zsavers =
        {
            new TestingZSaver(FindObjectOfType<Testing>()),
            new TestingZSaver(FindObjectOfType<Testing>())
        };
        
        Persister[] persisters = { new Persister(){objectsToPersist = zsavers}};
        
        PersisterManager manager = new PersisterManager() {_persisters = persisters};
        
        manager.Save();


        // t = FindObjectOfType<Testing>();
        // TestingZSaver[] zsavers =
        // {
        //     new TestingZSaver(FindObjectOfType<Testing>()),
        //     new TestingZSaver(FindObjectOfType<Testing>())
        // };
        //
        // string json = JsonHelper.ToJson(zsavers);
        // Debug.Log(json);
        // t.num1 = 200000;
        // Debug.Log(t.num1);
        // // Destroy(t);
        // zsavers = JsonHelper.FromJson<TestingZSaver>(json);
        //
        // zsavers[0].Load();
    }
}