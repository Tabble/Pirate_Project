using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileType
{
    public TileTypeCategory Category;
    public GameObject TilePrefab;
    public float MovementCost;
}

public enum TileTypeCategory
{
    NONE,
    Water,
    Island,
    ShallowWater,
    Goal
}
