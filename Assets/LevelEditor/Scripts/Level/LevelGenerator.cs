using System.IO;
using LevelEditor.Scripts.Placeable;
using LevelEditor.Scripts.Road;
using UnityEditor;
using UnityEngine;

namespace LevelEditor.Scripts.Level
{
    public class LevelGenerator : MonoBehaviour
    {
        [HideInInspector] public RoadData roadData;
        [HideInInspector] public LevelCreationData levelCreationData;
        [HideInInspector] public PlaceableData[] placeableData;
        [HideInInspector] public Material skybox;

        private GameObject _roadGameObject;
        private Road.Road _road;

        private GameObject _placeableGameObject;
        private Placement _placement;

        //Also updates
        public void CreateRoad()
        {
            _road ??= new Road.Road(transform, levelCreationData.levelIndex);
            _roadGameObject = _road.Create(roadData);

            var sceneView = SceneView.sceneViews[0] as SceneView;
            if (sceneView != null)
                sceneView.in2DMode = false;
        }

        //Also updates
        public void CreatePlacements()
        {
            _placement ??= new Placement(_road.splineComputer, transform);
            _placeableGameObject = _placement.Create(placeableData);
        }

        public void CreateLevel()
        {
            try
            {
                var levelName = levelCreationData.levelName + (levelCreationData.levelIndex + 1);
                var pathToSaveIn = System.IO.Path.Combine(levelCreationData.pathToSaveIn, levelName + ".prefab");

                var levelGameObject = CreateLevelPrefab(levelName);
                _roadGameObject.transform.SetParent(levelGameObject.transform);
                _placeableGameObject.transform.SetParent(levelGameObject.transform);
                levelGameObject.GetComponent<Level>().skybox = skybox;

                _road.Bake();
                _placement.Bake();

                PrefabUtility.SaveAsPrefabAsset(levelGameObject, pathToSaveIn);
                PrefabUtility.UnloadPrefabContents(levelGameObject);

                levelCreationData.levelIndex++;

                ResetStates();
            }
            catch
            {
                Debug.LogError("One or more of the following elements is missing : Road, Placeables...");
                throw;
            }
        

        }

        public void RemoveLastLevel()
        {
            if (levelCreationData.levelIndex <= 0) return;

            var levelName = levelCreationData.levelName + levelCreationData.levelIndex;
            var pathToDelete = System.IO.Path.Combine(levelCreationData.pathToSaveIn, levelName + ".prefab");
            File.Delete(pathToDelete);
            AssetDatabase.Refresh();

            levelCreationData.levelIndex--;
        }

        public void CreatePath()
        {
            //Create or get path object
            var pathObject = roadData.pathCreatorGameObject;
            if (!pathObject)
            {
                pathObject = Instantiate(Resources.Load<GameObject>("PathCreator"), transform);
                pathObject.name = "Path";
                roadData.pathCreatorGameObject = pathObject;
            }

            //Display in scene
            Selection.activeObject = pathObject;
            SceneView.lastActiveSceneView.FrameSelected();
            var sceneView = SceneView.sceneViews[0] as SceneView;
            if (sceneView != null)
                sceneView.in2DMode = true;
        }

        private GameObject CreateLevelPrefab(string levelName)
        {
            levelCreationData.levelTemplate = Resources.Load<GameObject>("levelTemplate");
            var templatePath = AssetDatabase.GetAssetPath(levelCreationData.levelTemplate);

            var level = PrefabUtility.LoadPrefabContents(templatePath);
            level.name = levelName;

            return level;
        }

        private void ResetStates()
        {
            _road = null;
            _placement = null;
        }
    }
}