using System;

namespace Assets.Scripts.Tetris
{
    public class GameState : IGameState
    {
        private readonly IReset _resetHandler;
        private readonly IInitialization _init;
        private readonly SpawnFigure _spawn;
        private readonly Score _score;
        private SaveData _saveData;
        private ISaveController _saveController;

        public event Action OnReadyToStartEvent;

        public GameState(IInitialization init, SpawnFigure spawn, Score score, IReset resetHandler, ISaveController saveController)
        {
            _resetHandler = resetHandler;
            _saveController = saveController;
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
            SetScore();
        }

        private void GameOver()
        {
            UpdateRecord();
            StartOver();
        }

        private void UpdateRecord()
        {
            if (_score.IsCurrentLarger())
            {
                _saveController.SaveScore(_score.Current);
            }
        }

        private void SetScore()
        {
            _score.SetTop(_saveController.GetData().TopScore);
        }

        private void ReadyToStart()
        {
            _saveController.SetMute();
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

        ~GameState()
        {
            RemoveListeners();
        }
    }
}
