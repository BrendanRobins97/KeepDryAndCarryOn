using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{

    private Rigidbody rb;
    private Transform snapTarget;
    private Vector3 prevPosition;
    private Vector3 velocity;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {

        if (snapTarget)
        {
            transform.position = Vector3.Lerp(transform.position, snapTarget.position + snapTarget.forward / 2f, 8 * Time.fixedDeltaTime);
            transform.rotation = snapTarget.rotation;
        }
        velocity = (transform.position - prevPosition) / Time.fixedDeltaTime;

        prevPosition = transform.position;


    }

    public void Grab(Transform grabber)
    {
        snapTarget = grabber;
        rb.useGravity = false;
        rb.isKinematic = true;
    }

    public void Drop()
    {
        snapTarget = null;
        rb.useGravity = true;
        rb.isKinematic = false;
        rb.velocity = velocity;
    }
}
