using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using ZSerializer;
using Random = UnityEngine.Random;

public class ObjectSpawner : PersistentMonoBehaviour
{
    [NonZSerialized]public GameObject[] objects;
    public List<GameObject> spawnedObjects;
    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        //camera raycast
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out var hit))
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //spawn object
                var go = Instantiate(objects[Random.Range(0, objects.Length)], hit.point + hit.normal * 5,
                    Quaternion.Euler(Random.Range(0, 360), Random.Range(0, 360), Random.Range(0, 360)), transform);
                spawnedObjects.Add(go);
                go.transform.localScale *= .5f;
                
                var pg = go.AddComponent<PersistentGameObject>();
                pg.AddComponent<Rigidbody>(PersistentType.Component);
                pg.AddComponent<MeshCollider>().convex = true;
            }
        }

        for (var i = 0; i < spawnedObjects.Count; i++)
        {
            if(spawnedObjects[i] == null) 
            {
                spawnedObjects.RemoveAt(i);
                return;
            }
            if (spawnedObjects[i].transform.position.y < -10) Destroy(spawnedObjects[i]);
        }
    }
}