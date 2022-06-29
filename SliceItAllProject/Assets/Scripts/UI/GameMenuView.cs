using System;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameMenuView : MonoBehaviour
    {
        [SerializeField] private Button playButton;
        [SerializeField] private Button replayButton;
        
        [SerializeField] private GameObject resultsPanel;
        [SerializeField] private Button replayCurrentButton;
        [SerializeField] private Button playNextButton;

        public event Action OnPlayButtonPressed;
        public event Action OnReplayButtonPressed;
        public event Action OnReplayCurrentGameButtonPressed;
        public event Action OnPlayNextLevelButtonPressed;

        public void ShowStartView()
        {
            playButton.gameObject.SetActive(true);
            replayButton.gameObject.SetActive(false);
            resultsPanel.SetActive(false);
        }

        public void ShowGameView()
        {
            playButton.gameObject.SetActive(false);
            replayButton.gameObject.SetActive(true);
            resultsPanel.SetActive(false);
        }

        public void ShowFinalScreen(bool isVictory)
        {
            playButton.gameObject.SetActive(false);
            replayButton.gameObject.SetActive(false);
            resultsPanel.SetActive(true);
            
            replayCurrentButton.gameObject.SetActive(!isVictory);
            playNextButton.gameObject.SetActive(isVictory);
        }
        
        private void Start()
        {
            playButton.onClick.AddListener(delegate { OnPlayButtonPressed?.Invoke(); });
            replayButton.onClick.AddListener(delegate { OnReplayButtonPressed?.Invoke(); });
            replayCurrentButton.onClick.AddListener(delegate { OnReplayCurrentGameButtonPressed?.Invoke(); });
            playNextButton.onClick.AddListener(delegate { OnPlayNextLevelButtonPressed?.Invoke(); });
        }
    }
}
