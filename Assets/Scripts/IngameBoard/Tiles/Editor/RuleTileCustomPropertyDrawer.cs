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
        SerializedProperty sprite = property.FindPropertyRelative("Sprite");
        var spriteRect = new Rect(newPosition.x + 250 , newPosition.y,newPosition.width -300, newPosition.height-20);
        //EditorGUI.ObjectField(spriteRect,sprite,typeof(Sprite));
        EditorGUI.ObjectField(spriteRect,sprite,typeof(Sprite));

        int index = 0;
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if(index < rows.arraySize)
                {
                    if(index == 4)
                    {
                        EditorGUI.PropertyField(new Rect(newPosition.x+80, newPosition.y, 80, 80), rows.GetArrayElementAtIndex(index), GUIContent.none);
                    }
                    else
                    {
                        EditorGUI.PropertyField(new Rect(newPosition.x, newPosition.y, 80, 80), rows.GetArrayElementAtIndex(index), GUIContent.none);
                    }
                    
                    newPosition.x += 80;
                    if(i == 1 && j == 1)
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
        return 20 *6;
    }

}
