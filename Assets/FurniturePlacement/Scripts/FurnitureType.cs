using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/FurnitureType", order = 1)]
public class FurnitureType : ScriptableObject
{
    [Tooltip("Furniture pieces that can be spawned")]
    [SerializeField]
    private GameObject[] possibleFurniturePieces;

    [Tooltip("Higher numbers are more likely")]
    [SerializeField]
    private int[] probability;

    private int total = 0;

    void Awake()
    {
        if (possibleFurniturePieces.Length != probability.Length)
            throw new System.Exception("Counts must be equal");

        CalculateProbabilities();
    }

    private void CalculateProbabilities()
    {
        for (int i = 0; i < probability.Length; ++i)
        {
            int current = probability[i];
            probability[i] += total;
            total += current;
        }
    }

    public GameObject GetRandomFurniturePrefab()
    {
        return possibleFurniturePieces[GetRandomIndex()];
    }

    private int GetRandomIndex()
    {
        int num = Random.Range(0, total + 1);

        for (int i = 0; i < probability.Length; ++i)
        {
            if (probability[i] >= num)
                return i;
        }

        return probability.Length - 1;
    }
}
