using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MKObject : MonoBehaviour
{
    bool touching = false;


    private void OnCollisionExit() {
        touching = false;
    }
 
    private void OnCollisionStay(Collision other){
        if(other.transform.tag != "Object")
            touching = true;
    }
    public bool isTouching(){
        return touching;
    }
}
