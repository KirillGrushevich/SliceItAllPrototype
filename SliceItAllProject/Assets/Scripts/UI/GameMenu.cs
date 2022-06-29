using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace UI
{
    public class GameMenu
    {
        public GameMenu()
        {
            view = Object.Instantiate(Resources.Load<GameMenuView>("GameMenuView"));
            Object.DontDestroyOnLoad(view);
            
            view.OnPlayButtonPressed += ShowGameView;
            view.OnReplayButtonPressed += ShowStartView;
            view.OnPlayNextLevelButtonPressed += PlayNextLevel;
            view.OnReplayCurrentGameButtonPressed += ReplayLevel;
        }

        public event Action OnGameStarted;
        public event Action OnGameReset;
        
        private readonly GameMenuView view;

        public void ShowStartView()
        {
            view.ShowStartView();
            OnGameReset?.Invoke();
        }
        
        private void ShowGameView()
        {
            view.ShowGameView();
            OnGameStarted?.Invoke();
        }

        private void ShowFinalScreen(bool isVictory)
        {
            view.ShowFinalScreen(isVictory);
        }

        private void ReplayLevel()
        {
            
        }

        private void PlayNextLevel()
        {
            
        }
    }
}