using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public static class Meta
    {
        private static Game game;
        
        [RuntimeInitializeOnLoadMethod]
        public static async void GameInitialization()
        {
            while (GameScene.Instance == null)
            {
                await Task.Yield();
            }

            var obj = new GameObject("GameSystems");
            Object.DontDestroyOnLoad(obj);

            var inputSystem = obj.AddComponent<InputSystem>();
            
            game = new Game();
            GameScene.Instance.Setup(inputSystem);
        }
    }
}