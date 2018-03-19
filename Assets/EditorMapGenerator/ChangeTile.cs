using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeTile : MonoBehaviour {

    public MeshRenderer Renderer;
    public Material[] DefaultMaterial;
    private int matIndex = 0;
    
	public void ChangeMaterial()
    {
        if(matIndex < DefaultMaterial.Length - 1)
        {
            matIndex++;
        }
        else
        {
            matIndex = 0;
        }
        Renderer.material = DefaultMaterial[matIndex];
    }
}
