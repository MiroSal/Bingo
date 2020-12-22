using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[System.Serializable]
//public class BingoLineData
//{
//    public bool line1;
//    public bool line2;
//    public bool line3;
//    public bool line4;
//    public bool line5;
//}

//// IngredientDrawer
//[CustomPropertyDrawer(typeof(BingoLineData))]
//public class DataDrawer : PropertyDrawer
//{
//    // Draw the property inside the given rect
//    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
//    {
//        // Using BeginProperty / EndProperty on the parent property means that
//        // prefab override logic works on the entire property.
//        EditorGUI.BeginProperty(position, label, property);

//        // Draw label
//        position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

//        // Don't make child fields be indented
//        var indent = EditorGUI.indentLevel;
//        EditorGUI.indentLevel = 0;

//        // Calculate rects
//        var line1Rect = new Rect(position.x - 30, position.y, position.width, position.height);
//        var line2Rect = new Rect(position.x, position.y, position.width, position.height);
//        var line3Rect = new Rect(position.x + 30, position.y, position.width, position.height);
//        var line4Rect = new Rect(position.x + 60, position.y, position.width, position.height);
//        var line5Rect = new Rect(position.x + 90, position.y, position.width, position.height);

//        // Draw fields - pass GUIContent.none to each so they are drawn without labels
//        EditorGUI.PropertyField(line1Rect, property.FindPropertyRelative("line1"), GUIContent.none);
//        EditorGUI.PropertyField(line2Rect, property.FindPropertyRelative("line2"), GUIContent.none);
//        EditorGUI.PropertyField(line3Rect, property.FindPropertyRelative("line3"), GUIContent.none);
//        EditorGUI.PropertyField(line4Rect, property.FindPropertyRelative("line4"), GUIContent.none);
//        EditorGUI.PropertyField(line5Rect, property.FindPropertyRelative("line5"), GUIContent.none);
//        // Set indent back to what it was
//        EditorGUI.indentLevel = indent;

//        EditorGUI.EndProperty();
//    }
//}

//[System.Serializable]
//public class BLineData
//{
//    public BingoLineData[] bingoLineData = new BingoLineData[5];
//}


//New version with array
[System.Serializable]
public class ArrayLayout
{
    [System.Serializable]
    public struct rowData
    {
        public bool[] row;
    }

    public rowData[] rows = new rowData[5]; //Grid of 5x5
}

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

[System.Serializable]
[CreateAssetMenu(fileName = "LineData", menuName = "ScriptableObjects/LineData", order = 1)]
public class LineData : ScriptableObject
{
    //public BLineData[] bingoLineData = new BLineData[1];

    public ArrayLayout[] bingoLineData = new ArrayLayout[1];

}

