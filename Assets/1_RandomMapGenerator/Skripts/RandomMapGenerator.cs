using MapCreator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomMapGenerator : MonoBehaviour {

    public Transform TilePrefab;
    public Vector2 MapSize;
    private GameObject tileParent = null;

    private List<MapCreatorTile> TilesToUse = new List<MapCreatorTile>();
    public List<GameObject> AllTiles = new List<GameObject>();

    private List<TileTypeCategory> categoriesToUse 
            = new List<TileTypeCategory> { TileTypeCategory.Water, TileTypeCategory.ShallowWater, TileTypeCategory.Island };

	// Use this for initialization
	void Start () {
        GenerateMapToDrawOn();
        MapCreatorTile startTile = TilesToUse[Random.Range(0, TilesToUse.Count)];
        startTile.ChangeMaterial(TileTypeCategory.Start, true);
        TilesToUse.Remove(startTile);

        MapCreatorTile goalTile = TilesToUse[Random.Range(0, TilesToUse.Count)];
        goalTile.ChangeMaterial(TileTypeCategory.Goal, true);
        TilesToUse.Remove(goalTile);

        foreach(var tile in TilesToUse)
        {
            tile.ChangeMaterial(categoriesToUse[Random.Range(0, categoriesToUse.Count)], true);
        }
    }

    public void GenerateMapToDrawOn()
    {
        if (tileParent == null)
        {
            tileParent = new GameObject("Tile Parent");
        }
        else
        {
            DestroyImmediate(tileParent);
            tileParent = new GameObject("Tile Parent");
        }

        for (int x = 0; x < MapSize.x; x++)
        {
            for (int y = 0; y < MapSize.y; y++)
            {
                Vector3 tilePosition = new Vector3(-MapSize.x / 2 + 0.5f + x, 0, -MapSize.y / 2 + 0.5f + y);
                Transform newTile = Instantiate(TilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                newTile.GetComponent<MapCreatorTile>().PositionX = x;
                newTile.GetComponent<MapCreatorTile>().PositionY = y;
                newTile.SetParent(tileParent.transform);
                TilesToUse.Add(newTile.GetComponent<MapCreatorTile>());
                AllTiles.Add(newTile.gameObject);
            }
        }
    }
}
