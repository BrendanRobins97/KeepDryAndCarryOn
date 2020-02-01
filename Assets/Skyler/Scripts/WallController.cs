using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    public bool debug = false;
    [Tooltip("Possible hole variants of walls.")]
    public GameObject[] holes;
    [Tooltip("The number of hole walls when the game starts.")]
    public int startingHoles = 1;

    [SerializeField]
    [Tooltip("The currently placed walls.")]
    private List<GameObject> currentWalls;
    [SerializeField]
    [Tooltip("The currently placed holes.")]
    private List<GameObject> currentHoles;

    int selectedWall = 0;
    int selectedHole = 0;

    private void Start()
    {
        AddHoles(startingHoles);
    }

    private bool PlaceHole()
    {
        // Can't place a hole if there aren't anymore walls
        if (currentWalls.Count == 0)
            return false;

        // Choose the wall to replace and the hole which will replace it
        selectedWall = Random.Range(0, currentWalls.Count);
        selectedHole = Random.Range(0, holes.Length);

        // Place the selected hole at the selected wall's location
        currentHoles.Add(Instantiate(holes[selectedHole]));
        currentHoles[currentHoles.Count - 1].transform.position = currentWalls[selectedWall].transform.position;
        currentHoles[currentHoles.Count - 1].transform.rotation = currentWalls[selectedWall].transform.rotation;

        // Remove and destroy the selected wall
        GameObject wall = currentWalls[selectedWall];
        currentWalls.RemoveAt(selectedWall);
        Destroy(wall);

        // We successfully placed a hole
        return true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L) && debug)
        {
            PlaceHole();
        }
    }

    public void AddHoles(int numHoles)
    {
        // Add holes until there aren't anymore walls to be replaced
        for (int i = numHoles; i > 0; i--)
        {
            if (!PlaceHole())
                break;
        }
    }
}
