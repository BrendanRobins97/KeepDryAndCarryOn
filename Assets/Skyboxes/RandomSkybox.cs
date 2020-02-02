using UnityEngine;
using static WeightedProbability;

public class RandomSkybox : MonoBehaviour
{
    [SerializeField]
    private WeightedMaterial[] skyboxes;

    private int total;

    void Awake()
    {
        total = TotalWeights<WeightedMaterial, Material>(skyboxes);
    }

    void Start()
    {
        RenderSettings.skybox = skyboxes[RandomIndex<WeightedMaterial, Material>(skyboxes, total)].Item;
    }
}
