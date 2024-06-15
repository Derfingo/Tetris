namespace Assets.Scripts.Tetris
{
    public class GamePresenter
    {
        private readonly ISettingsView _settingsView;
        private readonly IPauseView _pauseView;
        private readonly IScoreView _scoreView;
        private readonly IShowView _showView;
        private readonly IMainView _mainView;

        private readonly ISaveController _saveController;
        private readonly IGameState _state;
        private readonly ISpawn _spawn;
        private readonly IScore _score;
        private readonly IPause _pause;
        private readonly ISound _sound;

        public GamePresenter(ISettingsView settingsView,
                             IPauseView pauseView,
                             IScoreView scoreView,
                             IShowView showView,
                             IMainView mainView,
                             ISpawn spawn,
                             IScore score,
                             IPause pause,
                             ISound sound,
                             IGameState state,
                             ISaveController saveController)
        {
            _settingsView = settingsView;
            _pauseView = pauseView;
            _scoreView = scoreView;
            _showView = showView;
            _mainView = mainView;

            _saveController = saveController;
            _spawn = spawn;
            _score = score;
            _pause = pause;
            _state = state;
            _sound = sound;

            AddListeners();
        }

        private void AddListeners()
        {
            _pauseView.OnPauseClickEvent += _pause.Pause;
            _spawn.OnShowNextEvent += _showView.Show;

            _score.ChangeCurrentEvent += _scoreView.SetCurrent;
            _score.ChangeLinesEvent += _scoreView.SetLines;
            _score.ChangeLevelEvent += _scoreView.SetLevel;
            _score.ChangeTopEvent += _scoreView.SetTop;

            _state.OnReadyToStartEvent += _mainView.ShowButtons;
            _mainView.OnStartClickEvent += _state.StartOver;
            _mainView.OnStartClickEvent += () => _pause.Pause(false);
            _mainView.OnStartClickEvent += _sound.PlayMainTheme;

            _settingsView.OnChangeEffectEvent += _saveController.SetEffectMute;
            _settingsView.OnChangeMusicEvent += _saveController.SetMusicMute;
            _settingsView.OnShowFPSCounterEvent += _saveController.SetViewFPSCounter;

            _saveController.OnChangeEffectEvent += _settingsView.SetMuteEffect;
            _saveController.OnChangeMusicEvent += _settingsView.SetMuteMusic;
            _saveController.OnShowFPSCounterEvent += _settingsView.SetViewFPSCounter;
        }

        private void RemoveListeners()
        {
            _pauseView.OnPauseClickEvent -= _pause.Pause;
            _spawn.OnShowNextEvent -= _showView.Show;

            _score.ChangeCurrentEvent -= _scoreView.SetCurrent;
            _score.ChangeLinesEvent -= _scoreView.SetLines;
            _score.ChangeTopEvent -= _scoreView.SetTop;

            _state.OnReadyToStartEvent -= _mainView.ShowButtons;
            _mainView.OnStartClickEvent -= _state.StartOver;
            _mainView.OnStartClickEvent -= () => _pause.Pause(false);

            _settingsView.OnChangeEffectEvent -= _saveController.SetEffectMute;
            _settingsView.OnChangeMusicEvent -= _saveController.SetMusicMute;
            _settingsView.OnShowFPSCounterEvent -= _saveController.SetViewFPSCounter;

            _saveController.OnChangeEffectEvent -= _settingsView.SetMuteEffect;
            _saveController.OnChangeMusicEvent -= _settingsView.SetMuteMusic;
            _saveController.OnShowFPSCounterEvent -= _settingsView.SetViewFPSCounter;
        }

        ~GamePresenter()
        {
            RemoveListeners();
        }
    }
}
