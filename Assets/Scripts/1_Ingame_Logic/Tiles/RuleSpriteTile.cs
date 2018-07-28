using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuleSpriteTile : ClickableTile {

    public MeshRenderer MeshRenderer;
    public Material DefaultMaterial;
    [Header("Erste Spalte")]
    public Material CornerLeftUp;
    public Material LeftMiddle;
    public Material CornerLeftDown;

    [Header("Zweite Spalte")]
    public Material MiddleTop;
    public Material MiddleMiddle;
    public Material MiddleDown;

    [Header("Dritte Spalte")]
    public Material CornerRightUp;
    public Material RightMiddle;
    public Material CornerRightDown;


    

    public override void HandleTileMaterial()
    {
        CheckNeigbors();
    }

    public void CheckNeigbors()
    {
        if (Map.GetTypeOfTile(GridPositionX, GridPositionY - 1) != TileTypeCategory.Island)
        {
            if (Map.GetTypeOfTile(GridPositionX - 1, GridPositionY) != TileTypeCategory.Island)
            {
                MeshRenderer.material = CornerLeftDown;
                /*
                   * . . .
                   * - . .
                   * . - .
                   */
            }
            else
            {
                /*
                   * . . .
                   * + . .
                   * . - .
                   */
                if (Map.GetTypeOfTile(GridPositionX + 1, GridPositionY) == TileTypeCategory.Island)
                {
                    MeshRenderer.material = MiddleDown;
                    /*
                    * . . .
                    * + . +
                    * . - .
                    */
                }
                else
                {
                    /*
                    * . . .
                    * + . -
                    * . - .
                    */
                    MeshRenderer.material = CornerRightDown;
                }
            }
        }
        else if (Map.GetTypeOfTile(GridPositionX, GridPositionY - 1) == TileTypeCategory.Island)
        {
            if (Map.GetTypeOfTile(GridPositionX - 1, GridPositionY) != TileTypeCategory.Island)
            {
                if(Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == TileTypeCategory.Island)
                {
                    MeshRenderer.material = LeftMiddle;
                    /*
                    * . + .
                    * - . .
                    * . + .
                    */
                }
                else
                {
                    MeshRenderer.material = CornerLeftUp;
                    /*
                    * . - .
                    * - . .
                    * . + .
                    */
                }

            }
            else
            {
                /*
                 * 
                 * +
                 * . +
                 */

                if(Map.GetTypeOfTile(GridPositionX, GridPositionY + 1) == TileTypeCategory.Island)
                {
                    /*
                    *   +
                    * +
                    * . +
                    */
                    if (Map.GetTypeOfTile(GridPositionX + 1, GridPositionY) == TileTypeCategory.Island)
                    {
                        /*
                        *   +
                        * +   +
                        * . +
                        */
                        MeshRenderer.material = MiddleMiddle;
                    }
                    else
                    {
                        /*
                        *   +
                        * +   -
                        * . +
                        */
                        MeshRenderer.material = RightMiddle;
                    }


                }
                else
                {
                    /*
                    *   -
                    * +
                    * . +
                    */
                    MeshRenderer.material = CornerRightUp;
                    if (Map.GetTypeOfTile(GridPositionX + 1, GridPositionY) == TileTypeCategory.Island)
                    {
                        /*
                        *   -
                        * +   +
                        * . +
                        */
                        MeshRenderer.material = MiddleTop;
                    }
                }
            }
        }
        else
        {
            MeshRenderer.material = DefaultMaterial;
        }
    }
}
