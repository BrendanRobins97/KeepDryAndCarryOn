using UnityEngine;
using static WeightedProbability;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FurnitureType", order = 1)]
public class FurnitureType : ScriptableObject
{
    [Tooltip("Furniture pieces that can be spawned")]
    [SerializeField]
    private WeightedSpawnableFurniture[] possibleFurniture;

    private int total = 0;
    private bool initialized;

    void OnEnable()
    {
        if (possibleFurniture == null || possibleFurniture.Length == 0)
            throw new System.Exception("Must have at least one piece of furniture to spawn");

        total = TotalWeights<WeightedSpawnableFurniture, SpawnableFurniture>(possibleFurniture);
    }

    public SpawnableFurniture RandomFurniturePrefab()
    {
        return possibleFurniture[RandomIndex<WeightedSpawnableFurniture, SpawnableFurniture>(possibleFurniture, total)].Item;
    }
}
