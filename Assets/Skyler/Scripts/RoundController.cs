using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public bool debug = false;
    public int debugFillPoints = 20000;
    [Tooltip("The amount of time in seconds before a round ends.")]
    public float roundLength = 60f;

    [SerializeField]
    private int roundsSurvived = 0;

    private bool betweenRounds = false;

    private WaterFillController fillCon;
    // private Timer timer;
    // private Raycaster caster;

    private void Start()
    {
        fillCon = gameObject.GetComponent<WaterFillController>();
        // timer = gameObject.GetComponent<Timer>();
        // caster = gameObject.GetComponent<Raycaster>();
    }

    private void Update()
    {
        // Check if the timer has reached time
        if(Input.GetKeyDown(KeyCode.M) && debug && !betweenRounds)
        {
            betweenRounds = true;
            // pause the timer
            // Call raycaster to get number of fill points

            //if()
            fillCon.BeginFilling(debugFillPoints);
            roundsSurvived++;
            betweenRounds = false;
        }
    }
}
