using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class MainMenu : MonoBehaviour
{
    public void LoadLastSavedScene()
    {
        ZSerialize.LoadSceneGroup("Home", out _);
    }
}
