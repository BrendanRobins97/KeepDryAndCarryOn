using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caster : MonoBehaviour
{
    public bool rdy;
    public int holes;
    public int plugs;
    public bool corDone;
    public GameObject myTopLeft;
    public GameObject myBottomRight;
    //public float timer;
    public float interval;
    // Start is called before the first frame update
    void Start()
    {
        interval = .01f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire3") > 0.0 && rdy == true)
        {
            rdy = false;
            castBoot(myTopLeft.transform.position.x, myBottomRight.transform.position.x, myBottomRight.transform.position.y, myTopLeft.transform.position.y);
        }
        else if (Input.GetAxis("Fire3") == 0.0)
        {
            rdy = true;
        }
    }
    void castBoot(float xMin, float xMax, float yMin, float yMax)
    {
        holes = 0;
        plugs = 0;
        corDone = false;
        transform.position = new Vector3(xMin, yMin, transform.position.z);
        StartCoroutine(cast(xMin, xMax, yMin, yMax));
    }
    int returnHoles(int myHoles, int myPlugs)
    {
        print(myHoles+ " "+ holes);
        corDone = true;
        return (myHoles);
    }
    IEnumerator cast(float xMin, float xMax, float yMin, float yMax)
    {
        var timer = new System.Timers.Timer(16.666666666666667);
        timer.Elapsed += (s, e) => timer.Stop();
        timer.Start();
        //timer += Time.deltaTime;
        //print("beep");
        //print(transform.position.x);
        //print(xMax);
        while (transform.position.x < xMax)
        {
            print("x");
            while (transform.position.y < yMax)
            {
                if (!timer.Enabled)
                {
                    yield return null;
                    timer.Start();
                }
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    //Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "hole")
                        holes += 1;
                    else
                    {
                        plugs += 1;
                    }
                }
                transform.position = new Vector3(transform.position.x, transform.position.y + interval, transform.position.z);
            }
            transform.position = new Vector3(transform.position.x, yMin, transform.position.z);
            transform.position = new Vector3(transform.position.x + interval, transform.position.y, transform.position.z);
        }
        returnHoles(holes, plugs);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        //{
        //    Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //    Debug.Log(hit.collider.gameObject.name);
        //    if (hit.collider.gameObject.name == "holeTrigger")
        //        holes += 1;
        //}
        //transform.position = new Vector3(transform.position.x + .2f, transform.position.y, transform.position.z);
        //if (transform.position.x > xMax)
        //{
        //    transform.position = new Vector3(xMin, transform.position.y, transform.position.z);
        //    transform.position = new Vector3(transform.position.x, transform.position.y + .2f, transform.position.z);
        //    if (transform.position.y > yMax)
        //    {
        //        return holes;
        //    }
        //}
        //cast(xMin, xMax, yMin, yMax);
        //return (holes);
    }
}
