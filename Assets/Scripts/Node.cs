using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public List<Node> Neighbors;
    public int X;
    public int Y;
    public float EnergyToThisTile;
    public Node()
    {
        Neighbors = new List<Node>();
    }

    public float DistanceTo(Node n)
    {
        return Vector2.Distance(
            new Vector2(X, Y),
            new Vector2(n.X, n.Y));
    }
}
