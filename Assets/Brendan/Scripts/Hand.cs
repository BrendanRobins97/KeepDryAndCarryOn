
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections.Generic;

public class Hand : MonoBehaviour
{
    public Transform guardian;
    public bool right;
    public Animator handAnimator;
    public float rotateForce = 1f;
    private GameObject handToFollow;
    private List<PhysicsObject> targets = new List<PhysicsObject>();
    private PhysicsObject closestTarget;
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
        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] == null)
            {
                targets.RemoveAt(i);
            }
        }
        GetClosestTarget();
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (!collision.attachedRigidbody)
        {
            return;
        }
        PhysicsObject obj = collision.attachedRigidbody.GetComponent<PhysicsObject>();
        if (obj)
        {
            targets.Add(obj);
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (!collision.attachedRigidbody)
        {
            return;
        }
        PhysicsObject obj = collision.attachedRigidbody.GetComponent<PhysicsObject>();
        if (obj && targets.Contains(obj))
        {
            obj.Untarget();
            targets.Remove(obj);
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
        if ((obj.state.source.handedness == InteractionSourceHandedness.Left && right)
            || (obj.state.source.handedness == InteractionSourceHandedness.Right && !right))
        {
            return;
        }

        if (obj.pressType == InteractionSourcePressType.Select)
        {
            handAnimator.SetBool("Grabbed", true);
            grabbedObject = closestTarget;
            grabbedObject?.Grab(transform);
        }
    }

    private void GetClosestTarget()
    {
        grabbedObject?.Target();
        if (targets.Count <= 0)
        {
            closestTarget = null;
            return;
        }
        closestTarget = targets[0];
        if (targets.Count > 1)
        {
            for (int i = 1; i < targets.Count; i++)
            {
                if ((targets[i].transform.position - transform.position).sqrMagnitude < (closestTarget.transform.position - transform.position).sqrMagnitude)
                {
                    closestTarget = targets[i];

                }
            }
        }

        for (int i = 0; i < targets.Count; i++)
        {
            if (targets[i] != closestTarget && targets[i] != grabbedObject)
            {
                targets[i].Untarget();
            } else
            {
                targets[i].Target();
            }
        }
    }
    private void InteractionSourceUpdated(InteractionSourceUpdatedEventArgs obj)
    {
        if ((obj.state.source.handedness == InteractionSourceHandedness.Left && right)
            || (obj.state.source.handedness == InteractionSourceHandedness.Right && !right))
        {
            return;
        }
        if (obj.state.source.handedness == InteractionSourceHandedness.Left)
        {
            if (obj.state.thumbstickPosition.magnitude > 0.25f)
            {
                Vector3 cameraDirection = Camera.main.transform.TransformDirection(new Vector3(obj.state.thumbstickPosition.x, 0, obj.state.thumbstickPosition.y));
                Vector3 direction = new Vector3(cameraDirection.x, 0, cameraDirection.z);
                guardian.Translate(new Vector3(2 * Time.deltaTime * direction.x, 0, 2 * Time.deltaTime * direction.z), Space.Self);
            }
        }
        if (obj.state.source.handedness == InteractionSourceHandedness.Right)
        {
            if (grabbedObject)
            {
                if (obj.state.thumbstickPosition.magnitude > 0.25f)
                {
                    float horizontal = obj.state.thumbstickPosition.x;
                    float vertical = obj.state.thumbstickPosition.y;
                    Vector3 torque = new Vector3(0, -horizontal * 1, 0);
                    torque += vertical * transform.right * 1;
                    grabbedObject.GetComponent<Rigidbody>().AddTorque(torque);
                }
                else
                {
                    grabbedObject.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
                }
            }
            
        }
    }

    private void InteractionSourceReleased(InteractionSourceReleasedEventArgs obj)
    {
        if ((obj.state.source.handedness == InteractionSourceHandedness.Left && right)
            || (obj.state.source.handedness == InteractionSourceHandedness.Right && !right))
        {
            return;
        }
        if (obj.pressType == InteractionSourcePressType.Select)
        {
            handAnimator.SetBool("Grabbed", false);
            if (grabbedObject != null)
            {
                grabbedObject.Untarget();

                grabbedObject.Drop();
                grabbedObject = null;
            }
        }
    }


}
