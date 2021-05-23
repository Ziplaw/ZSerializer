using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using ZSaver;
using SaveType = ZSaver.SaveType;

[AddComponentMenu("ZSaver/Persistent GameObject")]
public class PersistentGameObject : MonoBehaviour
{
    private Text text;

    private int[] materialInstanceIDs;
    private Renderer _renderer;
    private Material[] _materials;

    private void Start()
    {
        _renderer = GetComponent<Renderer>();
        materialInstanceIDs = _renderer.sharedMaterials.Select(sm => sm.GetInstanceID()).ToArray();
    }

    private void OnEnable()
    {
        ZSave.OnBeforeSave += RestoreMaterialsToDefaultState;
        ZSave.OnAfterSave += RestoreMaterialsToPreSaveState;
    }

    private void OnDisable()
    {
        ZSave.OnBeforeSave -= RestoreMaterialsToDefaultState;
        ZSave.OnAfterSave -= RestoreMaterialsToPreSaveState;
    }

    private void RestoreMaterialsToPreSaveState()
    {
        _renderer.sharedMaterials = _materials;
    }

    private void RestoreMaterialsToDefaultState()
    {
        _materials = _renderer.sharedMaterials;
        var restoredSharedMaterials = (from id in materialInstanceIDs select ZSave.FindObjectFromInstanceID(id) as Material).ToArray();
        _renderer.sharedMaterials = restoredSharedMaterials;
    }

    private void Update()
    {
        if (_renderer)
        {
            if (_renderer.sharedMaterials.Length != materialInstanceIDs.Length)
                materialInstanceIDs = new int[_renderer.sharedMaterials.Length];

            for (var i = 0; i < _renderer.sharedMaterials.Length; i++)
            {
                var id = _renderer.sharedMaterials[i] == null ? 0 : _renderer.sharedMaterials[i].GetInstanceID();
                if (materialInstanceIDs[i] != id && id >= 0)
                {
                    materialInstanceIDs[i] = id;
                }
            }
        }
    }

    public static int CountParents(Transform transform)
    {
        int totalParents = 1;
        if (transform.parent != null)
        {
            totalParents += CountParents(transform.parent);
        }

        return totalParents;
    }
}