using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class globals : MonoBehaviour
{
    public static int globalHoles;
    public static int doneChecker = 0;
    public static UnityEvent FireEvent;
    // Start is called before the first frame update
    public GameObject[] holeZones;
    private void Awake()
    {
        FireEvent = new UnityEvent();
    }

    void Start()
    {
        //if (holeZones == null)
        holeZones = GameObject.FindGameObjectsWithTag("holecheck");
        //print(holeZones.Length);
        //print(doneChecker);
    }

    // Update is called once per frame
    void Update()
    {
        //print(doneChecker);
    }

    public void FireFunction()
    {
        FireEvent.Invoke();
    }
}
