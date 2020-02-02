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
        Instantiate(gameObject,
            position - Vector3.Scale(spawnOrigin.localPosition, transform.localScale),
            randomizeRotation ? rotation * GetRandomRotation() : rotation);
    }

    private Quaternion GetRandomRotation()
    {
        return Quaternion.Euler(0, Random.Range(0f, 360f), 0);
    }
}
