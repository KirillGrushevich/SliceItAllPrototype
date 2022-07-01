using System.Collections;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class LevelScoresView : MonoBehaviour
    {
        [SerializeField] private Text scoreLabel;
        [SerializeField] private float showingTime;
        [SerializeField] private int poolSize;

        private GameObjectsPool pool;
        
        public void ShowScore(int score, Vector3 position)
        {
            var textLabel = pool.Get<Text>();
            textLabel.text = $"+{score}";
            position.x = textLabel.transform.position.x;
            textLabel.transform.position = position;

            StartCoroutine(ShowScore(textLabel));
        }

        private void Start()
        {
            pool = new GameObjectsPool(scoreLabel, poolSize);
        }

        private IEnumerator ShowScore(Text textLabel)
        {
            var t = showingTime;
            var color = Color.white;
            
            while (t > 0f)
            {
                t -= Time.deltaTime;
                color.a = t / showingTime;
                textLabel.color = color;
                yield return null;
            }
            
            pool.Return(textLabel);
        }
    }
}