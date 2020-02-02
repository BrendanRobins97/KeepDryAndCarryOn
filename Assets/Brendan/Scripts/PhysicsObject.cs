
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
    private bool stuck = false;
    [HideInInspector]
    public bool grabbed = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        if (snapTarget)
        {
            transform.position = Vector3.Lerp(transform.position, snapTarget.position + snapTarget.forward * distanceFromHand, 8 * Time.fixedDeltaTime);
            //transform.position = Vector3.Lerp(transform.position, snapTarget.position + snapTarget.forward * distanceFromHand, 2 * Time.deltaTime);
            rb.velocity = Vector3.zero;
            //transform.rotation = snapTarget.rotation;
            //Quaternion.RotateTowards()
            angularVelocity = (snapTarget.eulerAngles - prevRotation) / Time.fixedDeltaTime;
            prevRotation = snapTarget.eulerAngles;
        }
        velocity = (transform.position - prevPosition) / Time.fixedDeltaTime;

        prevPosition = transform.position;

        foreach (var renderer in GetComponentsInChildren<MeshRenderer>())
        {
            foreach (Material material in renderer.materials)
            {
                material.SetFloat("_Glue", glued ? 1f : 0);
            }
        }
        if (!grabbed)
        {
            if (glued && objectsCollidingWith.Count > 0)
            {
                rb.useGravity = false;
                rb.isKinematic = true;
            }
        }

    }

    public void Target()
    {
        foreach (Renderer outline in GetComponentsInChildren<Renderer>())
        {
            foreach (Material material in outline.materials)
            {
                material.SetFloat("_Highlight", 1);

            }
        }
    }

    public void Untarget()
    {
        foreach (Renderer outline in GetComponentsInChildren<Renderer>())
        {
            foreach (Material material in outline.materials)
            {
                material.SetFloat("_Highlight", 0);

            }
        }
    }


    public void Grab(Transform grabber)
    {
        //originalRotation = transform.eulerAngles;
        snapTarget = grabber;
        rb.useGravity = false;
        grabbed = true;
        rb.isKinematic = false;
    }

    public void Drop()
    {
        snapTarget = null;
        if (glued && objectsCollidingWith.Count > 0)
        {
            rb.useGravity = false;
            rb.isKinematic = true;
        }
        else
        {
            rb.isKinematic = false;
            rb.useGravity = true;
            rb.velocity = velocity * 1.1f;
            rb.angularVelocity = angularVelocity;
        }
        grabbed = false;
    }

    private List<GameObject> objectsCollidingWith = new List<GameObject>();
    private void OnCollisionEnter(Collision other)
    {
        if (!gameObject.CompareTag("Glue") && other.rigidbody && other.rigidbody.CompareTag("Glue") && other.rigidbody.GetComponent<PhysicsObject>().grabbed)
        {
            glued = true;
            Destroy(other.gameObject.GetComponent<PhysicsObject>());
            Destroy(other.gameObject);
        } else
        {
            objectsCollidingWith.Add(other.gameObject);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        objectsCollidingWith.Remove(collision.gameObject);
    }

}
