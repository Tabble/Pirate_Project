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
    Undefined = 0,
    Water = 1,
    Island = 2,
    ShallowWater = 3,
    Goal = 4,
    Start = 5,
    Edge = 99
}
