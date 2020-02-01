using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KMPickup : MonoBehaviour
{
    private Transform currentItem;
    [SerializeField]
    private Transform grabPoint;
    private Transform camOrigin;
    public float grabForce = 10f;
    public float rotateForce = 1f;
    [SerializeField]
    Animator leftHandAnim;
    [SerializeField]
    Animator rightHandAnim;
    private void Update() {
        GrabInput();
        RotateInput();
        MoveItem();
        
    }
    private void Awake() {
        camOrigin = GetComponentInChildren<Camera>().transform.parent;
    }
    void GrabInput(){
        if(!Input.GetKeyDown(KeyCode.Mouse0))
            return;
        if(currentItem == null){
            RaycastHit hit;
            Vector3 dir = (grabPoint.position - camOrigin.position).normalized;
            if (Physics.Raycast(camOrigin.position, dir, out hit, 2))
            {
                print(currentItem);
                if(hit.transform.GetComponent<Rigidbody>()){
                    rightHandAnim.SetBool("Grabbed", true);
                    currentItem = hit.transform;
                    currentItem.GetComponent<Rigidbody>().useGravity = false;
                    currentItem.GetComponent<Rigidbody>().drag = 5f;
                }
            }
        }else{
            currentItem.GetComponent<Rigidbody>().useGravity = true;
            currentItem.GetComponent<Rigidbody>().drag = 0f;
            currentItem = null;
            rightHandAnim.SetBool("Grabbed", false);
        }
    }
    void RotateInput(){
        if(currentItem == null)
            return;
        if(Input.GetKeyDown(KeyCode.Mouse1)||Input.GetKeyUp(KeyCode.Mouse1)){
            if(currentItem){
                currentItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
        }
        if(Input.GetKey(KeyCode.Mouse1)){
            float horizontal = Input.GetAxisRaw("Mouse X");
            float vertical = Input.GetAxisRaw("Mouse Y");
            Vector3 torque = new Vector3(0,-horizontal * rotateForce,0);
            torque += vertical * camOrigin.right*rotateForce;
            currentItem.GetComponent<Rigidbody>().AddTorque(torque);
            leftHandAnim.SetBool("Rotating", true);
            rightHandAnim.SetBool("Rotating", true);
        }else{
            leftHandAnim.SetBool("Rotating", false);
            rightHandAnim.SetBool("Rotating", false);
        }
    }
    void MoveItem(){
        if(currentItem == null)
            return;
        Rigidbody rb = currentItem.GetComponent<Rigidbody>();
        Vector3 dir = grabPoint.transform.position - currentItem.transform.position;
        float distance = dir.magnitude;
        dir.Normalize();
        rb.AddForce(dir*distance* distance *grabForce);
    }
}
