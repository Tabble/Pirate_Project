using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RuleTileTest : MonoBehaviour {

    public List<RuleTile> RuleTiles;
   
    public RuleTileTest()
    {
        RuleTiles = new List<RuleTile>();
        RuleTiles.Add(new RuleTile());
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

[System.Serializable]
public enum NeighborStatus
{
    egal,
    no,
    same
}
