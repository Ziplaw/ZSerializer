using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using ZSerializer;

public class ButtonInfo : MonoBehaviour
{
    public string levelName;
    public Transform levelParent;
    
    public void SaveLevel()
    {
        FindObjectOfType<NodeSpawner>().ResetNodes();
        ZSerialize.SaveLevel(levelName, levelParent);
        var uiManager = FindObjectOfType<UIManager>();
        uiManager.DestroyAllButtons();
        var levelNames = ZSerialize.GetLevelNames();
        foreach (var levelName in levelNames)
        {
            uiManager.CreateButton(levelName, transform, () =>
            {
                ZSerialize.LoadLevel(levelName, levelParent, true);
            });
        }
    }
    
    public void SetLevelName(string name)
    {
        levelName = name;
    }

}
