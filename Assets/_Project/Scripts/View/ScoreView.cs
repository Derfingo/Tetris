using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class ScoreView : ViewBase, IScoreView
    {
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private TMP_Text _linesText;
        [SerializeField] private TMP_Text _levelText;
        [SerializeField] private TMP_Text _topText;

        public void Initialize()
        {
            _amountText.text = "0";
            _linesText.text = "0";
            _levelText.text = "1";
            _topText.text = "0";
        }

        public void SetCurrent(int count)
        {
            _amountText.text = count.ToString();
        }

        public void SetLevel(int count)
        {
            _levelText.text = count.ToString();
        }

        public void SetLines(int count)
        {
            _linesText.text = count.ToString();
        }

        public void SetTop(int count)
        {
            _topText.text = count.ToString();
        }
    }
}
