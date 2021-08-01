using System;
using UnityEngine;
using ZSerializer;
[Persistent]
public class GameManager : MonoBehaviour
{
    int highScore;
    int currentScore;
    string playerName;

    public BallMover ballMover;

    private void Start()
    {
        ballMover = FindObjectOfType<BallMover>();
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            ZSave.SaveAll();
        }

        if (GUILayout.Button("Load"))
        {
            ZSave.LoadAll();
        }

        if (GUILayout.Button("Destroy Player"))
        {
            Destroy(ballMover.gameObject);
        }

        if (ballMover)
        {
            GUILayout.Label("Velocity: " + ballMover.rb.velocity);
        }
    }
}