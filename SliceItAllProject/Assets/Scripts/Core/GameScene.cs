using GamePlay;
using UnityEngine;

namespace Core
{
    public class GameScene : MonoBehaviour
    {
        public static GameScene Instance;

        [SerializeField] private Knife knife;

        public void Setup(InputSystem inputSystem)
        {
            knife.Setup(inputSystem);
        }
        
        private void Awake()
        {
            Instance = this;
        }

        
    }
}
