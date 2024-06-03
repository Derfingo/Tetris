using System;

namespace Assets.Scripts.Tetris
{
    public class Score : IScore, IReset
    {
        public event Action<int> ChangeCurrentScoreEvent;
        public event Action<int> ChangeLinesScoreEvent;
        public event Action<int> ChangeTopScoreEvent;

        public int Amount { get; private set; }

        private readonly int _factor;
        private int _lines;

        public Score(int factor)
        {
            Amount = 0;
            _lines = 0;
            _factor = factor;
        }

        public void Add(int count)
        {
            int sum = count * _factor;
            Amount += sum;
            ChangeCurrentScoreEvent?.Invoke(Amount);
        }

        public void AddLines(int count)
        {
            _lines += count;
            ChangeLinesScoreEvent?.Invoke(_lines);
        }

        public void SetTop(int count)
        {
            ChangeTopScoreEvent?.Invoke(count);
        }

        public void Reset()
        {
            Amount = 0;
            _lines = 0;

            ChangeCurrentScoreEvent?.Invoke(Amount);
            ChangeLinesScoreEvent?.Invoke(_lines);
        }
    }
}
