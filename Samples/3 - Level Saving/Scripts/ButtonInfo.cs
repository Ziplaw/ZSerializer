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
    }
    
    public void SetLevelName(string name)
    {
        levelName = name;
    }

}
