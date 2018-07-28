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
    Water = 0,
    Island = 1,
    ShallowWater = 2,
    Goal = 3,
    Start = 4,
    Edge = 99
}
