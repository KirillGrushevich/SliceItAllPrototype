using GamePlay;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class FinishBonusesView : MonoBehaviour
    {
        [SerializeField] private Finish[] finishes;
        [SerializeField] private Text scoreLabel;
        
        private void Start()
        {
            SetupLabel(scoreLabel, finishes[0].transform.position.y, finishes[0].ScoreMultiplier);

            for (var i = 1; i < finishes.Length; i++)
            {
                var label = Instantiate(scoreLabel, scoreLabel.transform.parent);
                label.transform.position = scoreLabel.transform.position;
                SetupLabel(label, finishes[i].transform.position.y, finishes[i].ScoreMultiplier);
            }
        }

        private void SetupLabel(Text label, float posY, int scoreMultiplier)
        {
            label.text = $"x{scoreMultiplier}";
            var pos = label.transform.position;
            pos.y = posY;
            label.transform.position = pos;
        }
    }
}