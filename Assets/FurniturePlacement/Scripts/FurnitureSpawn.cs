using UnityEngine;
using static WeightedProbability;

public class FurnitureSpawn : MonoBehaviour
{
    [Tooltip("Furniture types that can be spawned")]
    [SerializeField]
    private WeightedFurnitureType[] allowedTypes;

    private int total = 0;

    void Awake()
    {
        if (allowedTypes == null || allowedTypes.Length == 0)
            throw new System.Exception("Must have at least one furniture type");

        total = TotalWeights<WeightedFurnitureType, FurnitureType>(allowedTypes);
    }

    void Start()
    {
        Spawn();
    }

    /// <summary>
    /// Spawns a random piece of furniture from the allowed types.
    /// Respawns the current piece if one already exists
    /// </summary>
    public void Spawn()
    {
        SpawnableFurniture prefab = RandomFurnitureType().RandomFurniturePrefab();
        prefab.Instantiate(transform.position, transform.rotation);
    }

    private FurnitureType RandomFurnitureType()
    {
        return allowedTypes[RandomIndex<WeightedFurnitureType, FurnitureType>(allowedTypes, total)].Item;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "FurnitureSpawn.png");
    }
}
