using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterFillController : MonoBehaviour
{
    public bool debug = false;
    public int debugFillPoints = 20000;
    [Tooltip("The amount of water raising contributed by each fill point.")]
    public float fillRatio = 0.00001f;
    [Tooltip("Amount to raise the water. Determined by amount of uncovered holes.")]
    [Range(0f, 1f)]
    public float waterFillAmount = 0f;
    [Tooltip("What percent of the room is currently filled. O% is empty 100% is full and implies a game over.")]
    [Range(0f, 1f)]
    public float currentFill = 0f;
    [Tooltip("The water game object which will rise as the room is filled.")]
    public GameObject water;
    [Tooltip("The height at which the the water will be at max fill.")]
    public float maxFillHeight = 5f;
    [Tooltip("The height at which the the water will be at min fill.")]
    public float minFillHeight = 0f;
    [Tooltip("The interpolant used for moving the water.")]
    [Range(0f, 1f)]
    public float t;

    private bool fill = false;
    public bool currentlyFilling = false;
    private float amountToIncreaseHeight = 0f;
    private float currentFillHeight = 0f;

    private void Start()
    {
        currentFillHeight = minFillHeight;
        water.transform.position = new Vector3(water.transform.position.x, currentFillHeight, water.transform.position.z);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P) && debug)
        {
            BeginFilling(debugFillPoints);
        }

        if (fill && water.transform.position.y < currentFillHeight-0.01)
        {
            water.transform.position = Vector3.Lerp(water.transform.position, new Vector3(water.transform.position.x, currentFillHeight, water.transform.position.z), t);
        }
        else if(fill)
        {
            fill = false;
            waterFillAmount = 0f;
            amountToIncreaseHeight = 0f;
            currentlyFilling = false;
        }
    }

    public void BeginFilling(int numFillPoints)
    {
        if (currentlyFilling)
            return;

        waterFillAmount = numFillPoints * fillRatio;

        currentFill += waterFillAmount;
        if (currentFill > 1f)
            currentFill = 1f;

        amountToIncreaseHeight = waterFillAmount * maxFillHeight;

        currentFillHeight += amountToIncreaseHeight;
        if (currentFillHeight > maxFillHeight)
            currentFillHeight = maxFillHeight;
        currentlyFilling = true;
        fill = true;
    }
}
