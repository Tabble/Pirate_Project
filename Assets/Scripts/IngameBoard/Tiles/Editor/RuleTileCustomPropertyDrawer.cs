using UnityEditor;
using UnityEngine;

[CustomPropertyDrawer(typeof(RuleTile))]
public class RuleTileCustomPropertyDrawer : PropertyDrawer
{
    Rect newPosition;
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect newPosition = position;
        newPosition.y += 20;
        SerializedProperty rows = property.FindPropertyRelative("Neighbors");
        //SerializedProperty sprite = property.FindPropertyRelative("Sprite");
        var spriteRect = new Rect(newPosition.x + 150, newPosition.y, newPosition.width - 300, 20);
        //EditorGUI.ObjectField(spriteRect, sprite, typeof(Sprite));
        ////EditorGUI.ObjectField(spriteRect,sprite,typeof(Texture));
        SerializedProperty material = property.FindPropertyRelative("Material");
        EditorGUI.PropertyField(spriteRect, material, GUIContent.none);

        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                newPosition.height = 20f;
                if (index < rows.arraySize)
                {
                    newPosition.width = 30;
                    if (index == 4)
                    {
                        EditorGUI.PropertyField(new Rect(newPosition.x+ newPosition.width,newPosition.y, newPosition.width, newPosition.height), rows.GetArrayElementAtIndex(index), GUIContent.none);
                    }
                    else
                    {
                        EditorGUI.PropertyField(newPosition, rows.GetArrayElementAtIndex(index), GUIContent.none);
                    }

                    newPosition.x += newPosition.width;
                    if (i == 1 && j == 1)
                    {
                        j = 3;
                    }
                    index++;
                }
            }
            newPosition.x = position.x;
            newPosition.y += 20;
        }

    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 20 * 4;
    }

}
