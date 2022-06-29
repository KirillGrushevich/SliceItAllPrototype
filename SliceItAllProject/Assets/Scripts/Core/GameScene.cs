using GamePlay;
using UnityEngine;

namespace Core
{
    public class GameScene : MonoBehaviour
    {
        public static GameScene Instance;

        [SerializeField] private Knife knife;
        [SerializeField] private CameraController cameraController;

        public void Setup(InputSystem inputSystem)
        {
            knife.Setup(inputSystem);
            cameraController.Setup(knife.transform);
        }

        public void ActivateScene()
        {
            knife.Activate();
        }

        public void ResetScene()
        {
            knife.ResetPosition();
            cameraController.ResetPosition();
        }

        public void StopScene()
        {
            knife.Release();
        }
        
        private void Awake()
        {
            Instance = this;
        }

        
    }
}
