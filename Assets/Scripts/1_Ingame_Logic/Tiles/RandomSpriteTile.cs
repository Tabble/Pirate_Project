using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpriteTile : ClickableTile
{
    public MeshRenderer MeshRenderer;
    public Material[] RandomMaterials;

    private void Start()
    {
        if(RandomMaterials != null)
        {
            MeshRenderer.material = RandomMaterials[Random.Range(0, RandomMaterials.Length)];
        }
    }

    public override void HandleOnMouseUp()
    {
        base.HandleOnMouseUp();
    }

}
