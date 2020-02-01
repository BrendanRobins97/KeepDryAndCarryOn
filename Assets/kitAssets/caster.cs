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
            castBoot(myTopLeft.transform.localPosition.x, myBottomRight.transform.localPosition.x, myBottomRight.transform.localPosition.y, myTopLeft.transform.localPosition.y);
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
        if (xMin > xMax)
        {
            float temp = xMin;
            xMin = xMax;
            xMax = temp;
        }
        transform.localPosition = new Vector3(xMin, yMin, transform.localPosition.z);
        StartCoroutine(cast(xMin, xMax, yMin, yMax));
    }
    int returnHoles(int myHoles, int myPlugs)
    {
        print(myHoles);// + " "+ holes);
        corDone = true;
        globals.globalHoles += myHoles;
        print(globals.globalHoles);
        return (myHoles);
    }
    IEnumerator cast(float xMin, float xMax, float yMin, float yMax)
    {
        globals.doneChecker += 1;
        var timer = new System.Timers.Timer(16.666666666666667);//this is a frame at 60 fps
        timer.Elapsed += (s, e) => timer.Stop();
        timer.Start();
        //timer += Time.deltaTime;
        //print("beep");
        //print(transform.localPosition.x);
        //print(xMax);
        while (transform.localPosition.x < xMax)
        {
            //print("x");
            while (transform.localPosition.y < yMax)
            {
                if (!timer.Enabled)
                {
                    yield return null;
                    timer.Start();
                }
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
                {
                    //Debug.DrawRay(transform.localPosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
                    //Debug.Log(hit.collider.gameObject.name);
                    if (hit.collider.gameObject.tag == "holecheck")
                        holes += 1;
                    else
                    {
                        plugs += 1;
                    }
                }
                transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + interval, transform.localPosition.z);
            }
            transform.localPosition = new Vector3(transform.localPosition.x, yMin, transform.localPosition.z);
            transform.localPosition = new Vector3(transform.localPosition.x + interval, transform.localPosition.y, transform.localPosition.z);
        }
        globals.doneChecker -= 1;
        returnHoles(holes, plugs);
        //RaycastHit hit;
        //if (Physics.Raycast(transform.localPosition, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        //{
        //    Debug.DrawRay(transform.localPosition, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
        //    Debug.Log(hit.collider.gameObject.name);
        //    if (hit.collider.gameObject.name == "holeTrigger")
        //        holes += 1;
        //}
        //transform.localPosition = new Vector3(transform.localPosition.x + .2f, transform.localPosition.y, transform.localPosition.z);
        //if (transform.localPosition.x > xMax)
        //{
        //    transform.localPosition = new Vector3(xMin, transform.localPosition.y, transform.localPosition.z);
        //    transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y + .2f, transform.localPosition.z);
        //    if (transform.localPosition.y > yMax)
        //    {
        //        return holes;
        //    }
        //}
        //cast(xMin, xMax, yMin, yMax);
        //return (holes);
    }
}
