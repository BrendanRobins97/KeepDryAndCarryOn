using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardMovement : MonoBehaviour
{
    #region Variables
    Camera cam;
    CharacterController charCon;
    
    float movementSpeed = 5f;
    float sprintScale = 1.5f;
    float cameraHorizontalSpeed = 3f;
    float cameraVerticalSpeed = 2f;
    public bool invert = true;
    Vector3 movement;
    #endregion
    #region Event Functions
    private void Awake() {
        charCon = GetComponent<CharacterController>();
        cam = GetComponentInChildren<Camera>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
    }
    void CameraInput(){
        float horizontal = Input.GetAxisRaw("Mouse X");
        float vertical = Input.GetAxisRaw("Mouse Y");
        if(invert)
            vertical*=-1;
        transform.Rotate(0,horizontal*cameraHorizontalSpeed,0);
        
        cam.transform.Rotate(vertical*cameraVerticalSpeed,0,0);
        float angle = cam.transform.eulerAngles.x;
        angle = (angle > 180) ? angle - 360 : angle;
        cam.transform.localEulerAngles = new Vector3( Mathf.Clamp( angle, -60, 60), 0, 0);
        
    }
    #endregion
    #region Physics
    void ApplyMovement(){
        float speed = movementSpeed;
        if(Input.GetKey(KeyCode.LeftShift))
            speed *= sprintScale;
        charCon.SimpleMove(movement*speed);
    }
    #endregion
}
