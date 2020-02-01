using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class globals : MonoBehaviour
{
    public static int globalHoles;
    // Start is called before the first frame update
    public GameObject[] holeZones;
    void Start()
    {
        //if (holeZones == null)
        holeZones = GameObject.FindGameObjectsWithTag("holecheck");
        print(holeZones.Length);
    }

    // Update is called once per frame
    void Update()
    {

    }
    //IEnumerator checkAllWalls()
    //{
    //    print("lock");
    //}
}
