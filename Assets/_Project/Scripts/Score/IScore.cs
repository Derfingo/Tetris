using System;

namespace Assets.Scripts.Tetris
{
    public interface IScore
    {
        public event Action<int> ChangeCurrentEvent;
        public event Action<int> ChangeLinesEvent;
        public event Action<int> ChangeLevelEvent;
        public event Action<int> ChangeTopEvent;

        public int Current { get; }
        void Add(int count);
        void SetTop(int count);
        void AddLines(int count);
        void AddLevel();
    }
}