using UI;
using UnityEngine;

namespace Core
{
    public class Game
    {
        private readonly GameMenuUiController gameMenuUiController;
        private readonly LevelScoresUiController scoresUiController;

        private int scorePoints;
        
        public Game(InputSystem inputSystem)
        {
            GameScene.Instance.Setup(inputSystem, EndGame, ReceiveScorePoints);
            
            gameMenuUiController = new GameMenuUiController();
            gameMenuUiController.ShowStartView();

            gameMenuUiController.OnGameStarted += PlayGame;
            gameMenuUiController.OnGameReset += ResetGame;


            scoresUiController = new LevelScoresUiController();
        }

        private void PlayGame()
        {
            GameScene.Instance.ActivateScene();
            scorePoints = 0;
        }
        
        private void ResetGame()
        {
            GameScene.Instance.ResetScene();
            gameMenuUiController.ShowStartView();
        }

        private void EndGame(bool isWon, int scoreMultiplier)
        {
            gameMenuUiController.ShowFinalScreen(isWon, scorePoints * scoreMultiplier);
            GameScene.Instance.StopScene();
        }
        
        private void ReceiveScorePoints(int points, Vector3 position)
        {
            scorePoints += points;
            scoresUiController.ShowNewScore(points, position);
        }
        
    }
}
