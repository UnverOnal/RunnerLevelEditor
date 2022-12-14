using System.Collections.Generic;
using System.Linq;
using Dreamteck.Splines;
using UnityEngine;

namespace LevelEditor.Scripts.Placeable
{
    public class Placement
    {
        private readonly SplineComputer _splineComputer;
        private readonly Transform _parent;
        private List<ObjectController> _objectControllers;
        private PlaceableData[] _placeableData;

        private readonly GameObject _placeables;

        public Placement(SplineComputer splineComputer, Transform parent)
        {
            _splineComputer = splineComputer;
            _parent = parent;

            _placeables = new GameObject("Placeables");
            _placeables.transform.SetParent(parent);
        }

        public GameObject Create(PlaceableData[] placeableData)
        {
            _placeableData = placeableData;

            if (!(_objectControllers == null || _objectControllers.Count <= _placeableData.Length))
                RemoveRedundantPlaceables();

            CreateObjectControllers();
            Set();
            foreach (var objectController in _objectControllers)
                objectController.Spawn();

            return _placeables;
        }

        public void Bake()
        {
            for (var i = 0; i < _objectControllers.Count; i++)
                Object.DestroyImmediate(_objectControllers[^(i + 1)]);
        }

        private void Set()
        {
            for (var i = 0; i < _placeableData.Length; i++)
            {
                var data = _placeableData[i];
                
                _objectControllers[i].spawnCount = 0;
                _objectControllers[i].objects = data.placeables;
                
                _objectControllers[i].spawnCount = data.count;

                _objectControllers[i].minOffset = data.minPositionOffset;
                _objectControllers[i].maxOffset = data.maxPositionOffset;

                _objectControllers[i].minRotation = data.minAngleOffset;
                _objectControllers[i].maxRotation = data.maxAngleOffset;

                _objectControllers[i].minScaleMultiplier = data.minScaleOffset;
                _objectControllers[i].maxScaleMultiplier = data.maxScaleOffset;

                _objectControllers[i].evaluateOffset = data.evaluateOffset;
            }
        }

        private void CreateObjectControllers()
        {
            var index = 0;
            if (_objectControllers == null || _objectControllers.Count != _placeableData.Length)
            {
                var previous = _objectControllers;
                _objectControllers = new List<ObjectController>();
                if (previous != null)
                {
                    _objectControllers.AddRange(previous);
                    index = previous.Count;
                }
            }

            for (var i = index; i < _placeableData.Length; i++)
            {
                var objectController = _objectControllers.Count > 0 && _objectControllers.Count > i
                    ? _objectControllers[i]
                    : null;
                if (!objectController)
                {
                    var placeable = new GameObject(_placeableData[i].type.ToString());
                    placeable.transform.SetParent(_placeables.transform);
                    objectController = placeable.AddComponent<ObjectController>();
                    _objectControllers.Add(objectController);
                }

                objectController.spline = _splineComputer;
                objectController.applyRotation = true;
                objectController.applyScale = true;
                objectController.objectPositioning = ObjectController.Positioning.Stretch;
            }
        }

        private void RemoveRedundantPlaceables()
        {
            var countToRemove = _objectControllers.Count - _placeableData.Length;
            for (var i = 0; i < countToRemove; i++)
            {
                var controller = _objectControllers[^1];
                controller.spawnCount = 0;
                controller.Spawn();

                _objectControllers.RemoveAt(_objectControllers.Count - 1);
                Object.DestroyImmediate(controller.gameObject);
            }
        }
    }
}