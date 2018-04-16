using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Consistent ID", menuName = "Consistent ID", order = 1)]
public class ConsistentMapID : ScriptableObject {

    private int ConsistentID;

    public int GetConsistantID()
    {
        ConsistentID++;
        return ConsistentID;
        
    }
}
