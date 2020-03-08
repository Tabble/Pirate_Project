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
        CheckForApplyingRule();
    }

    /// <summary>
    /// Checks if a Rule applies to the Tile.
    /// </summary>
    public void CheckForApplyingRule()
    {
        foreach (var tile in Rules)
        {
            bool foundTile = false;
            for (int i = 0; i < 8; i++)
            {
                if (Neighbors[i] != tile.Neighbors[i] )
                {
                    foundTile = false;
                    break;
                }
                else
                {
                    foundTile = true;
                }
            }
            
            meshRenderer.material = foundTile ? tile.Material : DefaultMaterial;
            if (foundTile)
            {
                //Debug.Log("Found Tile");
                break;
            }
        }
    }

    private void SetNeighbors()
    {
        Neighbors = new bool[8];

        Neighbors[0] = GridPositionX > 0 && GridPositionY < 8 
            ?  Map.GetTypeOfTile(GridPositionX - 1, GridPositionY + 1) == RuleTileTypeCategory 
            : false;

        Neighbors[1] = GridPositionY < 8 
            ? Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == RuleTileTypeCategory 
            : false;

        Neighbors[2] = GridPositionX < 8 && GridPositionY < 8
            ? Map.GetTypeOfTile(GridPositionX + 1, GridPositionY + 1) == RuleTileTypeCategory
            : false;

        Neighbors[3] = GridPositionY > 0 
            ? Map.GetTypeOfTile(GridPositionX - 1, GridPositionY ) == RuleTileTypeCategory
            : false;

        Neighbors[4] = GridPositionX < 8 
            ? Map.GetTypeOfTile(GridPositionX + 1, GridPositionY ) == RuleTileTypeCategory
            : false;

        Neighbors[5] = GridPositionX > 0 && GridPositionY > 0 
            ? Map.GetTypeOfTile(GridPositionX - 1, GridPositionY - 1) == RuleTileTypeCategory
            : false;

        Neighbors[6] = GridPositionY > 0 
            ? Map.GetTypeOfTile(GridPositionX, GridPositionY - 1) == RuleTileTypeCategory
            : false;

        Neighbors[7] = GridPositionX < 8 && GridPositionY > 0 
            ? Map.GetTypeOfTile(GridPositionX + 1, GridPositionY - 1) == RuleTileTypeCategory
            : false;

        //Debug.Log(string.Format("{0} - {1} - {2}", Neighbors[0], Neighbors[1], Neighbors[2]));
        //Debug.Log(string.Format("{0} -     - {1}", Neighbors[3], Neighbors[4]));
        //Debug.Log(string.Format("{0} - {1} - {2}", Neighbors[5], Neighbors[6], Neighbors[7]));
        //Debug.Log("+++++++++++++++++++++++++++++++++++++++++++++++");
    }
}
