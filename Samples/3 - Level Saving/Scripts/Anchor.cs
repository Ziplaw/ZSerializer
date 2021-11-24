using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class Anchor : PersistentMonoBehaviour
{
    [SerializeField] Vector3 position;
    public Transform nodeTransform;

    private void Update()
    {
        if(nodeTransform != null)
        {
            position = nodeTransform.position + nodeTransform.right * nodeTransform.localScale.x;
            transform.position = position;
        }
    }
}
