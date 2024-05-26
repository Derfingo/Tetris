using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class Score : IScore
    {
        public event Action<int> ChangeCurrentScoreEvent;
        public event Action<int> ChangeLinesScoreEvent;
        public event Action<int> ChangeTopScoreEvent;

        public int Amount { get; private set; }

        private readonly int _multiplayer;
        private int _lines;

        public Score()
        {
            Amount = 0;
            _lines = 0;
            _multiplayer = 500;
        }

        public void Add(int count)
        {
            int sum = count * _multiplayer;
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
