using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    #region Variables
    Camera cam;
    Transform camPivot;
    CharacterController charCon;

    Animator camAnim;
    
    float movementSpeed = 5f;
    float sprintScale = 1.5f;
    float cameraHorizontalSpeed = 3f;
    float cameraVerticalSpeed = 2f;
    public bool invert = true;
    public float jumpForce = 100;
    private float verticalVelocity = 0;

    Vector3 movement;
    #endregion
    #region Event Functions
    private void Awake() {
        charCon = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        camAnim = cam.GetComponent<Animator>();
        camPivot = cam.transform.parent;
    }
    void Update(){
        MovementInput();
        CameraInput();
        ApplyMovement();
        if(Input.GetKeyDown(KeyCode.Escape)){
            Cursor.lockState = CursorLockMode.None;
            GetComponent<LoadScene>().Load();
        }
    }
    #endregion
    #region Input
    void MovementInput(){
        movement = Vector3.zero;
        if(Input.GetKey(KeyCode.W))
            movement += transform.forward;
        if(Input.GetKey(KeyCode.A))
            movement -= transform.right;
        if(Input.GetKey(KeyCode.S))
            movement -= transform.forward;
        if(Input.GetKey(KeyCode.D))
            movement += transform.right;
        movement.Normalize();
        if(Input.GetKeyDown(KeyCode.Space)){
            RaycastHit hit;
            if (Physics.Raycast(transform.position, Vector3.down, out hit, 1.5f))
            {
                 verticalVelocity = jumpForce;
            }
        }
    }
    void CameraInput(){
        if(Input.GetKey(KeyCode.Mouse1))
            return;
        float horizontal = Input.GetAxisRaw("Mouse X");
        float vertical = Input.GetAxisRaw("Mouse Y");
        if(invert)
            vertical*=-1;
        transform.Rotate(0,horizontal*cameraHorizontalSpeed,0);
        
        camPivot.transform.Rotate(vertical*cameraVerticalSpeed,0,0);
        float angle = camPivot.transform.eulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;
        camPivot.transform.localEulerAngles = new Vector3( Mathf.Clamp( angle, -80, 80), 0, 0);
        
    }
    #endregion
    #region Physics
    void ApplyMovement(){
        float speed = movementSpeed;
        float y = transform.position.y;
        if(Input.GetKey(KeyCode.LeftShift))
            speed *= sprintScale;
        charCon.SimpleMove(movement*speed);
        if(verticalVelocity > 0){
            y = y - transform.position.y;
            charCon.Move(Vector3.up * y);
            charCon.Move(Vector3.up * verticalVelocity*Time.deltaTime);
            verticalVelocity = Mathf.Max(verticalVelocity - Physics.gravity.magnitude*Time.deltaTime,0);
        }
        
        camAnim.SetFloat("Speed", movement.magnitude*(speed/movementSpeed));
    }
    #endregion
}
