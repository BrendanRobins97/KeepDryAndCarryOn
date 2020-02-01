
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public Transform guardian;
    public bool right;

    private GameObject handToFollow;
    private PhysicsObject target;
    private PhysicsObject grabbedObject;
    private bool foundHand;
    private bool canRotate = true;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (right)
        {
            handToFollow = GameObject.Find("Right_Right_GizmoRight(Clone)");
            if (handToFollow)
            {
                transform.SetPositionAndRotation(handToFollow.transform.position, handToFollow.transform.rotation);

            }
        } else
        {
            handToFollow = GameObject.Find("Left_Left_GizmoLeft(Clone)");
            if (handToFollow)
            {
                transform.SetPositionAndRotation(handToFollow.transform.position, handToFollow.transform.rotation);

            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Object"))
        {
            Debug.Log("target acquired");
            target = collision.GetComponent<PhysicsObject>();
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.CompareTag("Object") && collision.GetComponent<PhysicsObject>() == target)
        {
            target = null;
        }
    }

    // Start is called before the first frame update
    void OnEnable()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased += InteractionSourceReleased;
        InteractionManager.InteractionSourceUpdated += InteractionSourceUpdated;

    }

    private void OnDisable()
    {
        InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
        InteractionManager.InteractionSourceReleased -= InteractionSourceReleased;
        InteractionManager.InteractionSourceUpdated -= InteractionSourceUpdated;
    }

    private void InteractionSourcePressed(InteractionSourcePressedEventArgs obj)
    {
        if (obj.pressType == InteractionSourcePressType.Select)
        {
            grabbedObject = target;
            grabbedObject?.Grab(transform);
        }

    }

    private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        if (obj.state.source.handedness == InteractionSourceHandedness.Left)
        {
            if (obj.state.thumbstickPosition.magnitude > 0.25f)
            {
                guardian.Translate(new Vector3(1 * Time.deltaTime * obj.state.thumbstickPosition.x, 0, 1 * Time.deltaTime * obj.state.thumbstickPosition.y), Space.Self);
            }
        }
        if (obj.state.source.handedness == InteractionSourceHandedness.Right)
        {
            if (obj.state.thumbstickPosition.x > 0.5f)
            {
                if (canRotate)
                {
                    canRotate = false;
                    guardian.RotateAround(transform.position, Vector3.up, 45f);
                }
            }
            else if (obj.state.thumbstickPosition.x < -0.5f)
            {
                if (canRotate)
                {
                    canRotate = false;
                    guardian.RotateAround(transform.position, Vector3.up, -45f);
                }

            } else
            {
                canRotate = true;
            }
        }
    }

    private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
    {
        if (obj.pressType == InteractionSourcePressType.Select)
        {
            grabbedObject?.Drop();
        }
    }


}
