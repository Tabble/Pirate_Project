using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Consistent ID", menuName = "Consistent ID", order = 1)]
public class ConsistentMapID : ScriptableObject {

    private int ConsistentID;

    public int GetConsistantID()
    {
        ConsistentID++;
        AssetDatabase.SaveAssets();
        return ConsistentID;
        
    }
}
