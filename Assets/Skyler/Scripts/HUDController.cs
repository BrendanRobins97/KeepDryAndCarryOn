using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour
{
    public Text timer;
    public Text rounds;
    public Text waterLevel;

    private timerScript ts;
    private RoundController rc;
    private WaterFillController wfc;

    private void Start()
    {
        ts = gameObject.GetComponent<timerScript>();
        rc = gameObject.GetComponent<RoundController>();
        wfc = gameObject.GetComponent<WaterFillController>();

        timer.text = rc.roundLength.ToString();
        rounds.text = "0";
        waterLevel.text = "0%";
    }

    private void Update()
    {
        timer.text = (rc.roundLength - ts.timer).ToString("0.00");
        rounds.text = rc.roundsSurvived.ToString();
        waterLevel.text = (wfc.currentFill * 100).ToString() + "%";
    }
}
