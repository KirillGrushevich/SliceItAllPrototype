using UI;

namespace Core
{
    public class Game
    {
        private readonly GameMenu gameMenu;

        private int scorePoints;
        
        public Game(InputSystem inputSystem)
        {
            GameScene.Instance.Setup(inputSystem, EndGame, ReceiveScorePoints);
            
            gameMenu = new GameMenu();
            gameMenu.ShowStartView();

            gameMenu.OnGameStarted += PlayGame;
            gameMenu.OnGameReset += ResetGame;
        }

        private void PlayGame()
        {
            GameScene.Instance.ActivateScene();
            scorePoints = 0;
        }
        
        private void ResetGame()
        {
            GameScene.Instance.ResetScene();
            gameMenu.ShowStartView();
        }

        private void EndGame(bool isWon, int scoreMultiplier)
        {
            gameMenu.ShowFinalScreen(isWon, scorePoints * scoreMultiplier);
        }
        
        private void ReceiveScorePoints(int points)
        {
            scorePoints += points;
        }
        
    }
}
