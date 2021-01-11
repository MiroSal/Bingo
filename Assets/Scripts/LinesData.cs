using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[System.Serializable]
public class ArrayLayout
{
    [System.Serializable]
    public struct rowData
    {
        public bool[] row;
    }

    public rowData[] rows = new rowData[5]; //5x5 grid
}

/// <summary>
/// Make booleans appear in horizontal lines in inspector
/// </summary>
[CustomPropertyDrawer(typeof(ArrayLayout))]
public class CustPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.PrefixLabel(position, label);
        Rect newposition = position;
        newposition.y += 18f;
        SerializedProperty data = property.FindPropertyRelative("rows");

        for (int j = 0; j < 5; j++)
        {
            SerializedProperty row = data.GetArrayElementAtIndex(j).FindPropertyRelative("row");
            newposition.height = 18f;

            if (row.arraySize != 5)
                row.arraySize = 5;

            newposition.width = position.width / 8;

            for (int i = 0; i < 5; i++)
            {
                EditorGUI.PropertyField(newposition, row.GetArrayElementAtIndex(i), GUIContent.none);
                newposition.x += newposition.width; 
            }

            newposition.x = position.x;
            newposition.y += 23f;
        }
    }

    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
    {
        return 18f * 8;
    }
}

/// <summary>
/// ScriptableObject for Setting current Gamemodes wanted lines
/// </summary>
[System.Serializable]
[CreateAssetMenu(fileName = "LineData", menuName = "ScriptableObjects/LineData", order = 1)]
public class LinesData : ScriptableObject
{
    [SerializeField]
    private ArrayLayout[] bingoLineData = new ArrayLayout[1];

    public ArrayLayout[] GetbingoLineData()
    {
        return bingoLineData;
    }

}