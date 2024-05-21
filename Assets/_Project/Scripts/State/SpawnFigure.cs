using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = System.Random;

namespace Assets.Scripts.Tetris
{
    public class SpawnFigure : MonoBehaviour, ISpawn
    {
        [SerializeField] private Vector3Int _spawnPosition;
        [SerializeField] private TetrominoData[] _data;

        public event Action OnGameOverEvent;
        public event Action<Image> OnShowNextEvent;

        private FigureControl _control;
        private TileGrid _grid;
        private Random _random;

        private Queue<Figure> _figures;

        public void Initialize(FigureControl control, TileGrid grid)
        {
            _control = control;
            _grid = grid;

            _random = new Random();
            _figures = new Queue<Figure>();
            InitializeData();
            FillQueue();
        }

        public void Spawn()
        {
            if (_figures.Count < 2)
            {
                FillQueue();
            }

            Figure current = _figures.Dequeue();
            OnShowNextEvent?.Invoke(_figures.Peek().Data.Image);
            _control.ActiveFigure = current;

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
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i].Initialize();
            }
        }

        private void FillQueue()
        {
            for (int i = 0; i < 20; i++)
            {
                _figures.Enqueue(Prepare());
            }
        }

        private int GetRandomIndex()
        {
            int number = _random.Next(0, int.MaxValue);
            return Math.Abs(number % _data.Length);
        }

        private Figure Prepare()
        {
            int index = GetRandomIndex();
            TetrominoData data = _data[index];
            return new Figure(data, _spawnPosition, _grid);
        }
    }
}
