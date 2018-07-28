namespace MapCreator
{
    using UnityEngine;
    using UnityEditor;

    [CustomEditor(typeof(MapCreator))]
    public class MapCreatorEditor : Editor
    {

        public MapsSaver MapsSaver;
        private MapCreator map;
        private int currentMapID = 0;
        public override void OnInspectorGUI()
        {
            map = target as MapCreator;
            base.OnInspectorGUI();
            if (map.MapsSaver == null)
            {
                base.OnInspectorGUI();
            }
            if (map.ConsistentMapID == null)
            {
                base.OnInspectorGUI();
            }
            if (GUILayout.Button("Reset Map"))
            {

                map.GenerateMapToDrawOn();
                currentMapID = 0;
            }
            if (GUILayout.Button("Save NEW Map"))
            {

                map.SaveNewMap();
            }
            if (GUILayout.Button("LoadMaps"))
            {

                map.LoadMaps();
            }
            GUILayout.Label("Current ID :" + currentMapID.ToString(), EditorStyles.boldLabel);
            DrawMapIdsInspector();


        }

        private void DrawMapIdsInspector()
        {
            GUILayout.Space(5);
            for (int i = 0; i < map.MapIds.Length; i++)
            {
                DrawMapIds(i);
            }
        }

        private void DrawMapIds(int index)
        {
            if (index < 0 || index >= map.MapIds.Length)
            {
                return;
            }
            GUILayout.BeginHorizontal();
            {
                GUILayout.Label("Map ID", EditorStyles.boldLabel);
                GUILayout.Label(map.MapIds[index].ToString());
                if (GUILayout.Button("Show"))
                {
                    map.ShowMap(map.MapIds[index]);
                    currentMapID = map.MapIds[index];
                }
                if (GUILayout.Button("Overwrite"))
                {

                    if (currentMapID != map.MapIds[index])
                    {
                        EditorApplication.Beep();
                        if (EditorUtility.DisplayDialog("Wrong ID", "Do you really want to overwrite " +
                            map.MapIds[index] + " when you chose map ID " + currentMapID, "overwrite", "cancel"))
                        {
                            map.OverwrideMapWithID(map.MapIds[index]);
                        }
                    }
                    else
                    {
                        map.OverwrideMapWithID(map.MapIds[index]);
                    }
                }

                if (GUILayout.Button("Delete"))
                {
                    EditorApplication.Beep();
                    if (EditorUtility.DisplayDialog("Delete", "Are you sure you want to delete this map with ID" + map.MapIds[index],
                        "Delete", "Cancel"))
                    {
                        map.DeleteMapWithID(map.MapIds[index]);
                    }
                }
            }
            GUILayout.EndHorizontal();
        }
    }
}