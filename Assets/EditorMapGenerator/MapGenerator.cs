using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {


    public Transform TilePrefab;
    public Vector2 MapSize;
    private List<Transform> generatedMap = null;
    private GameObject tileParent = null;

    void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        if(tileParent == null)
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
                newTile.SetParent(tileParent.transform);
            }
        }
    }
}
