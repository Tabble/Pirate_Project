using UnityEngine;

/// <summary>
/// Descripes a Rule for a RuleTile.
/// </summary>
[System.Serializable]
public class RuleTileRule {

    [SerializeField]
    public bool[] Neighbors = new bool[8];
    public Texture Sprite;
    public Material Material;
}
