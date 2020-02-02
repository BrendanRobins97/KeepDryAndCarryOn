using System;
using UnityEngine;

interface IWeighted<T>
{
    T Item { get; }
    int Weight { get; }
}

[Serializable]
struct WeightedFurnitureType : IWeighted<FurnitureType>
{
    [SerializeField]
    private FurnitureType item;

    [SerializeField]
    private int weight;

    public FurnitureType Item => item;
    public int Weight => weight;
}

[Serializable]
struct WeightedSpawnableFurniture : IWeighted<SpawnableFurniture>
{
    [SerializeField]
    private SpawnableFurniture item;

    [SerializeField]
    private int weight;

    public SpawnableFurniture Item => item;
    public int Weight => weight;
}

[Serializable]
struct WeightedMaterial : IWeighted<Material>
{
    [SerializeField]
    private Material item;

    [SerializeField]
    private int weight;

    public Material Item => item;
    public int Weight => weight;
}