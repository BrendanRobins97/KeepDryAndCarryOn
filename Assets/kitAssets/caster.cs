using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caster : MonoBehaviour
{
    public bool rdy;
    public int holes;
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
            castBoot(-2.5f, 6.5f, 2.25f, 5.0f);
        }
        else if (Input.GetAxis("Fire3") == 0.0)
        {
            rdy = true;
        }
    }
    void castBoot(float xMin, float xMax, float yMin, float yMax)
    {
        holes = 0;
        transform.position = new Vector3(xMin, yMin, transform.position.z);
        cast(xMin, yMin, xMax, yMax);
        print(holes);
    }
    int cast(float xMin, float xMax, float yMin, float yMax)
    {
        //print("beep");
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                //Debug.Log(hit.collider.gameObject.name);
                if (hit.collider.gameObject.name == "holeTrigger")
                 holes += 1;
            }
        transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
        if(transform.position.x > xMax)
        {
            transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
            transform.position = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
            if (transform.position.y> yMax)
            {
                return holes;
            }
        }
        cast(xMin, xMax, yMin, yMax);
        return(holes);
    }
}
