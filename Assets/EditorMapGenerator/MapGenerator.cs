using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    public ConsistentMapID ConsistentMapID;
    public MapsSaver MapsSaver;
    public Transform TilePrefab;
    public Vector2 MapSize;
    private GameObject tileParent = null;
    private int mapID = 0;
    private List<MapVO> Maps = new List<MapVO>();
    public int[] MapIds;
    public int mapIndex = 0;


    void Start()
    {
        GenerateMapToDrawOn();
    }

    public void GenerateMapToDrawOn()
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
                newTile.GetComponent<ChangeTile>().PositionX = x;
                newTile.GetComponent<ChangeTile>().PositionY= y;
                newTile.SetParent(tileParent.transform);
            }
        }
    }

    public void ShowMapWithID(int id)
    {
        if (tileParent == null)
        {
            tileParent = new GameObject("Tile Parent");
        }
        else
        {
            DestroyImmediate(tileParent);
            tileParent = new GameObject("Tile Parent");
        }
        MapVO mapWithID = MapsSaver.GetMapWithID(id);
        //MapVO mapWithID = Maps.Find(x => x.MapID == id);
        List<ChangeTile> tiles = new List<ChangeTile>();
        if(mapWithID != null)
        {
            for (int x = 0; x < MapSize.x; x++)
            {
                for (int y = 0; y < MapSize.y; y++)
                {
                    Vector3 tilePosition = new Vector3(-MapSize.x / 2 + 0.5f + x, 0, -MapSize.y / 2 + 0.5f + y);
                    Transform newTile = Instantiate(TilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                    newTile.SetParent(tileParent.transform);
                    tiles.Add(newTile.GetComponent<ChangeTile>());
                }
            }
            for(int i = 0; i < tiles.Count; i++)
            {
                tiles[i].Category = mapWithID.Tiles[i].Category;
                tiles[i].PositionX = mapWithID.Tiles[i].PositionX;
                tiles[i].PositionY = mapWithID.Tiles[i].PositionY;
                tiles[i].ChangeMaterial();
            }
        }
        
    }

    public void ShowMap(int ID)
    {
        Debug.Log("Show map with id" + ID);
        ShowMapWithID(ID);
    }

    public void OverwrideMapWithID(int ID)
    {
        Debug.Log("Override Map with ID: " + ID);
        OverrideMap(ID);
    }

    public void DeleteMapWithID(int ID)
    {
        //TODO write function
        MapsSaver.DeleteMapWithID(ID);
        LoadMaps();
    }

    public void LoadMaps()
    {
        if(MapsSaver.LoadAllMaps() != null)
        {
            Maps = MapsSaver.LoadAllMaps();
            List<int> ids = new List<int>();
            foreach(var map in Maps)
            {
                ids.Add(map.MapID);
            }
            MapIds = ids.ToArray();
        }
        else
        {
            Debug.Log("[MapGenerator] no maps saved");
            //JSONObject mapJson = new JSONObject(LoadString(GameConstants.MAP_FILE_LOCATION));
            //Maps = new List<MapVO>();
            //List<int> MapIDs = new List<int>();
            ////Debug.Log("[Json] show json: " + mapJson.ToString());
            //for (int i = 0; i < mapJson.list.Count; i++)
            //{
            //    //Debug.Log("[Json] maps " + mapJson[i].ToString());
            //    MapVO newMap = new MapVO();
            //    newMap.SetID((int)mapJson[i][0].i);
            //    MapIDs.Add(newMap.MapID);
            //    Debug.Log("Map ID: " + newMap.MapID);
            //    List<TileVO> changeTiles = new List<TileVO>();
            //    for (int t = 1; t < mapJson[i].Count; t++)
            //    {
            //        //Debug.Log("tiles:" + mapJson[i][t][0].ToString());
            //        TileVO newTile = new TileVO();
            //        if (Enum.IsDefined(typeof(TileTypeCategory), mapJson[i][t][GameConstants.CATEGORY_KEY].str))
            //        {
            //            newTile.Category = (TileTypeCategory)Enum.Parse(typeof(TileTypeCategory), mapJson[i][t][GameConstants.CATEGORY_KEY].str);
            //        }
            //        else
            //        {
            //            Debug.Log("Enum not defined: " + mapJson[i][t][GameConstants.CATEGORY_KEY].str);
            //        }

            //        newTile.PositionX = int.Parse(mapJson[i][t][GameConstants.POSITION_X_KEY].str);
            //        newTile.PositionY = int.Parse(mapJson[i][t][GameConstants.POSITION_Y_KEY].str);
            //        changeTiles.Add(newTile);
            //    }
            //    newMap.SetTiles(changeTiles.ToArray());
            //    Maps.Add(newMap);
            //}
            //MapIds = MapIDs.ToArray();
            //MapsSaver.SetAllMaps(Maps);
        }
        

    }

    public void SaveNewMap()
    {
        //JSONObject mapJson = new JSONObject(LoadString(GameConstants.MAP_FILE_LOCATION));
        
        ChangeTile[] tiles = tileParent.GetComponentsInChildren<ChangeTile>();
        mapID = ConsistentMapID.GetConsistantID();
        //if (PlayerPrefs.HasKey(GameConstants.MAP_ID_KEY))
        //{
        //    mapID = PlayerPrefs.GetInt(GameConstants.MAP_ID_KEY, 0);
        //    mapID++;
        //}
        //else
        //{
        //    mapID = 0;
        //}
        //PlayerPrefs.SetInt(GameConstants.MAP_ID_KEY, mapID);
        //PlayerPrefs.Save();
        //JSONObject map = new JSONObject();
        //map.AddField(GameConstants.MAP_ID_KEY, mapID);
        List<TileVO> tileVOs = new List<TileVO>();
        foreach(var tile in tiles)
        {
            //map.Add(tile.GetTileJson());
            tileVOs.Add(new TileVO(tile.Category, tile.PositionX, tile.PositionY));

        }
        MapVO newMap = new MapVO();
        newMap.SetID(mapID);
        newMap.SetTiles(tileVOs.ToArray());
        MapsSaver.AddNewMap(newMap);
        //mapJson.Add(map);
        //SaveString(GameConstants.MAP_FILE_LOCATION, mapJson.ToString());
        Debug.Log(string.Format("[MapGenerator] save {0} tiles to a map.", tiles.Length));
        LoadMaps();
    }

    public void OverrideMap(int id)
    {
        //JSONObject newMaps = new JSONObject();
        ChangeTile[] tiles = tileParent.GetComponentsInChildren<ChangeTile>();
        MapVO mapWithID = MapsSaver.GetMapWithID(id);
        //MapVO mapWithID = Maps.Find(x => x.MapID == id);
        
        if (mapWithID != null)
        {
            for (int i = 0; i < tiles.Length; i++)
            {
                mapWithID.Tiles[i].Category = tiles[i].Category;
                mapWithID.Tiles[i].PositionX = tiles[i].PositionX;
                mapWithID.Tiles[i].PositionY = tiles[i].PositionY;
            }
            MapsSaver.OverrideMapWithID(id, mapWithID);
        }
        else
        {
            Debug.Log("[Overwrite Tile] no map with id found");
        }
        //foreach(var m in Maps)
        //{
        //    JSONObject map = new JSONObject();
        //    map.AddField(GameConstants.MAP_ID_KEY, m.MapID);
        //    foreach(var tile in m.Tiles)
        //    {
        //        map.Add(tile.GetTileJson());
        //    }
        //    newMaps.Add(map);
        //}
        //SaveString(GameConstants.MAP_FILE_LOCATION, newMaps.ToString());
        LoadMaps();
    }

    //public void SaveString(string fileName, string data)
    //{
    //    using (FileStream fileStream = new FileStream(Application.persistentDataPath + "/" + fileName, FileMode.Create, FileAccess.ReadWrite))
    //    {
    //        BinaryFormatter bf = new BinaryFormatter();
    //        bf.Serialize(fileStream, data);
    //    }
    //}

    //public static string LoadString(string fileName)
    //{
    //    string loadedData = default(string);
    //    string filePath = Application.persistentDataPath + "/" + fileName;
    //    if (File.Exists(filePath))
    //    {
    //        using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
    //        {
    //            try
    //            {
    //                BinaryFormatter bf = new BinaryFormatter();
    //                loadedData = (string)bf.Deserialize(fileStream);
    //            }
    //            catch (Exception)
    //            {

    //            }
    //        }
    //    }
    //    return loadedData == default(string) ? "" : loadedData;
    //}

}

[Serializable]
public class MapVO
{
    public TileVO[] Tiles { get; private set; }
    public int MapID { get; private set; }
    
    public void SetID(int ID)
    {
        MapID = ID;
    }
    public void SetTiles(TileVO[] tiles)
    {
        Tiles = tiles;
    }
}
