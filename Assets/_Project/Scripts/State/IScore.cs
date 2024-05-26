using System;

namespace Assets.Scripts.Tetris
{
    public interface IScore
    {
        public event Action<int> ChangeCurrentScoreEvent;
        public event Action<int> ChangeLinesScoreEvent;
        public event Action<int> ChangeTopScoreEvent;

        public int Amount { get; }
        void Add(int count);
        void SetTop(int count);
        void AddLines(int count);
    }
}