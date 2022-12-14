using System;
using Dreamteck.Splines;
using UnityEditor;
using UnityEngine;
using MeshUtility = Dreamteck.MeshUtility;
using Object = UnityEngine.Object;

namespace LevelEditor.Scripts.Road
{
    public class Road
    {
        public SplineComputer splineComputer;
        private RoadData _roadData;

        private GameObject _roadGameObject;

        private readonly Transform _parent;

        private SplineMesh.Channel _channel;
        private SplineMesh _splineMesh;

        private readonly int _index;

        public Road(Transform parent, int index)
        {
            _parent = parent;
            _index = index;
        }

        //Also updates
        public GameObject Create(RoadData roadData)
        {
            _roadData = roadData;

            CreateRoadGameObject();
            SetSpline();

            CreateRoadMesh();
            SetRoadMesh();

            return _roadGameObject;
        }

        public void Bake()
        {
            _splineMesh.Bake(true, false);
            var assetMesh = MeshUtility.Copy(_roadGameObject.GetComponent<MeshFilter>().sharedMesh);
            AssetDatabase.CreateAsset(assetMesh, "Assets/LevelEditor/Models/RoadMeshes/road" + _index + ".asset");

            _roadGameObject.GetComponent<MeshFilter>().sharedMesh = assetMesh;
            Object.DestroyImmediate(_roadGameObject.GetComponent<SplineMesh>());
            Object.DestroyImmediate(_roadGameObject.GetComponent<SplineComputer>());
        }

        private void CreateRoadGameObject()
        {
            if (_roadGameObject) return;

            _roadGameObject = new GameObject("Road");
            splineComputer = _roadGameObject.AddComponent<SplineComputer>();
            _roadGameObject.transform.SetParent(_parent);
        }

        private void CreateRoadMesh()
        {
            if (_splineMesh != null) return;

            _splineMesh = _roadGameObject.AddComponent<SplineMesh>();
            _splineMesh.RemoveChannel(0);
            _channel = _splineMesh.AddChannel(_roadData.roadMesh.GetComponent<MeshFilter>().sharedMesh, "road");
        }

        private void SetSpline()
        {
            //Get segments
            var segments = _roadData.pathCreatorGameObject.GetComponent<PathCreator>().path
                .CalculateEvenlySpacedPoints(0.5f);

            var points = new SplinePoint[segments.Length];
            for (var i = 0; i < points.Length; i++)
                points[i] = new SplinePoint
                {
                    size = 1f,
                    color = Color.white,
                    normal = Vector3.up,
                    position = (Vector3.forward * (segments[i].x + Math.Abs(segments[0].x)) +
                                Vector3.right * (segments[i].y * -1f)) * _roadData.unitScale
                };

            splineComputer.SetPoints(points);
            splineComputer.type = _roadData.type;
        }

        private void SetRoadMesh()
        {
            _roadGameObject.GetComponent<MeshRenderer>().sharedMaterial =
                _roadData.roadMesh.GetComponent<MeshRenderer>().sharedMaterial;
            _channel.count = 100;
            _channel.minScale = _roadData.roadScale;
        }
    }
}