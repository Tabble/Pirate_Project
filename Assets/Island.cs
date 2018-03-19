using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Island : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    void OnMouseDown()
    {
        Debug.Log("On Island Clicked");
        Vector3Int[] temp = gameObject.GetComponent<GridInformation>().GetAllPositions("Island");
        foreach(var obj in temp)
        {
            Debug.Log("Island Position" + temp);
        }
    }
}
