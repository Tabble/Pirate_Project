using UnityEngine;

[System.Serializable]
public class RuleTile {

    [SerializeField]
    public NeighborStatus[] Neighbors = new NeighborStatus[8];
    public Sprite Sprite;
    public Material Material;

}
