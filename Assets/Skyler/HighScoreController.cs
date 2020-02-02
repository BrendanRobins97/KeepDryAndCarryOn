using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    private Text highScore;

    private void Start()
    {
        highScore = gameObject.GetComponent<Text>();
        if(PlayerPrefs.HasKey("HighScore"))
        {
            highScore.text = "High Score: " + PlayerPrefs.GetInt("HighScore") + " Rounds";
        }
        else
        {
            highScore.text = "High Score: 0 Rounds";
        }
    }
}
