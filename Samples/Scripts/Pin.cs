using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using ZSerializer;

public class Pin : PersistentMonoBehaviour
{
    private Renderer _renderer;
    private MaterialPropertyBlock _propBlock;
    public int hits;

    private void Awake()
    {
        _propBlock = new MaterialPropertyBlock();
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        StopAllCoroutines();
        StartCoroutine(PingColor());
        hits++;
    }

    IEnumerator PingColor()
    {
        var color = _renderer.sharedMaterial.color;
        for (int i = 0; i < 120; i++)
        {
            float t = i / 120f;
            
            _renderer.GetPropertyBlock(_propBlock);
            _propBlock.SetColor("_Color",Color.Lerp(Color.cyan, color, t));
                _renderer.SetPropertyBlock(_propBlock);
            
            yield return null;
        }
    }

    #if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Handles.color = Color.cyan;
        Handles.Label(transform.position + Vector3.up*2.5f + Quaternion.Euler(0,45,0)*Vector3.left*.125f, hits.ToString());
    }
    #endif
}
