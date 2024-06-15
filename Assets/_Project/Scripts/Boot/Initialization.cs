using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tetris
{
    public class Initialization : MonoBehaviour, IInitialization
    {
        public event Action OnInitializedEvent;

        [SerializeField] private Tilemap _tilemap;
        [SerializeField] private GameLoop _gameLoop;
        [SerializeField] private SoundSystem _soundSystem;
        [SerializeField] private GameConfigProvider _configProvider;
        [Space()]
        [Header("View")]
        [SerializeField] private ViewTransition _viewTransition;
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private FigureView _figureView;
        [SerializeField] private ScoreView _scoreView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private MainView _mainView;

        private Score _score;
        private TileGrid _tileGrid;
        private SpawnFigure _spawn;
        private GameState _gameState;
        private FigureControl _control;
        private AppSettings _appSettings;
        private GamePresenter _presenter;
        private ResetHandler _resetHandler;
        private PauseHandler _pauseHandler;
        private BinarySaveSystem _saveSystem;
        private KeyboardInput _keyboardInput;
        private GameDifficulty _gameDifficulty;
        private SaveController _saveController;

        private GameConfig _config;

        private void Awake()
        {
            _config = _configProvider.Get();
            Compose();

            OnInitializedEvent?.Invoke();
        }

        private void Compose()
        {
            BindAppSettings();
            BindInput();
            BindSaveSystem();
            BindSaveController();
            BindScore();
            BindGrid();
            BindControl();
            BindSpawn();
            BindGameLoop();
            BindDifficulty();
            BindReset();
            BindPause();
            BindGameState();
            BindView();
            BindPresenter();
            BindSoundSystem();
        }

        private void BindSaveController()
        {
            _saveController = new SaveController(_saveSystem);
        }

        private void BindAppSettings()
        {
            _appSettings = new AppSettings();
        }

        private void BindSoundSystem()
        {
            _soundSystem.Initialize(_score, _saveController);
        }

        private void BindReset()
        {
            _resetHandler = new ResetHandler();
            _resetHandler.Register(_score, _gameDifficulty, _gameLoop, _tileGrid);
        }

        private void BindDifficulty()
        {
            _gameDifficulty = new GameDifficulty(_score, _gameLoop, _config.InitialDecrease, _config.EndDecrease);
        }

        private void BindPause()
        {
            _pauseHandler = new PauseHandler();
            _pauseHandler.Register(_gameLoop, _keyboardInput);
        }

        private void BindView()
        {
            _mainView.Initialize();
            _scoreView.Initialize();
            _pauseView.Initialize();
            _settingsView.Initialize();
            _viewTransition.Initialize();
        }

        private void BindPresenter()
        {
            _presenter = new GamePresenter(_settingsView,
                                           _pauseView, 
                                           _scoreView, 
                                           _figureView, 
                                           _mainView, 
                                           _spawn, 
                                           _score, 
                                           _pauseHandler, 
                                           _soundSystem, 
                                           _gameState,
                                           _saveController);
        }

        private void BindGameState()
        {
            _gameState = new GameState(this, _spawn, _score, _resetHandler, _saveController);
        }

        private void BindSaveSystem()
        {
            _saveSystem = new BinarySaveSystem();
        }

        private void BindScore()
        {
            _score = new Score(_config.ScoreFactor, _saveController.GetData().TopScore);
        }

        private void BindInput()
        {
            _keyboardInput = new KeyboardInput();
        }

        private void BindControl()
        {
            _control = new FigureControl(_keyboardInput);
        }

        private void BindSpawn()
        {
            _spawn = new SpawnFigure(_control, _tileGrid, _config.SpawnPosition, _config.Figures);
        }

        private void BindGrid()
        {
            _tileGrid = new TileGrid(_score, _tilemap, _config.Columns, _config.Rows);
        }

        private void BindGameLoop()
        {
            _gameLoop.Initialize(_tileGrid, _control, _spawn, _keyboardInput, _config.StepDelay);
        }
    }
}