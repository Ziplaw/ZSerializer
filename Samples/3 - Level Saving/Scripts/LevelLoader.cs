using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class LevelLoader : MonoBehaviour
{
    private List<string> levelNames;
    private string levelName;
    private int currentLevelIndex;

    private void OnGUI()
    {
        levelNames = ZSerialize.GetLevelNames();
        GUILayout.Label("Press Space to spawn objects");
        levelName = GUILayout.TextField(levelName);
        
        if (GUILayout.Button("Save"))
        {
            ZSerialize.SaveLevel(string.IsNullOrEmpty(levelName) ? levelNames[currentLevelIndex] : levelName, transform);
            return;
        }
        
        if (GUILayout.Button("Load"))
        {
            ZSerialize.LoadLevel(string.IsNullOrEmpty(levelName) ? levelNames[currentLevelIndex] : levelName, transform, true);
            return;
        }
    }
}
