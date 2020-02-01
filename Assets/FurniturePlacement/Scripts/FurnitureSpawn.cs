using UnityEngine;
using static RelativeProbability;

public class FurnitureSpawn : MonoBehaviour
{
    [Tooltip("Furniture types that can be spawned")]
    [SerializeField]
    private FurnitureType[] allowedTypes;

    [Tooltip("Higher numbers are more likely")]
    [SerializeField]
    private int[] relativeProbabilities;

    private int total = 0;

    void Awake()
    {
        if (allowedTypes.Length != relativeProbabilities.Length)
            throw new System.Exception("Counts must be equal");

        if (allowedTypes.Length == 0)
            throw new System.Exception("Must have at least one furniture type");

        total = Total(relativeProbabilities);
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
        return allowedTypes[RandomIndex(relativeProbabilities, total)];
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "FurnitureSpawn.png");
    }
}
