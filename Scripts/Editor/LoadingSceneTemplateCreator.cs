using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class LoadingSceneTemplateCreator : Editor
{
    [MenuItem("Assets/Create/ZSerializer/Loading Scene")]
    public static void CopyTemplate()
    {
        var path = "";
        var obj = Selection.activeObject;
        if (obj == null) path = "Assets";
        else path = AssetDatabase.GetAssetPath(obj.GetInstanceID());
        if (path.Length > 0)
        {
            if (!Directory.Exists(path))
            {
                var split = path.Split('/').ToList();
                split.RemoveAt(split.Count - 1);
                path = string.Join("/", split.ToArray());
            }

            if(File.Exists(Path.Combine(path, "New Loading Scene.unity"))) throw new Exception("Couldn't create Loading Scene since a scene named 'New Loading Scene' already exists in this folder.");
            
            AssetDatabase.CopyAsset(@"Assets/ZSerializer/Scripts/Editor/LoadingSceneTemplate.unity", Path.Combine(path, "New Loading Scene.unity"));
            var newScene = AssetDatabase.LoadAssetAtPath<SceneAsset>(Path.Combine(path, "New Loading Scene.unity"));
            Selection.activeObject = newScene;
            EditorGUIUtility.PingObject(newScene);
            AssetDatabase.Refresh();
        }
        else
        {
            Debug.Log("Not in assets folder");
        }
    }
}