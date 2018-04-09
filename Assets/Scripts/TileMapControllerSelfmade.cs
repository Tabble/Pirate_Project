using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public class TileMapControllerSelfmade : MonoBehaviour {

    public int MapID = 12;
    public GameObject PlayerUnit;
    public TileType[] tileTypes;
    Node[,] graph;
    TileTypeCategory[,] tiles;
    int mapSizeX = 10;
    int mapSizeY = 10;
    float eneregyToGoal = 0;
    Node targetNode;
    TileType currentTileUnitIsOn;
    TileType nextTileUnitMovesTo;
    private List<MapVO> Maps = new List<MapVO>();
    private Vector2 playerStart = new Vector2();


    private void Start()
    {
        // create default map tiles
        // Setup selected 
        PlayerUnit.GetComponent<Unit>().Map = this;
       
        tiles = new TileTypeCategory[mapSizeX, mapSizeY];

        GenerateMapData();
        PlayerUnit.GetComponent<Unit>().SetCurrentPositionInGrid((int)playerStart.x, (int)playerStart.y);
        // instantiate visual prefabs
        GeneratePathfindingPath();
        GenerateMapVisuals();
        GeneratePathToGoal();
    }

    private void GenerateMapData()
    {
        //for (int x = 0; x < mapSizeX; x++)
        //{
        //    for (int y = 0; y < mapSizeY; y++)
        //    {
        //        tiles[x, y] = TileTypeCategory.Water;
        //    }
        //}

        //for (int x = 3; x <= 5; x++)
        //{
        //    for (int y = 0; y < 4; y++)
        //    {
        //        tiles[x, y] = TileTypeCategory.ShallowWater;
        //    }
        //}

        ////make an Island
        //tiles[4, 4] = TileTypeCategory.Island;
        //tiles[5, 4] = TileTypeCategory.Island;
        //tiles[6, 4] = TileTypeCategory.Island;
        //tiles[7, 4] = TileTypeCategory.Island;
        //tiles[8, 4] = TileTypeCategory.Island;
        //tiles[4, 5] = TileTypeCategory.Island;
        //tiles[8, 6] = TileTypeCategory.Island;
        //tiles[0, 5] = TileTypeCategory.Island;
        //tiles[1, 5] = TileTypeCategory.Island;
        //tiles[2, 6] = TileTypeCategory.ShallowWater;
        //tiles[3, 6] = TileTypeCategory.ShallowWater;
        //tiles[4, 6] = TileTypeCategory.ShallowWater;
        //tiles[2, 5] = TileTypeCategory.ShallowWater;
        //tiles[3, 5] = TileTypeCategory.ShallowWater;

        //// make a goal
        //tiles[9, 8] = TileTypeCategory.Goal;

        LoadMaps();
        MapVO mapWithID = Maps.Find(x => x.MapID == MapID);
        foreach (var tile in mapWithID.Tiles)
        {
            if (tile.Category == TileTypeCategory.Start)
            {
                playerStart = new Vector2(tile.PositionX, tile.PositionY);
            }
            tiles[tile.PositionX, tile.PositionY] = tile.Category;
        }
    }

    private void GeneratePathfindingPath()
    {
        graph = new Node[mapSizeX, mapSizeY];
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                graph[x, y] = new Node
                {
                    X = x,
                    Y = y
                };
            }
        }
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
               
                
                // 4 way- connection
                if(x > 0)
                {
                    graph[x, y].Neighbors.Add(graph[x - 1, y]);
                }
                if (x < mapSizeX - 1)
                {
                    graph[x, y].Neighbors.Add(graph[x + 1, y]);
                }
                if (y > 0)
                {
                    graph[x, y].Neighbors.Add(graph[x, y - 1]);
                }
                if (y < mapSizeY - 1)
                {
                    graph[x, y].Neighbors.Add(graph[x, y + 1]);
                }

            }
        }
    }

    private void GenerateMapVisuals()
    {
        for (int x = 0; x < mapSizeX; x++)
        {
            for (int y = 0; y < mapSizeY; y++)
            {
                TileType tt = GetTileOfCategory(tiles[x, y]);
                if(tt != null)
                {
                    GameObject go = Instantiate(tt.TilePrefab, new Vector3(x, y, 0), Quaternion.identity);
                    go.transform.SetParent(gameObject.transform);
                    ClickableTile ct = go.GetComponent<ClickableTile>();
                    ct.GridPositionX = x;
                    ct.GridPositionY = y;
                    ct.Map = this;
                    if(tt.Category == TileTypeCategory.Goal)
                    {
                        targetNode = new Node();
                        targetNode.X = x;
                        targetNode.Y = y;
                    }
                }
            }
        }
    }

    private TileType GetTileOfCategory(TileTypeCategory category)
    {
        foreach(var tile in tileTypes)
        {
            if(tile.Category == category)
            {
                return tile;
            }
        }
        return null;
    }

    public Vector3 TileCoordinatsToWoldCoordinates(int x, int y)
    {
        return new Vector3(x, y, -1);
    }

    public void GeneratePathToGoal()
    {
        
        // clearout old path
        PlayerUnit.GetComponent<Unit>().CurrentPath = null;

        Dictionary<Node, float> distance = new Dictionary<Node, float>();
        Dictionary<Node, Node> previous = new Dictionary<Node, Node>();
        // list of nodes we haven't checked yet;
        List<Node> unvisited = new List<Node>();
        Node source = graph[PlayerUnit.GetComponent<Unit>().GridPositionX,
                            PlayerUnit.GetComponent<Unit>().GridPositionY];
        Node target = graph[targetNode.X, targetNode.Y];

        distance[source] = 0;
        previous[source] = null;

        // initialize everyting to have INFINITY distance
        foreach( Node node in graph)
        {
            if(node != source)
            {
                distance[node] = Mathf.Infinity;
                previous[node] = null;
            }

            unvisited.Add(node);
        }

        while(unvisited.Count > 0)
        {
            // u is going to be te unvisited node with the smalles distance.
            Node u = null;
            foreach(Node possibleU in unvisited)
            {
                if(u == null || distance[possibleU] < distance[u])
                {
                    u = possibleU;
                }
            }
           
            if(u == target)
            {
                // exit while loop
                break;
            }
            unvisited.Remove(u);

            float alt = 0;
            foreach (Node node in u.Neighbors)
            {
                //float alt = distance[u] + u.DistanceTo(node);
                alt = distance[u] + CostToEnterTile(node.X, node.Y);
                
                if (alt < distance[node])
                {
                    distance[node] = alt;
                    previous[node] = u;
                    node.EnergyToThisTile = alt;
                }
            }
            
        }

        // if we get here, either we found the shortest route or there is no route at ALL
        if(previous[target] == null)
        {
            // there is route between target and source
            return;
        }

        List<Node> currentPath = new List<Node>();
        Node tempNode = target;
        while(tempNode != null)
        {
            currentPath.Add(tempNode);
            tempNode = previous[tempNode];
        }

        currentPath.Reverse();
        eneregyToGoal = currentPath[currentPath.Count - 1].EnergyToThisTile;
        Debug.Log("EnergyToThisTile : " + eneregyToGoal);
        MainGUIController.Instance.SetEnergy(eneregyToGoal);
        PlayerUnit.GetComponent<Unit>().CurrentPath = currentPath;
    }

    float CostToEnterTile(int x , int y)
    {
        TileType tt = new TileType();
        tt.Category = tiles[x, y];
        switch (tt.Category)
        {
            case TileTypeCategory.Water:
            case TileTypeCategory.Goal:
                return 1;
            case TileTypeCategory.Island:
                return 99999;
            case TileTypeCategory.ShallowWater:
                return 2;
            default:
                return 0;
        }
    }

    public void MoveUnit(int gridX, int gridY)
    {
       
        if(IsUnitNeighbour(gridX, gridY) && eneregyToGoal >= CostToEnterTile(gridX, gridY))
        {
            eneregyToGoal -= CostToEnterTile(gridX, gridY);
            Debug.Log("energy left: " + eneregyToGoal);
            MainGUIController.Instance.SetEnergy(eneregyToGoal);
            PlayerUnit.GetComponent<Unit>().SetCurrentPositionInGrid(gridX, gridY);
            if(eneregyToGoal == 0 && tiles[gridX, gridY] == TileTypeCategory.Goal)
            {
                Debug.Log("GEWONNEN");
                MainGUIController.Instance.ShowGameoverText(true);
            }
            else if(eneregyToGoal == 0)
            {
                Debug.Log("VERLOREN");
                MainGUIController.Instance.ShowGameoverText(false);
            }
            
        }
        
       // PlayerUnit.transform.position = tile.position;
    }

    private bool IsUnitNeighbour(int newX, int newY)
    {
        float currentX = PlayerUnit.GetComponent<Unit>().GridPositionX;
        float currentY = PlayerUnit.GetComponent<Unit>().GridPositionY;

        if(newX == currentX+1 && newY == currentY || newX == currentX - 1 && newY == currentY 
            || newY == currentY + 1 && newX == currentX || newY == currentY - 1 && newX == currentX)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void LoadMaps()
    {
        JSONObject mapJson = new JSONObject(LoadString(GameConstants.MAP_FILE_LOCATION));
        Maps = new List<MapVO>();
        List<int> MapIDs = new List<int>();
        //Debug.Log("[Json] show json: " + mapJson.ToString());
        for (int i = 0; i < mapJson.list.Count; i++)
        {
            //Debug.Log("[Json] maps " + mapJson[i].ToString());
            MapVO newMap = new MapVO();
            newMap.SetID((int)mapJson[i][0].i);
            MapIDs.Add(newMap.MapID);
            Debug.Log("Map ID: " + newMap.MapID);
            List<TileVO> changeTiles = new List<TileVO>();
            for (int t = 1; t < mapJson[i].Count; t++)
            {
                //Debug.Log("tiles:" + mapJson[i][t][0].ToString());
                TileVO newTile = new TileVO();
                if (Enum.IsDefined(typeof(TileTypeCategory), mapJson[i][t][GameConstants.CATEGORY_KEY].str))
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
            Maps.Add(newMap);
        }
    }

    public static string LoadString(string fileName)
    {
        string loadedData = default(string);
        string filePath = Application.persistentDataPath + "/" + fileName;
        if (File.Exists(filePath))
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                try
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    loadedData = (string)bf.Deserialize(fileStream);
                }
                catch (Exception)
                {

                }
            }
        }
        return loadedData == default(string) ? "" : loadedData;
    }
}
