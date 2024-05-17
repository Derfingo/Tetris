using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class Score : MonoBehaviour, IScore
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private TextMeshProUGUI _topText;

        public int Amount { get; private set; }

        private int _multiplayer;

        public void Initialize()
        {
            Amount = 0;
            _multiplayer = 500;
        }

        public void Add(int count)
        {
            int amount = count * _multiplayer;
            Amount += amount;
            _amountText.text = Amount.ToString();
        }

        public void Reset()
        {
            Amount = 0;
            _amountText.text = Amount.ToString();
        }

        public void SetTop(int count)
        {
            _topText.text = count.ToString();
        }
    }
}
