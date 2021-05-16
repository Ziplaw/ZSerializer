using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSave;

public class UIPersistantBridge : MonoBehaviour
{
    public void Save(int groupID)
    {
        PersistanceManager.SaveAllObjectsAndComponents();
    }
    
    public void Load(int groupID)
    {
        PersistanceManager.LoadAllObjectsAndComponents();
    }
}
