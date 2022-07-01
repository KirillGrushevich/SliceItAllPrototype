using System;
using GamePlay;
using UnityEngine;

namespace Core
{
    public class GameScene : MonoBehaviour
    {
        public static GameScene Instance;

        [SerializeField] private Knife knife;
        [SerializeField] private CameraController cameraController;
        [SerializeField] private GameObject cutObjects;
        [SerializeField] private Material[] cutEdgeMaterials;

        private GameObject cutObjectsCopy;

        public void Setup(InputSystem inputSystem, Action<bool, int> endGameCallback, Action<int, Vector3> scorePointsCallback)
        {
            knife.Setup(inputSystem, cutEdgeMaterials);
            knife.OnFellUnderground += delegate
            {
                endGameCallback?.Invoke(false, 0);
            };
            
            knife.OnHitFinish += delegate(int score)
            {
                endGameCallback?.Invoke(true, score);
            };
            
            knife.OnReceiveScorePoints += delegate(int points, Vector3 pos)
            {
                scorePointsCallback?.Invoke(points, pos);
            };
            
            cameraController.Setup(knife.transform);

            cutObjectsCopy = Instantiate(cutObjects);
            cutObjects.SetActive(false);
        }

        public void ActivateScene()
        {
            knife.Activate();
        }

        public void ResetScene()
        {
            knife.ResetPosition();
            cameraController.ResetPosition();
            
            Destroy(cutObjectsCopy);
            cutObjectsCopy = Instantiate(cutObjects);
            cutObjectsCopy.SetActive(true);
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
