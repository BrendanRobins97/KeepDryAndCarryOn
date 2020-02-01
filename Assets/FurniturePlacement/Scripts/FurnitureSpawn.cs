using UnityEngine;

public class FurnitureSpawn : MonoBehaviour
{
    [Tooltip("Furniture types that can be spawned")]
    [SerializeField]
    private FurnitureType[] allowedTypes;

    void OnDrawGizmos()
    {
        Gizmos.DrawIcon(transform.position, "FurnitureSpawn.png");
    }

    void Awake()
    {
        if (allowedTypes == null || allowedTypes.Length == 0)
            throw new System.Exception("Must have at least one furniture type");
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
        GameObject spawned = Instantiate(GetRandomFurnitureType().GetRandomFurniturePrefab(), transform.position, transform.rotation);
    }

    private FurnitureType GetRandomFurnitureType()
    {
        return allowedTypes[Random.Range(0, allowedTypes.Length)];
    }
}
