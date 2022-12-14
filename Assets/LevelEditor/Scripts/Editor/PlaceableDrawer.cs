using LevelEditor.Scripts.Placeable;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace LevelEditor.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(PlaceableData))]
    public class PlaceableDrawer : PropertyDrawer
    {
        private bool _isExpanded;

        // Draw the property inside the given rect
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            //If the placeable array fold out , position properties again
            var amountToSlideDown = 0f;
            var arraySize = 0;
            if (property.FindPropertyRelative("placeables").isExpanded)
            {
                arraySize = property.FindPropertyRelative("placeables").arraySize;
                arraySize = arraySize == 0 ? 1 : arraySize;
                amountToSlideDown = arraySize * 20f + 25f;
            }
            
            #region CalculateRects
            
            var typeRect = new Rect(position.x - 200, position.y + 25, position.width + 200, 20f);
            var placeableRect = new Rect(position.x - 200, position.y + 50, position.width + 200, 20f);
            var minPositionOffsetRect =
                new Rect(position.x - 200, position.y + 75f + amountToSlideDown, position.width + 200, 20f);            
            var maxPositionOffsetRect =
                new Rect(position.x - 200, position.y + 100f + amountToSlideDown, position.width + 200, 20f);
            var minAngleOffsetRect = new Rect(position.x - 200, position.y + 125f + amountToSlideDown, position.width + 200, 20f);
            var maxAngleOffsetRect = new Rect(position.x - 200, position.y + 150f + amountToSlideDown, position.width + 200, 20f);
            var minScaleOffset = new Rect(position.x - 200, position.y + 175f + amountToSlideDown, position.width + 200, 20f);
            var maxScaleOffset = new Rect(position.x - 200, position.y + 200f + amountToSlideDown, position.width + 200, 20f);
            var countRect = new Rect(position.x - 200, position.y + 225f + amountToSlideDown, position.width + 200, 20f);
            var evaluateOffset = new Rect(position.x - 200, position.y + 250f + amountToSlideDown, position.width + 200, 20f);
            
            #endregion

            #region DrawFields
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), new GUIContent("Type"));
            EditorGUI.PropertyField(placeableRect, property.FindPropertyRelative("placeables"), new GUIContent("Prefabs"));
            EditorGUI.PropertyField(minPositionOffsetRect, property.FindPropertyRelative("minPositionOffset"),
                new GUIContent("MinPositionOffset"));            
            EditorGUI.PropertyField(maxPositionOffsetRect, property.FindPropertyRelative("maxPositionOffset"),
                new GUIContent("MaxPositionOffset"));
            EditorGUI.PropertyField(minAngleOffsetRect, property.FindPropertyRelative("minAngleOffset"),
                new GUIContent("MinAngleOffset"));            
            EditorGUI.PropertyField(maxAngleOffsetRect, property.FindPropertyRelative("maxAngleOffset"),
                new GUIContent("MaxAngleOffset"));
            EditorGUI.PropertyField(minScaleOffset, property.FindPropertyRelative("minScaleOffset"),
                new GUIContent("MinScaleOffset"));            
            EditorGUI.PropertyField(maxScaleOffset, property.FindPropertyRelative("maxScaleOffset"),
                new GUIContent("MaxScaleOffset"));
            EditorGUI.PropertyField(countRect, property.FindPropertyRelative("count"), new GUIContent("Count"));
            EditorGUI.PropertyField(evaluateOffset, property.FindPropertyRelative("evaluateOffset"),
                new GUIContent("Evaluate Offset"));
            #endregion
            
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            var arraySize = 0;
            if (property.FindPropertyRelative("placeables").isExpanded)
            {
                arraySize = property.FindPropertyRelative("placeables").arraySize;
                arraySize = arraySize == 0 ? 1 : arraySize;
            }
            return 30f * 10 + arraySize * 20f;
        }
    }
}
#endif