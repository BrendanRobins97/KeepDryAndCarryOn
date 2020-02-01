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
                    currentItem = hit.transform;
                    currentItem.GetComponent<Rigidbody>().useGravity = false;
                    currentItem.GetComponent<Rigidbody>().drag = 5f;
                }
            }
        }else{
            currentItem.GetComponent<Rigidbody>().useGravity = true;
            currentItem.GetComponent<Rigidbody>().drag = 0f;
            currentItem = null;
        }
    }
    void RotateInput(){
        if(Input.GetKeyDown(KeyCode.Mouse1)||Input.GetKeyUp(KeyCode.Mouse1)){
            if(currentItem){
                currentItem.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;
            }
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
