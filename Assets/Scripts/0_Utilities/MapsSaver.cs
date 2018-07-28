using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[CreateAssetMenu(fileName = "MapsSaver", menuName = "MapSaver", order = 1)]
public class MapsSaver : ScriptableObject {

    public List<MapVO> AllMaps = new List<MapVO>();

    public void AddNewMap(MapVO newMap)
    {
        AllMaps.Add(newMap);
        SaveMapData();
    }

    public MapVO GetMapWithID(int id)
    {
        return AllMaps.Find(x => x.MapID == id);
    }

    public void DeleteMapWithID(int id)
    {
        MapVO map = AllMaps.Find(x => x.MapID == id);
        if(map != null)
        {
            AllMaps.Remove(map);
        }
        SaveMapData();
        Debug.Log("Delete Map with ID: " + id);
    }

    public void OverrideMapWithID(int id, MapVO newMapVO)
    {
        for(int i = 0; i < AllMaps.Count; i++)
        {
            if(AllMaps[i].MapID == id)
            {
                AllMaps[i] = newMapVO;
            }
        }
        SaveMapData();
    }

    public List<MapVO> LoadAllMaps()
    {
        if(AllMaps.Count < 1)
        {
            LoadMapData();
        }
        return AllMaps;
    }

    private string gameDataProjectFilePath = "/Resources/maps.json";

    private void LoadMapData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;

        if (File.Exists(filePath))
        {
            JSONObject mapJson = new JSONObject(File.ReadAllText(filePath));
            AllMaps = new List<MapVO>();
            List<int> MapIDs = new List<int>();

            for (int i = 0; i < mapJson.list.Count; i++)
            {
                MapVO newMap = new MapVO();
                newMap.SetID((int)mapJson[i][0].i);
                MapIDs.Add(newMap.MapID);
                Debug.Log("Map ID: " + newMap.MapID);
                List<TileVO> changeTiles = new List<TileVO>();
                for (int t = 1; t < mapJson[i].Count; t++)
                {
                    TileVO newTile = new TileVO();
                    if (System.Enum.IsDefined(typeof(TileTypeCategory), mapJson[i][t][GameConstants.CATEGORY_KEY].str))
                    {
                        newTile.Category = (TileTypeCategory)Enum.Parse(typeof(TileTypeCategory), mapJson[i][t][GameConstants.CATEGORY_KEY].str);
                    }
                    else
                    {
                        Debug.Log("Enum not defined: " + mapJson[i][t][GameConstants.CATEGORY_KEY].str);
                    }

                    newTile.PositionX = int.Parse(mapJson[i][t][GameConstants.POSITION_X_KEY].str);
                    newTile.PositionY = int.Parse(mapJson[i][t][GameConstants.POSITION_Y_KEY].str);
                    changeTiles.Add(newTile);
                }
                newMap.SetTiles(changeTiles.ToArray());
                AllMaps.Add(newMap);
            }                     
        }
        else
        {
            // TODO 20180728 create file
        }
    }

    private void SaveMapData()
    {
        JSONObject mapsJson = new JSONObject();
        foreach (var m in AllMaps)
        {
            JSONObject map = new JSONObject();
            map.AddField(GameConstants.MAP_ID_KEY, m.MapID);
            foreach (var tile in m.Tiles)
            {
                map.Add(tile.GetTileJson());
            }
            mapsJson.Add(map);
        }
        string dataAsJson = mapsJson.ToString();
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}
