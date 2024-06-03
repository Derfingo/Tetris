using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts.Tetris
{
    public class SpawnFigure : ISpawn
    {
        public event Action OnGameOverEvent;
        public event Action<Image> OnShowNextEvent;

        private readonly FigureControl _control;
        private readonly TileGrid _grid;
        private readonly Random _random;

        private Vector3Int _spawnPosition;
        private readonly TetrominoData[] _figures;

        private readonly Queue<Figure> _queueFigures;
        private const sbyte MIN_FIGURES = 2;
        private const sbyte MAX_FIGURES = 20;

        public SpawnFigure(FigureControl control, TileGrid grid, Vector3Int spawnPosition, TetrominoData[] figures)
        {
            _control = control;
            _grid = grid;

            _figures = figures;
            _spawnPosition = spawnPosition;

            _random = new Random();
            _queueFigures = new Queue<Figure>();
            InitializeData();
            FillQueue();
        }

        public void Spawn()
        {
            if (_queueFigures.Count < MIN_FIGURES)
            {
                FillQueue();
            }

            Figure current = _queueFigures.Dequeue();
            OnShowNextEvent?.Invoke(_queueFigures.Peek().Data.Image);
            _control.SetActive(current);

            if (_grid.IsValidPosition(current, _spawnPosition))
            {
                _grid.SetFigure(current);
            }
            else
            {
                OnGameOverEvent?.Invoke();
            }
        }

        private void InitializeData()
        {
            for (int i = 0; i < _figures.Length; i++)
            {
                _figures[i].Initialize();
            }
        }

        private void FillQueue()
        {
            for (int i = 0; i < MAX_FIGURES; i++)
            {
                _queueFigures.Enqueue(Prepare());
            }
        }

        private int GetRandomIndex()
        {
            int number = _random.Next(0, int.MaxValue);
            return Math.Abs(number % _figures.Length);
        }

        private Figure Prepare()
        {
            int index = GetRandomIndex();
            TetrominoData data = _figures[index];
            return new Figure(data, _spawnPosition, _grid);
        }
    }
}
