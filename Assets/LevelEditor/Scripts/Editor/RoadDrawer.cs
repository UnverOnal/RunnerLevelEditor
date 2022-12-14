using LevelEditor.Scripts.Road;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace LevelEditor.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(RoadData))]
    public class RoadDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
        
            //Put code here
            // Calculate rects
            var typeRect = new Rect(position.x - 200, position.y + 25, position.width + 200, 20f);
            var roadMeshRect =
                new Rect(position.x - 200, position.y + 50f, position.width + 200, 20f);
            var unitScaleRect = new Rect(position.x - 200, position.y + 75f, position.width + 200, 20f);
            var roadScaleRect = new Rect(position.x - 200, position.y + 100f, position.width + 200, 20f);

            // Draw fields
            EditorGUI.PropertyField(typeRect, property.FindPropertyRelative("type"), new GUIContent("Type"));
            EditorGUI.PropertyField(roadMeshRect, property.FindPropertyRelative("roadMesh"),
                new GUIContent("RoadMesh"));
            EditorGUI.PropertyField(unitScaleRect, property.FindPropertyRelative("unitScale"),
                new GUIContent("UnitScale"));
            EditorGUI.PropertyField(roadScaleRect, property.FindPropertyRelative("roadScale"),
                new GUIContent("RoadScale"));

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 16f * 8;
        }
    }
}
#endif
