using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Consistent ID", menuName = "Consistent ID", order = 1)]
[Serializable]
public class ConsistentMapID : ScriptableObject {

    private int ConsistentID;
    private string gameDataProjectFilePath = "/Resources/ConsistendID.json";
    public int GetConsistantID()
    {
        SaveConsistentIDData();
        LoadConsistentIDData();
        ConsistentID++;
        SaveConsistentIDData();
        return ConsistentID;
    }

    private void LoadConsistentIDData()
    {
        string filePath = Application.dataPath + gameDataProjectFilePath;
        if (File.Exists(filePath))
        {
            JSONObject idJson = new JSONObject(File.ReadAllText(filePath));
            ConsistentID = (int)idJson[0].i;
        }
    }

    private void SaveConsistentIDData()
    {
        JSONObject json = new JSONObject();
        json.AddField("ConsistendID", ConsistentID);
        string dataAsJson = json.ToString();
        string filePath = Application.dataPath + gameDataProjectFilePath;
        File.WriteAllText(filePath, dataAsJson);
    }
}
