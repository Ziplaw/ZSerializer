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
    public GameObject canvas;

    // public override void OnPostLoad()
    // {
    //     ballMover = FindObjectOfType<BallMover>();
    // }

    private void Start()
    {
        ballMover = FindObjectOfType<BallMover>();
    }


    private async void OnGUI()
    {
        if (GUILayout.Button("Save"))
        {
            canvas.SetActive(true);
            await ZSerialize.SaveAll();
            canvas.SetActive(false);
            return;
        }

        if (GUILayout.Button("Load"))
        {
            canvas.SetActive(true);
            await ZSerialize.LoadAll();
            canvas.SetActive(false);
            return;
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