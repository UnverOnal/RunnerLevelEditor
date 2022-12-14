using System;
using UnityEngine;

namespace LevelEditor.Scripts.Placeable
{
    public enum PlaceableType
    {
        Environment,
        Collectable,
        Obstacle,
        Gate
    }

    [Serializable]
    public struct PlaceableData
    {
        public PlaceableType type;

        [SerializeField] public GameObject[] placeables;

        public Vector3 minPositionOffset;
        public Vector3 maxPositionOffset;
        public Vector3 minAngleOffset;
        public Vector3 maxAngleOffset;
        public Vector3 minScaleOffset;
        public Vector3 maxScaleOffset;

        public int count;

        [Range(-2, 2)] public float evaluateOffset;
    }
}