using UnityEngine;

namespace LevelEditor.Scripts.Level
{
    public class Level : MonoBehaviour
    {
        [HideInInspector] public Material skybox;
        
        void Start()
        {
            if(skybox)
                RenderSettings.skybox = skybox;
        }
        
    }
}
