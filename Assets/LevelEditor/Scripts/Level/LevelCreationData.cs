using System;
using UnityEngine;

namespace LevelEditor.Scripts.Level
{
    [Serializable]
    public struct LevelCreationData
    {
        public string levelName;
        public string pathToSaveIn;

        public int levelIndex;

        public GameObject levelTemplate;
    }
}