using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsObject : MonoBehaviour
{
    public float distanceFromHand = 1f;
    private Rigidbody rb;
    private Transform snapTarget;
    private Vector3 prevPosition;
    private Vector3 prevRotation;

    private Vector3 velocity;
    private Vector3 angularVelocity;

    private bool glued = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        
        if (snapTarget)
        {
            transform.position = Vector3.Lerp(transform.position, snapTarget.position + snapTarget.forward * distanceFromHand, 8 * Time.deltaTime);
            //transform.rotation = snapTarget.rotation;
            //Quaternion.RotateTowards()
            angularVelocity = (snapTarget.eulerAngles - prevRotation) / Time.deltaTime;
            prevRotation = snapTarget.eulerAngles;
        }
        velocity = (transform.position - prevPosition) / Time.deltaTime;

        prevPosition = transform.position;

        foreach (var renderer in GetComponentsInChildren<MeshRenderer>()   )
        {
            foreach (Material material in renderer.materials)
            {
                material.SetFloat("_Glue", glued ? 0.9f : 0);
            }
        }

    }

    public void Grab(Transform grabber)
    {
        //originalRotation = transform.eulerAngles;
        snapTarget = grabber;
        rb.useGravity = false;
    }

    public void Drop()
    {
        snapTarget = null;
        rb.useGravity = true;
        rb.velocity = velocity;
        rb.angularVelocity = angularVelocity;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Glue"))
        {
            glued = true;
            Destroy(other.gameObject);
        }
    }

}
