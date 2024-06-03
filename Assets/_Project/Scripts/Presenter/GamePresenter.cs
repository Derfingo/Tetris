namespace Assets.Scripts.Tetris
{
    public class GamePresenter
    {
        private readonly ISettingsView _settingsView;
        private readonly IPauseView _pauseView;
        private readonly IScoreView _scoreView;
        private readonly IShowView _showView;
        private readonly IMainView _mainView;

        private readonly IGameState _state;
        private readonly ISpawn _spawn;
        private readonly IScore _score;
        private readonly IPause _pause;

        public GamePresenter(ISettingsView settingsView,
                             IPauseView pauseView,
                             IScoreView scoreView,
                             IShowView showView,
                             IMainView mainView,
                             ISpawn spawn,
                             IScore score,
                             IPause pause,
                             IGameState state)
        {
            _settingsView = settingsView;
            _pauseView = pauseView;
            _scoreView = scoreView;
            _showView = showView;
            _mainView = mainView;

            _spawn = spawn;
            _score = score;
            _pause = pause;
            _state = state;

            AddListeners();
        }

        private void AddListeners()
        {
            _pauseView.OnPauseClickEvent += _pause.Pause;
            _spawn.OnShowNextEvent += _showView.Show;

            _score.ChangeCurrentScoreEvent += _scoreView.SetCurrent;
            _score.ChangeLinesScoreEvent += _scoreView.SetLines;
            _score.ChangeTopScoreEvent += _scoreView.SetTop;

            _state.OnReadyToStartEvent += _mainView.ShowButtons;
            _mainView.OnStartClickEvent += _state.StartOver;
            _mainView.OnStartClickEvent += () => _pause.Pause(false);
        }

        private void RemoveListeners()
        {
            _pauseView.OnPauseClickEvent -= _pause.Pause;
            _spawn.OnShowNextEvent -= _showView.Show;

            _score.ChangeCurrentScoreEvent -= _scoreView.SetCurrent;
            _score.ChangeLinesScoreEvent -= _scoreView.SetLines;
            _score.ChangeTopScoreEvent -= _scoreView.SetTop;

            _state.OnReadyToStartEvent -= _mainView.ShowButtons;
            _mainView.OnStartClickEvent -= _state.StartOver;
            _mainView.OnStartClickEvent -= () => _pause.Pause(false);
        }

        ~GamePresenter()
        {
            RemoveListeners();
        }
    }
}
