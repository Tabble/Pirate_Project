using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTile : MonoBehaviour {

    public TileTypeCategory Category = TileTypeCategory.Water;
    public MeshRenderer Renderer;
    public Material Water;
    public Material Island;
    public Material ShallowWater;
    public Material Goal;
    public Material Start;

    public int PositionX = 0;
    public int PositionY = 0;

    private int matIndex = 0;
    private const int MAX_TILE_TYPE_CATEGORIES = 5;
    
	public void OnChangeTile()
    {
        if(matIndex < MAX_TILE_TYPE_CATEGORIES - 1)
        {
            matIndex++;
        }
        else
        {
            matIndex = 0;
        }
        ChangeCategory();
        ChangeMaterial();
    }

    private void ChangeCategory()
    {
        Category = (TileTypeCategory) matIndex;
    }

    private void ChangeMaterial()
    {
        switch (Category)
        {
            case TileTypeCategory.Goal:
                Renderer.material = Goal;
                break;
            case TileTypeCategory.Island:
                Renderer.material = Island;
                break;
            case TileTypeCategory.ShallowWater:
                Renderer.material = ShallowWater;
                break;
            case TileTypeCategory.Start:
                Renderer.material = Start;
                break;
            case TileTypeCategory.Water:
                Renderer.material = Water;
                break;
        }
    }
}
