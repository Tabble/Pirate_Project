using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuleTileTest : ClickableTile {

    public MeshRenderer MeshRenderer;
    public Material DefaultMaterial;
    public List<RuleTile> RuleTiles;
    public NeighborStatus[] Neighbors;



    public RuleTileTest()
    {
        RuleTiles = new List<RuleTile>();
        RuleTiles.Add(new RuleTile());
    }

    void Awake()
    {
        //CreateAllRuleTileMaterials();
    }

    private void CreateAllRuleTileMaterials()
    {
        foreach(var tile in RuleTiles)
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

        foreach (var tile in RuleTiles)
        {
            bool foundTile = false;
            for (int i = 0; i < 8; i++)
            {
                if (Neighbors[i] != tile.Neighbors[i])
                {
                    foundTile = false;
                    break;
                }
                else
                {
                    foundTile = true;
                }
            }

            if(MeshRenderer.material == null)
            {
                MeshRenderer.material = foundTile ? tile.Material : DefaultMaterial;
            }
           
        }

    }

    private void SetNeighbors()
    {
        Neighbors = new NeighborStatus[8];

        Neighbors[0] = Map.GetTypeOfTile(GridPositionX - 1, GridPositionY + 1) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;
        Neighbors[1] = Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;
        Neighbors[2] = Map.GetTypeOfTile(GridPositionX + 1, GridPositionY + 1) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;

        Neighbors[3] = Map.GetTypeOfTile(GridPositionX - 1, GridPositionY ) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;
        Neighbors[4] = Map.GetTypeOfTile(GridPositionX + 1, GridPositionY ) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;

        Neighbors[5] = Map.GetTypeOfTile(GridPositionX - 1, GridPositionY + 1) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;
        Neighbors[6] = Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;
        Neighbors[7] = Map.GetTypeOfTile(GridPositionX + 1, GridPositionY + 1) == TileTypeCategory.Island ? NeighborStatus.same : NeighborStatus.no;
    }

}

[System.Serializable]
public enum NeighborStatus
{
    egal,
    no,
    same
}
