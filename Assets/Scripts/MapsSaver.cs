using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "MapsSaver", menuName = "MapSaver", order = 1)]
public class MapsSaver : ScriptableObject {

    [SerializeField]
    private List<MapVO> AllMaps;

    public void AddNewMap(MapVO newMap)
    {
        if(AllMaps == null)
        {
            AllMaps = new List<MapVO>();
        }
        if(newMap != null)
        {
            AllMaps.Add(newMap);
        }
        AssetDatabase.SaveAssets();
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
    }

    public List<MapVO> LoadAllMaps()
    {
        return AllMaps;
    }

    public void SetAllMaps (List<MapVO> list)
    {
        AllMaps = list;
    }
}
