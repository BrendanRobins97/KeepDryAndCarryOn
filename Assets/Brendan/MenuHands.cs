
using UnityEngine;
using UnityEngine.XR.WSA.Input;
using UnityEngine.UI;

public class MenuHands : MonoBehaviour
{
    public bool right;
    
    private GameObject handToFollow;
    private PhysicsObject grabbedObject;
    private Button target;

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
        }
        else
        {
            handToFollow = GameObject.Find("Left_Left_GizmoLeft(Clone)");
            if (handToFollow)
            {
                transform.SetPositionAndRotation(handToFollow.transform.position, handToFollow.transform.rotation);

            }
        }
        Raycast();
    }
    private void Raycast()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit))
        {
            if (hit.collider.gameObject.GetComponent<Button>())
            {
                target = hit.collider.gameObject.GetComponent<Button>();
            }
        }
    }


    // Start is called before the first frame update
    void OnEnable()
    {
        InteractionManager.InteractionSourcePressed += InteractionSourcePressed;

    }

    private void OnDisable()
    {
        InteractionManager.InteractionSourcePressed -= InteractionSourcePressed;
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
            target?.onClick.Invoke();
        }
    }


 


}
