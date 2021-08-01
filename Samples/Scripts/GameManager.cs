using System;
using UnityEngine;
using ZSerializer;

[Persistent]
public class GameManager : MonoBehaviour
{
    public int highScore;
    public int currentScore;
    public string playerName;
    public Vector3 position;

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

        if (ballMover && ballMover.rb)
        {
            GUILayout.Label("Velocity: " + ballMover.rb.velocity);
        }
    }
}