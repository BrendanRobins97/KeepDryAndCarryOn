using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    public bool debug = false;
    public int debugFillPoints = 20000;
    [Tooltip("The amount of time in seconds before a round ends.")]
    public float roundLength = 60f;
    public int holesAddedPerRound = 1;
    public int roundsSurvived = 0;

    private bool betweenRounds = false;
    private bool dead = false;
    private bool filling = false;

    private WaterFillController fillCon;
    private timerScript timer;
    private WallController wallCon;
    private globals raycaster;

    private void Start()
    {
        fillCon = gameObject.GetComponent<WaterFillController>();
        timer = gameObject.GetComponent<timerScript>();
        wallCon = gameObject.GetComponent<WallController>();
        raycaster = gameObject.GetComponent<globals>();

        timer.StartTimer();
    }

    private void Update()
    {
        // Debugging
        if(Input.GetKeyDown(KeyCode.M) && debug && !betweenRounds)
        {
            betweenRounds = true;

            fillCon.BeginFilling(debugFillPoints);
            roundsSurvived++;
            betweenRounds = false;
        }

        // Check if done raycasting
        // Begin filling
        if (betweenRounds && globals.doneChecker == 0 && filling)
        {
            fillCon.BeginFilling(globals.globalHoles);
            filling = false;

            // Game Over
            if (fillCon.currentFill >= 1)
            {
                // Set a new high score if we did better than the curent high score
                if (PlayerPrefs.HasKey("HighScore"))
                {
                    if (PlayerPrefs.GetInt("HighScore") < roundsSurvived)
                    {
                        PlayerPrefs.SetInt("HighScore", roundsSurvived);
                        PlayerPrefs.Save();
                    }
                }
                else
                {
                    PlayerPrefs.SetInt("HighScore", roundsSurvived);
                    PlayerPrefs.Save();
                }

                Cursor.lockState = CursorLockMode.None;
                Debug.Log("ded :(");
                dead = true;
                SceneManager.LoadScene("MainMenu");
            }
        }

        // Fire the raycast
        if (timer.GetTime() >= roundLength && !betweenRounds)
        {
            betweenRounds = true;
            filling = true;
            timer.PauseTimer();
            raycaster.FireFunction(); // Startfiring raycast
        }

        // Start a new round
        if (betweenRounds && !fillCon.currentlyFilling && !dead && globals.doneChecker == 0 && !filling)
        {
            StartNewRound();
        }
    }

    private void StartNewRound()
    {
        globals.globalHoles = 0;
        wallCon.AddHoles(holesAddedPerRound);
        roundsSurvived++;
        timer.ResetTime();
        timer.StartTimer();
        betweenRounds = false;
    }
}
