using System;

namespace Assets.Scripts.Tetris
{
    public class Score : IScore, IReset
    {
        public event Action<int> ChangeCurrentEvent;
        public event Action<int> ChangeLinesEvent;
        public event Action<int> ChangeLevelEvent;
        public event Action<int> ChangeTopEvent;

        public int Current { get; private set; }

        private readonly int _factor;
        private int _level;
        private int _lines;
        private int _top;

        public Score(int factor, int top)
        {
            Current = 0;
            _lines = 0;
            _level = 1;

            _top = top;
            _factor = factor;
        }

        public bool IsCurrentLarger()
        {
            return Current > _top;
        }

        public void Add(int count)
        {
            int sum = count * _factor;
            Current += sum;
            ChangeCurrentEvent?.Invoke(Current);
        }

        public void AddLines(int count)
        {
            _lines += count;
            ChangeLinesEvent?.Invoke(_lines);
        }

        public void AddLevel()
        {
            _level++;
            ChangeLevelEvent?.Invoke(_level);
        }

        public void SetTop(int count)
        {
            ChangeTopEvent?.Invoke(count);
        }

        public void Reset()
        {
            Current = 0;
            _lines = 0;
            _level = 1;

            ChangeCurrentEvent?.Invoke(Current);
            ChangeLinesEvent?.Invoke(_lines);
            ChangeLevelEvent?.Invoke(_level);
        }
    }
}
