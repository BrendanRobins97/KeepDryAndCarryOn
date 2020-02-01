using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class waterRise : MonoBehaviour
{
    public GameObject myObject;
    public bool rdy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire3") > 0.0 && rdy == true)
        {
            rdy = false;
            Rise(4.0f);
        }
        else if (Input.GetAxis("Fire3") == 0.0)
        {
            rdy = true;
        }
    }
    void Rise(float val)
    {
        transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y + val, transform.localScale.z);
        transform.position = new Vector3(transform.position.x, transform.position.y + (val/2), transform.position.z);
    }
}
