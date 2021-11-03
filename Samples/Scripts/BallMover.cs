using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using ZSerializer;

// [Persistent]
public class BallMover : PersistentMonoBehaviour
{
    internal Rigidbody rb;
    private Vector3 movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        movement = Quaternion.Euler(0, 45, 0) * new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        if (transform.position.y < -10)
        {
            rb.velocity = Vector3.zero;
            transform.position = new Vector3(0, 0, 0);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(movement.normalized*5);
    }

#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (Application.isPlaying && rb.velocity.sqrMagnitude > .1f)
        {
            Handles.color = Color.cyan;

            Handles.ArrowHandleCap(0, transform.position,
                Quaternion.LookRotation(rb.velocity), Mathf.Min(rb.velocity.sqrMagnitude*.5f,3), EventType.Repaint);
        }
    }
#endif
}