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
    public float minZoom = 1;
    public float maxZoom = 5;

    [SerializeField]
    NailInventory nailUI;
    AudioSource sound;
    private int nails = 0;
    private void Update() {
        GrabInput();
        RotateInput();
        ZoomInput();
        NailInput();
        MoveItem();
    }
    private void Awake() {
        camOrigin = GetComponentInChildren<Camera>().transform.parent;
        sound = GetComponent<AudioSource>();
        grabPoint.localPosition = new Vector3(grabPoint.localPosition.x, grabPoint.localPosition.y, minZoom);
    }
    void ZoomInput(){
        float newZ = Mathf.Clamp(grabPoint.localPosition.z + Input.GetAxis("Mouse ScrollWheel"), minZoom, maxZoom);
        grabPoint.localPosition = new Vector3(grabPoint.localPosition.x, grabPoint.localPosition.y, newZ);
    }
    void GrabInput(){
        if(!Input.GetKeyDown(KeyCode.Mouse0))
            return;
        if(currentItem == null){
            RaycastHit hit;
            Vector3 dir = (grabPoint.position - camOrigin.position).normalized;
            if (Physics.Raycast(camOrigin.position, dir, out hit, 4))
            {
                if(hit.transform.tag == "Glue"){
                    if(nails == 10)
                        return;
                    AddNail();
                    Destroy(hit.transform.gameObject);
                }
                else if(hit.transform.GetComponent<Rigidbody>()){
                    rightHandAnim.SetBool("Grabbed", true);
                    currentItem = hit.transform;
                    currentItem.GetComponent<Rigidbody>().useGravity = false;
                    currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
                    currentItem.GetComponent<Rigidbody>().drag = 5f;
                    if(currentItem.GetComponent<AudioSource>()){
                        currentItem.GetComponent<AudioSource>().Play();
                    }
                }
            }
        }else{
            currentItem.GetComponent<Rigidbody>().useGravity = true;
            currentItem.GetComponent<Rigidbody>().drag = 0f;
            if(currentItem.GetComponent<AudioSource>()){
                currentItem.GetComponent<AudioSource>().Stop();
            }
            currentItem = null;
            rightHandAnim.SetBool("Grabbed", false);
            rightHandAnim.SetBool("Rotating", false);
            leftHandAnim.SetBool("Rotating", false);
            sound.Stop();
            
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
            if(!sound.isPlaying)
                sound.Play();
        }else{
            leftHandAnim.SetBool("Rotating", false);
            rightHandAnim.SetBool("Rotating", false);
            sound.Stop();
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

    void NailInput(){
        if(!Input.GetKeyDown(KeyCode.F))
            return;
        if(currentItem == null)
            return;
        if(nails <1)
            return;
        if(!currentItem.GetComponent<MKObject>().isTouching())
            return;
        currentItem.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
        currentItem.GetComponent<Rigidbody>().drag = 0f;
        currentItem = null;
        rightHandAnim.SetBool("Grabbed", false);
        rightHandAnim.SetBool("Rotating", false);
        leftHandAnim.SetBool("Rotating", false);
        sound.Stop();
        SubtractNail();
    }
    public void AddNail(){
        nails++;
        nailUI.Add();
    }
    public void SubtractNail(){
        nails--;
        nailUI.Subtract();
    }
    public int GetNails(){
        return nails;
    }
}
