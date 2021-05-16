using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSave;

public class UIPersistantBridge : MonoBehaviour
{
    public void Save(int groupID)
    {
        // PersistentAttribute.SaveAllObjects(groupID);
        PersistanceManager.SaveAllObjectsAndComponents();
    }
    
    public void Load(int groupID)
    {
        // PersistentAttribute.LoadAllObjects(groupID);
        PersistanceManager.LoadAllObjectsAndComponents();
        PersistanceManager.SaveAllObjectsAndComponents();

    }
}
