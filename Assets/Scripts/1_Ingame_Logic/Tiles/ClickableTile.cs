using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int GridPositionX;
    public int GridPositionY;
    public TileMapGameController Map;


    private void OnMouseUp()
    {
        HandleOnMouseUp();
    }

    public virtual void HandleOnMouseUp()
    {
        Map.MoveUnit(GridPositionX, GridPositionY);
    }

    public virtual void HandleTileMaterial()
    {

    }
}
