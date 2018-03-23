using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ChangeTile))]
public class ChangeTileEditor : Editor{

    public void OnSceneGUI()
    {
        if (Event.current.type == EventType.MouseDown)
        {
            RaycastHit hit;
            Ray ray = HandleUtility.GUIPointToWorldRay(Event.current.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.gameObject.GetComponent<ChangeTile>() != null)
                {
                    hit.collider.gameObject.GetComponent<ChangeTile>().OnChangeTile();
                }
            }
        }
    }
}
