using UnityEngine;

[System.Serializable]
public class RuleTile {

    [SerializeField]
    public bool[] Neighbors = new bool[8];
    public Texture Sprite;
    public Material Material;

}
