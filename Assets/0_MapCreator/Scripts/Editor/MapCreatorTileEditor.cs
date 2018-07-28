namespace MapCreator
{
    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(MapCreatorTile))]
    public class MapCreatorTileEditor : Editor
    {

        public void OnSceneGUI()
        {
            if (Event.current.type == EventType.MouseDown)
            {
                RaycastHit hit;
                Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.GetComponent<MapCreatorTile>() != null)
                    {
                        hit.collider.gameObject.GetComponent<MapCreatorTile>().OnChangeTile();
                    }
                }
            }
        }
    }
}