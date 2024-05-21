using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameState : MonoBehaviour
    {
        private IInitialization _init;
        private ISaveSystem _saveSystem;
        private SpawnFigure _spawn;
        private TileGrid _grid;
        private Score _score;
        private SaveData _saveData;

        public void Initialize(TileGrid grid, SpawnFigure spawn, Score score, ISaveSystem save, IInitialization init)
        {
            _score = score;
            _spawn = spawn;
            _grid = grid;
            _saveSystem = save;
            _init = init;

            AddListeners();
        }

        private void StartOver()
        {
            _grid.ClearGrid();
            _spawn.Spawn();
            _score.Reset();
            LoadScore();
        }

        private void GameOver()
        {
            SaveScore();
            StartOver();
        }

        private void SaveScore()
        {
            if (_score.Amount > _saveData.Score)
            {
                var data = new SaveData
                {
                    Score = _score.Amount
                };
                _saveSystem.Save(data);
            }
        }

        private void LoadScore()
        {
            _saveData = _saveSystem.Load();
            _score.SetTop(_saveData.Score);
        }

        private void AddListeners()
        {
            _spawn.OnGameOverEvent += GameOver;
            _init.OnInitializedEvent += StartOver;
        }

        private void RemoveListeners()
        {
            _spawn.OnGameOverEvent -= GameOver;
            _init.OnInitializedEvent -= StartOver;
        }
    }
}
