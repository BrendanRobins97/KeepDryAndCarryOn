using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class timerScript : MonoBehaviour
{
    public float timer;
    bool count = false;

    void FixedUpdate()
    {
        // Pausing
        if (!count)
            return;

        timer += Time.deltaTime;
        //GetComponent<UnityEngine.UI.Text>().text = timer.ToString("0.00");
    }

    public void PauseTimer()
    {
        count = false;
    }

    public void StartTimer()
    {
        count = true;
    }

    public void ResetTime()
    {
        timer = 0f;
    }

    public float GetTime()
    {
        return timer;
    }
}
