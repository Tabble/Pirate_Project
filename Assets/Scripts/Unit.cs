using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

    public int GridPositionX;
    public int GridPositionY;

    public TileMapControllerSelfmade Map;

    public List<Node> CurrentPath = null;

    public void SetCurrentPositionInGrid(int x , int y)
    {
        GridPositionX = x;
        GridPositionY = y;
        gameObject.transform.position = Map.TileCoordinatsToWoldCoordinates(x, y);
    }

    private void Update()
    {
        if (CurrentPath != null)
        {
            int currentNode = 0;
            while (currentNode < CurrentPath.Count - 1)
            {
                Vector3 start = Map.TileCoordinatsToWoldCoordinates(CurrentPath[currentNode].X, CurrentPath[currentNode].Y);
                Vector3 end = Map.TileCoordinatsToWoldCoordinates(CurrentPath[currentNode + 1].X, CurrentPath[currentNode + 1].Y);
                Debug.DrawLine(start, end, Color.red);
                currentNode++;
            }
        }
    }

}
