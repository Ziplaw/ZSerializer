using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using ZSave;

public class TestingManager : MonoBehaviour
{
    private Testing[] t;
    private TestingZSaver[] _zSavers;

    void Start()
    {
        Save();
        // Load();
        
        // Debug.Log(PersistanceManager.GetTypesWithPersistentAttribute(Assembly.GetAssembly(typeof(Testing))).Count());

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

    void Save()
    {
        t = FindObjectsOfType<Testing>();
        _zSavers = (from t1 in t select new TestingZSaver(t1)).ToArray();

        for (var i = 0; i < _zSavers.Length; i++)
        {
            _zSavers[i].num1 += 5;
        }
        
        // TestingPersister testingPersister = new TestingPersister(new Persister<TestingZSaver>(_zSavers));
        
        // PersistanceManager.Save(testingPersister._persister);
        PersistanceManager.Save(_zSavers);
    }

    void Load()
    {
        _zSavers = JsonHelper.FromJson<TestingZSaver>(PersistanceManager.ReadFromFile("save.save"));
        
        foreach (var testingZSaver in _zSavers)
        {
            testingZSaver.Load();
        }
    }
}