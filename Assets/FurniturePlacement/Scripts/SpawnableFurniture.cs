using UnityEngine;

public class SpawnableFurniture : MonoBehaviour
{
    [SerializeField]
    private bool randomizeRotation = false;

    [SerializeField]
    private Transform spawnOrigin;

    void Awake()
    {
        if (!spawnOrigin)
            throw new System.Exception("Must set a spawn origin");
    }

    public void Instantiate(Vector3 position, Quaternion rotation)
    {
        Instantiate(gameObject, position - spawnOrigin.position, rotation * GetSpawnRotation());
    }

    private Quaternion GetSpawnRotation()
    {
        if (randomizeRotation)
        {
            return transform.rotation * Quaternion.Euler(0, Random.Range(0f, 360f), 0);
        }
        else
        {
            return transform.rotation;
        }
    }
}
