using LevelEditor.Scripts.Level;
using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
namespace LevelEditor.Scripts.Editor
{
    [CustomEditor(typeof(LevelGenerator))]
    public class RunnerLevelEditor : UnityEditor.Editor
    {
        private LevelGenerator _levelGenerator;

        private SerializedProperty _placeable;
        private SerializedProperty _road;
        private SerializedProperty _levelCreation;

        private void OnEnable()
        {
            _levelGenerator = (LevelGenerator)target;
            EditorUtility.SetDirty(_levelGenerator);

            _placeable = serializedObject.FindProperty("placeableData");
            _road = serializedObject.FindProperty("roadData");
            _levelCreation = serializedObject.FindProperty("levelCreationData");
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            serializedObject.Update();

            #region Path

            GUILayout.Label("Road Path", EditorStyles.boldLabel);
            EditorGUILayout.ObjectField("Path Object", _levelGenerator.roadData.pathCreatorGameObject, typeof(GameObject),
                false);
            if (GUILayout.Button("Create/Update Path", GUILayout.MaxWidth(250f))) _levelGenerator.CreatePath();
            GUILayout.Space(10f);

            #endregion

            #region RoadData

            GUILayout.Label("Road", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_road, new GUIContent("Road"));

            if (GUILayout.Button("Create/Update Road", GUILayout.MaxWidth(250f))) _levelGenerator.CreateRoad();
            GUILayout.Space(10f);

            #endregion

            #region Placeables
        
            EditorGUILayout.PropertyField(_placeable, new GUIContent("Placeables",
                "All the placeables like environment, doors, obstacles, collectables can be put here."));
            if (GUILayout.Button("Create/Update Placeables", GUILayout.MaxWidth(250f))) _levelGenerator.CreatePlacements();
            GUILayout.Space(10f);
        
            #endregion

            #region Skybox

            GUILayout.Label("Skybox", EditorStyles.boldLabel);
            _levelGenerator.skybox = (Material)EditorGUILayout.ObjectField("Skybox", _levelGenerator.skybox, typeof(Material),
                false);
            GUILayout.Space(10f);
            
            #endregion
        
            #region CreateLevel
        
            GUILayout.Label("Level Creation", EditorStyles.boldLabel);
            EditorGUILayout.PropertyField(_levelCreation, new GUIContent("Level"));
            if (GUILayout.Button("Create/Update Level", GUILayout.MaxWidth(250f))) _levelGenerator.CreateLevel();
        
            if (GUILayout.Button("Remove Last Level", GUILayout.MaxWidth(250f))) _levelGenerator.RemoveLastLevel();
        
            #endregion

            serializedObject.ApplyModifiedProperties();
        }
    }
}
#endif