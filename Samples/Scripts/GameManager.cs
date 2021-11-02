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

    public override void OnPostLoad()
    {
        ballMover = FindObjectOfType<BallMover>();
    }

    private new void Start()
    {
        base.Start();
        ballMover = FindObjectOfType<BallMover>();
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