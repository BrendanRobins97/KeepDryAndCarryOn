using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globals : MonoBehaviour
{
    public static int globalHoles;
    public static int doneChecker = 0;
    // Start is called before the first frame update
    public GameObject[] holeZones;
    void Start()
    {
        //if (holeZones == null)
        holeZones = GameObject.FindGameObjectsWithTag("holecheck");
        print(holeZones.Length);
        print(doneChecker);
    }

    // Update is called once per frame
    void Update()
    {
        //print(doneChecker);
    }
    //IEnumerator checkAllWalls()
    //{
    //    print("lock");
    //}
}
