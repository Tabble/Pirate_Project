using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    [HideInInspector]
    public int GridPositionX;
    [HideInInspector]
    public int GridPositionY;
    [HideInInspector]
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
