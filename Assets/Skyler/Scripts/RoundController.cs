using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundController : MonoBehaviour
{
    public bool debug = false;
    public int debugFillPoints = 20000;
    [Tooltip("The amount of time in seconds before a round ends.")]
    public float roundLength = 60f;
    public int holesAddedPerRound = 1;

    [SerializeField]
    private int roundsSurvived = 0;

    private bool betweenRounds = false;
    private bool dead = false;

    private WaterFillController fillCon;
    private timerScript timer;
    private WallController wallCon;
    // private Raycaster caster;

    private void Start()
    {
        fillCon = gameObject.GetComponent<WaterFillController>();
        timer = gameObject.GetComponent<timerScript>();
        wallCon = gameObject.GetComponent<WallController>();
        // caster = gameObject.GetComponent<Raycaster>();

        timer.StartTimer();
    }

    private void Update()
    {
        // Check if the timer has reached time
        if(Input.GetKeyDown(KeyCode.M) && debug && !betweenRounds)
        {
            betweenRounds = true;

            fillCon.BeginFilling(debugFillPoints);
            roundsSurvived++;
            betweenRounds = false;
        }

        if(timer.GetTime() >= roundLength && !betweenRounds)
        {
            betweenRounds = true;
            timer.PauseTimer();
            fillCon.BeginFilling(debugFillPoints);

            if (fillCon.currentFill >= 1)
            {
                Debug.Log("ded :(");
                dead = true;
            }
        }

        if (betweenRounds && !fillCon.currentlyFilling && !dead)
        {
            StartNewRound();
        }
    }

    private void StartNewRound()
    {
        wallCon.AddHoles(holesAddedPerRound);
        roundsSurvived++;
        timer.ResetTime();
        timer.StartTimer();
        betweenRounds = false;
    }
}
