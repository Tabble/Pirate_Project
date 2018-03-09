﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickableTile : MonoBehaviour {

    public int GridPositionX;
    public int GridPositionY;
    public TileMap Map;


    private void OnMouseUp()
    {
        Map.MoveUnit(GridPositionX, GridPositionY);
    }
}
