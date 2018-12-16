namespace MapCreator
{
    using System.Collections.Generic;
    using UnityEngine;

    /// <summary>
    /// This class handles creating a new map in the Editor
    /// </summary>
    public class MapCreator : MonoBehaviour
    {
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
            if (tileParent == null)
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
                    newTile.GetComponent<MapCreatorTile>().PositionX = x;
                    newTile.GetComponent<MapCreatorTile>().PositionY = y;
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
            List<MapCreatorTile> tiles = new List<MapCreatorTile>();
            if (mapWithID != null)
            {
                for (int x = 0; x < MapSize.x; x++)
                {
                    for (int y = 0; y < MapSize.y; y++)
                    {
                        Vector3 tilePosition = new Vector3(-MapSize.x / 2 + 0.5f + x, 0, -MapSize.y / 2 + 0.5f + y);
                        Transform newTile = Instantiate(TilePrefab, tilePosition, Quaternion.Euler(Vector3.right * 90)) as Transform;
                        newTile.SetParent(tileParent.transform);
                        tiles.Add(newTile.GetComponent<MapCreatorTile>());
                    }
                }
                for (int i = 0; i < tiles.Count; i++)
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
            MapsSaver.DeleteMapWithID(ID);
            LoadMaps();
        }

        public void LoadMaps()
        {
            if (MapsSaver.LoadAllMaps().Count > 0)
            {
                Maps = MapsSaver.LoadAllMaps();
                List<int> ids = new List<int>();
                foreach (var map in Maps)
                {
                    ids.Add(map.MapID);
                }
                MapIds = ids.ToArray();
            }
            else
            {
                Debug.Log("[MapGenerator] no maps saved");
            }
        }

        public void SaveNewMap()
        {
            MapCreatorTile[] tiles = tileParent.GetComponentsInChildren<MapCreatorTile>();
            mapID = ConsistentMapID.GetConsistantID();
            JSONObject map = new JSONObject();
            map.AddField(GameConstants.MAP_ID_KEY, mapID);
            List<TileVO> tileVOs = new List<TileVO>();
            foreach (var tile in tiles)
            {
                map.Add(tile.GetTileJson());
                tileVOs.Add(new TileVO(tile.Category, tile.PositionX, tile.PositionY));
            }
            MapVO newMap = new MapVO();
            newMap.SetID(mapID);
            newMap.SetTiles(tileVOs.ToArray());
            MapsSaver.AddNewMap(newMap);
            LoadMaps();
        }

        public void OverrideMap(int id)
        {
            MapCreatorTile[] tiles = tileParent.GetComponentsInChildren<MapCreatorTile>();
            MapVO mapWithID = MapsSaver.GetMapWithID(id);

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

            LoadMaps();
        }
    }
}