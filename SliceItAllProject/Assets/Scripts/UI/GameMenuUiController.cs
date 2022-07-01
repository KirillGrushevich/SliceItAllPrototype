using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    public class GameMenuUiController : UiController
    {
        public GameMenuUiController()
        {
            view = LoadView<GameMenuView>("GameMenuView");
            
            view.OnPlayButtonPressed += ShowGameView;
            view.OnReplayButtonPressed += ReplayLevel;
            view.OnPlayNextLevelButtonPressed += PlayNextLevel;
            view.OnReplayCurrentGameButtonPressed += ReplayLevel;
        }

        public event Action OnGameStarted;
        public event Action OnGameReset;

        private readonly GameMenuView view;

        public void ShowStartView()
        {
            view.ShowStartView();
        }
        
        public void ShowFinalScreen(bool isVictory, int score)
        {
            view.ShowFinalScreen(isVictory, score);
        }
        
        private void ShowGameView()
        {
            view.ShowGameView();
            OnGameStarted?.Invoke();
        }

        private void ReplayLevel()
        {
            OnGameReset?.Invoke();
        }

        private void PlayNextLevel()
        {
            OnGameReset?.Invoke();
        }
    }
}