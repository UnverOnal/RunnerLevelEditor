using LevelEditor.Scripts.Level;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace LevelEditor.Scripts.Editor
{
    [CustomPropertyDrawer(typeof(LevelCreationData))]
    public class LevelCreationDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var levelNameRect = new Rect(position.x - 200, position.y + 25, position.width + 200, position.height / 6f);
            var pathToSaveInRect = new Rect(position.x - 200, position.y + 50, position.width + 200, position.height / 6f);
            var levelIndexRect =
                new Rect(position.x - 200, position.y + 75f, position.width + 200, position.height / 6f);

            // Draw fields
            EditorGUI.PropertyField(levelNameRect, property.FindPropertyRelative("levelName"), new GUIContent("LevelName"));
            EditorGUI.PropertyField(pathToSaveInRect, property.FindPropertyRelative("pathToSaveIn"), new GUIContent("PathToSaveIn"));
            EditorGUI.PropertyField(levelIndexRect, property.FindPropertyRelative("levelIndex"),
                new GUIContent("LevelIndex"));

            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return 16f * 7;
        }
    }
}
#endif
