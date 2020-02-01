using UnityEngine;
using static RelativeProbability;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FurnitureType", order = 1)]
public class FurnitureType : ScriptableObject
{
    [Tooltip("Furniture pieces that can be spawned")]
    [SerializeField]
    private SpawnableFurniture[] possibleFurniture;

    [Tooltip("Higher numbers are more likely")]
    [SerializeField]
    private int[] relativeProbabilities;

    private int total = 0;

    void Awake()
    {
        if (possibleFurniture.Length != relativeProbabilities.Length)
            throw new System.Exception("Counts must be equal");

        if (possibleFurniture.Length == 0)
            throw new System.Exception("Must have at least one piece of furniture to spawn");

        total = Total(relativeProbabilities);
    }


    public SpawnableFurniture RandomFurniturePrefab()
    {
        return possibleFurniture[RandomIndex(relativeProbabilities, total)];
    }
}
