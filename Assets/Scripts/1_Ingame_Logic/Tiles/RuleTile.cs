using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuleTile : ClickableTile {

    /// <summary>
    /// Default Material of this Tile.
    /// </summary>
    public Material DefaultMaterial;

    /// <summary>
    /// Tile Type of this Rule Tyle
    /// </summary>
    public TileTypeCategory RuleTileTypeCategory = TileTypeCategory.Island;

    /// <summary>
    /// All Rules for this Tile.
    /// </summary>
    public List<RuleTileRule> Rules;

    /// <summary>
    /// All neighbors of this tile descriped in if they are the same type or not.
    /// </summary>
    public bool[] Neighbors { get; private set; }

    private MeshRenderer meshRenderer;

    public RuleTile()
    {
        Rules = new List<RuleTileRule>();
        Rules.Add(new RuleTileRule());
    }

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        //CreateAllRuleTileMaterials();
    }

    private void CreateAllRuleTileMaterials()
    {
        foreach(var tile in Rules)
        {
            tile.Material = new Material(DefaultMaterial)
            {
                mainTexture = tile.Sprite
              
            };
            tile.Material.color = Color.green;
            tile.Material.name = "Geändert";
        }
    }

    public override void HandleTileMaterial()
    {
        SetNeighbors();
        CheckNeighbors();
    }

    public void CheckNeighbors()
    {

        foreach (var tile in Rules)
        {
            bool foundTile = false;
            for (int i = 0; i < 8; i++)
            {
                if (Neighbors[i] == tile.Neighbors[i] )
                {
                    foundTile = false;
                    break;
                }
                else
                {
                    foundTile = true;
                }
            }

            if(meshRenderer.material == null)
            {
                meshRenderer.material = foundTile ? tile.Material : DefaultMaterial;
            }
           
        }

    }

    private void SetNeighbors()
    {
        Neighbors = new bool[8];

        Neighbors[0] = Map.GetTypeOfTile(GridPositionX - 1, GridPositionY + 1) == TileTypeCategory.Island;
        Neighbors[1] = Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == TileTypeCategory.Island;
        Neighbors[2] = Map.GetTypeOfTile(GridPositionX + 1, GridPositionY + 1) == TileTypeCategory.Island;

        Neighbors[3] = Map.GetTypeOfTile(GridPositionX - 1, GridPositionY ) == TileTypeCategory.Island;
        Neighbors[4] = Map.GetTypeOfTile(GridPositionX + 1, GridPositionY ) == TileTypeCategory.Island;

        Neighbors[5] = Map.GetTypeOfTile(GridPositionX - 1, GridPositionY + 1) == TileTypeCategory.Island;
        Neighbors[6] = Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == TileTypeCategory.Island;
        Neighbors[7] = Map.GetTypeOfTile(GridPositionX + 1, GridPositionY + 1) == TileTypeCategory.Island;
    }

}

[System.Serializable]
public enum NeighborStatus
{
    egal,
    no,
    same
}
