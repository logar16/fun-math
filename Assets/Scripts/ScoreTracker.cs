using TMPro;
using UnityEngine;

namespace FunMath
{
    public class ScoreTracker : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ScoreUI;

        public int Score { get; set; }

        // Start is called before the first frame update
        void Start()
        {
            Score = 0;
        }

        public void AddScore(int score)
        {
            Score += score;
            ScoreUI.text = Score.ToString();
        }
    }
}
