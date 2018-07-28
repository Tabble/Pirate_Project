using MapCreator;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class MapVO
{
    public TileVO[] Tiles;
    public int MapID;

    public void SetID(int ID)
    {
        MapID = ID;
    }
    public void SetTiles(TileVO[] tiles)
    {
        Tiles = tiles;
    }
}
