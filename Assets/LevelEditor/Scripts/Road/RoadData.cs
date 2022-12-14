using System;
using Dreamteck.Splines;
using UnityEngine;

namespace LevelEditor.Scripts.Road
{
    [Serializable]
    public struct RoadData
    {
        public Spline.Type type;

        public GameObject pathCreatorGameObject;
        public GameObject roadMesh;

        public float unitScale;

        public Vector2 roadScale;
    }
}