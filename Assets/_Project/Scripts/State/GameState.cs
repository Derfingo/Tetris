using System;

namespace Assets.Scripts.Tetris
{
    public class GameState : IGameState
    {
        private readonly ISaveSystem _saveSystem;
        private readonly IInitialization _init;
        private readonly SpawnFigure _spawn;
        private readonly TileGrid _grid;
        private readonly Score _score;
        private SaveData _saveData;

        public event Action OnReadyToStartEvent;

        public GameState(IInitialization init, ISaveSystem saveSystem, SpawnFigure spawn, TileGrid grid, Score score)
        {
            _init = init;
            _saveSystem = saveSystem;
            _spawn = spawn;
            _grid = grid;
            _score = score;

            AddListeners();
        }

        public void StartOver()
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

        private void ReadyToStart()
        {
            OnReadyToStartEvent?.Invoke();
        }

        private void AddListeners()
        {
            _spawn.OnGameOverEvent += GameOver;
            _init.OnInitializedEvent += ReadyToStart;
        }

        private void RemoveListeners()
        {
            _spawn.OnGameOverEvent -= GameOver;
            _init.OnInitializedEvent -= ReadyToStart;
        }
    }
}
