using System;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class SpawnFigure : MonoBehaviour
    {
        public event Action OnGameOver;

        [SerializeField] private Vector3Int _spawnPosition;
        [SerializeField] private TetrominoData[] _data;
        [SerializeField] private Figure _activeFigure;

        private System.Random _random;

        private FigureControl _control;
        private TileGrid _grid;

        public void Initialize(FigureControl control, TileGrid grid)
        {
            _control = control;
            _grid = grid;

            _random = new System.Random();
            InitializeData();
        }

        private void InitializeData()
        {
            for (int i = 0; i < _data.Length; i++)
            {
                _data[i].Initialize();
            }
        }

        private int GetRandomIndex()
        {
            int number = _random.Next(0, int.MaxValue);
            return Math.Abs(number % _data.Length);
        }

        public void Spawn()
        {
            int index = GetRandomIndex();
            TetrominoData data = _data[index];
            _activeFigure.Initialize(data, _spawnPosition);
            _control.ActiveFigure = _activeFigure;

            if (_grid.IsValidPosition(_activeFigure, _spawnPosition))
            {
                _grid.SetFigure(_activeFigure);
            }
            else
            {
                OnGameOver?.Invoke();
            }
        }
    }
}
