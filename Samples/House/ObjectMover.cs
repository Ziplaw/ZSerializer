using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZSerializer;

public class ObjectMover : MonoBehaviour
{
    public Camera cam;
    public Rigidbody movingRb;
    public LayerMask mask;
    private Vector3 rhit;

    private void Start()
    {
        cam = Camera.main;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out var hit, mask))
            {
                var rb = hit.transform.GetComponent<Rigidbody>();
                if (rb)
                {
                    rb.isKinematic = false;
                    rb.useGravity = false;
                    movingRb = rb;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (movingRb)
            {
                movingRb.useGravity = true;
                movingRb = null;
            }
        }

        
    }

    private void FixedUpdate()
    {
        if (movingRb)
        {
            //unity mouse position to world point
            var ray = cam.ScreenPointToRay(Input.mousePosition);
            Physics.Raycast(ray, out var hit, mask);
            if (hit.transform)
            {
                rhit = hit.point;
                movingRb.velocity = hit.point - movingRb.transform.position;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (movingRb)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(rhit, 0.1f);
        }
    }
}
