using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float distanceFromHand = 1f;
    private Rigidbody rb;
    private Transform snapTarget;
    private Vector3 prevPosition;
    private Vector3 velocity;
    private Vector3 originalRotation;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalRotation = transform.eulerAngles;
    }

    private void Update()
    {

        if (snapTarget)
        {
            transform.position = Vector3.Lerp(transform.position, snapTarget.position + snapTarget.forward * distanceFromHand, 8 * Time.deltaTime);
            transform.rotation = snapTarget.rotation;
            transform.Rotate(originalRotation);
        }
        velocity = (transform.position - prevPosition) / Time.deltaTime;

        prevPosition = transform.position;


    }

    public void Grab(Transform grabber)
    {
        originalRotation = transform.eulerAngles;
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
