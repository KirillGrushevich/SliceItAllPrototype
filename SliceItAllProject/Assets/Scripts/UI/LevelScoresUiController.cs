using UnityEngine;

namespace UI
{
    public class LevelScoresUiController : UiController
    {
        public LevelScoresUiController()
        {
            view = LoadView<LevelScoresView>("LevelScoresView");
        }

        private readonly LevelScoresView view;

        public void ShowNewScore(int score, Vector3 position)
        {
            view.ShowScore(score, position);
        }
    }
}