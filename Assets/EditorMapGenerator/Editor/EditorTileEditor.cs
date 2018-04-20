using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EditorTile))]
public class EditorTileEditor : Editor{

    public void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            RaycastHit hit;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<EditorTile>() != null)
                {
                    hit.collider.gameObject.GetComponent<EditorTile>().OnChangeTile();
                }
            }
        }
    }
}
