using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class Score : MonoBehaviour, IScore
    {
        [SerializeField] private TextMeshProUGUI _scoreText;

        private int _score;
        private int _multiplayer;

        public void Initialize()
        {
            _score = 0;
            _multiplayer = 500;
        }

        public void Add(int count)
        {
            int score = count * _multiplayer;
            _score += score;
            _scoreText.text = _score.ToString();
        }

        public void Reset()
        {
            _score = 0;
            _scoreText.text = _score.ToString();
        }
    }
}
