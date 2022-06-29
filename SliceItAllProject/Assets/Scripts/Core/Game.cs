using UI;

namespace Core
{
    public class Game
    {
        private readonly GameMenu gameMenu;
        
        public Game(InputSystem inputSystem)
        {
            GameScene.Instance.Setup(inputSystem);
            
            gameMenu = new GameMenu();
            gameMenu.ShowStartView();

            gameMenu.OnGameStarted += PlayGame;
            gameMenu.OnGameReset += ResetGame;
        }

        private void PlayGame()
        {
            GameScene.Instance.ActivateScene();
        }
        
        private void ResetGame()
        {
            GameScene.Instance.ResetScene();
        }
    }
}
