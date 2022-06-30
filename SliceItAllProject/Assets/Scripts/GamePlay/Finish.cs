using UnityEngine;

namespace GamePlay
{
    public class Finish : MonoBehaviour
    {
        [SerializeField] private int scoreMultiplier;

        public int ScoreMultiplier => scoreMultiplier;
    }
}