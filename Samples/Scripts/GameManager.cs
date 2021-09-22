using System;
using UnityEngine;
using ZSerializer;

public class GameManager : PersistentMonoBehaviour
{
    public int highScore;
    public int currentScore;
    public string playerName;
    public Vector3 position;

    public BallMover ballMover;

    public void MySaveFunction()
    {
        
        ZSerialize.SaveAll();
        ZSerialize.LoadAll();
        
    }
    
    public void MyLoadFunction()
    {
        ZSerialize.LoadAll(ZSerialize.NameToSaveGroupID("Settings"));
    }
    
    public void MyAddComponentFunction()
    {
        GetComponent<PersistentGameObject>().AddComponent<BoxCollider>(PersistentType.Component);
    }

    private void Start()
    {
        ballMover = FindObjectOfType<BallMover>();
        // MySaveFunction();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            ZSerialize.SaveAll();
        }

        if (GUILayout.Button("Load"))
        {
            ZSerialize.LoadAll();
        }

        if (GUILayout.Button("Destroy Player"))
        {
            Destroy(ballMover.gameObject);
        }

        if (ballMover && ballMover.rb)
        {
            GUILayout.Label("Velocity: " + ballMover.rb.velocity);
        }
    }
}