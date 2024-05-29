using System;

namespace Assets.Scripts.Tetris
{
    public class GameState : IGameState
    {
        private readonly ISaveSystem _saveSystem;
        private readonly IReset _resetHandler;
        private readonly IInitialization _init;
        private readonly SpawnFigure _spawn;
        private readonly Score _score;
        private SaveData _saveData;

        public event Action OnReadyToStartEvent;

        public GameState(IInitialization init, ISaveSystem saveSystem, SpawnFigure spawn, Score score, IReset resetHandler)
        {
            _resetHandler = resetHandler;
            _saveSystem = saveSystem;
            _spawn = spawn;
            _score = score;
            _init = init;

            AddListeners();
            _resetHandler = resetHandler;
        }

        public void StartOver()
        {
            _resetHandler.Reset();
            _spawn.Spawn();
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
