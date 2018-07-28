using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates the Tile Map for the game.
/// </summary>
public class TileMapGameController : MonoBehaviour {

    public MapsSaver MapsSaver;
    public int MapID = 2;
    public GameObject PlayerUnit;
    public TileType[] tileTypes;
    Node[,] graph;
    TileTypeCategory[,] tiles;
    int mapSizeX = 10;
    int mapSizeY = 10;
    float eneregyToGoal = 0;
    Node targetNode;
    private List<MapVO> Maps = new List<MapVO>();
    private Vector2 playerStart = new Vector2();


    private void Start()
    {
        // TODO: change when prototype phase is finished
        LoadMaps();
        MapID = Maps[0].MapID;
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
                    ct.HandleTileMaterial();
                    if(tt.Category == TileTypeCategory.Goal)
                    {
                        targetNode = new Node
                        {
                            X = x,
                            Y = y
                        };
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
        TileType tt = new TileType
        {
            Category = tiles[x, y]
        };
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
        Maps = MapsSaver.LoadAllMaps();
    }

    public TileTypeCategory GetTypeOfTile(int x, int y)
    {
        if (x >= 0 && x <= 8 && y >= 0 && y <= 8)
            return tiles[x, y];
        else return TileTypeCategory.Edge;
    }
}
