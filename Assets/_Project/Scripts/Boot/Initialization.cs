using Assets.Scripts.Tetris;
using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Initialization : MonoBehaviour, IInitialization
{
    public event Action OnInitializedEvent;

    [SerializeField] private Tilemap _tilemap;
    [SerializeField] private SpawnFigure _spawn;
    [SerializeField] private GameLoop _gameLoop;
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
    private KeyboardInput _input;
    private GameState _gameState;
    private FigureControl _control;
    private GamePresenter _presenter;
    private PauseHandler _pauseHandler;
    private BinarySaveSystem _saveSystem;

    private void Awake()
    {
        Compose();

        OnInitializedEvent?.Invoke();
    }

    private void Compose()
    {
        BindSaveSystem();
        BindInput();
        BindControl();
        BindScore();
        BindGrid();
        BindSpawn();
        BindGameLoop();
        BindGameState();
        BindView();
        BindPause();
        BindPresenter();
    }

    private void BindPause()
    {
        _pauseHandler = new PauseHandler();
        _pauseHandler.Register(_gameLoop, _input);
    }

    private void BindView()
    {
        _mainView.Initialize();
        _scoreView.Initialize();
        _pauseView.Initialize();
        _viewTransition.Initialize();
    }

    private void BindPresenter()
    {
        _presenter = new GamePresenter(_settingsView, _pauseView, _scoreView, _figureView, _mainView, _spawn, _score, _pauseHandler, _gameState);
    }

    private void BindGameState()
    {
        _gameState = new GameState(this, _saveSystem, _spawn, _tileGrid, _score);
    }

    private void BindSaveSystem()
    {
        _saveSystem = new BinarySaveSystem();
    }

    private void BindScore()
    {
        _score = new Score();
    }

    private void BindInput()
    {
        _input = new KeyboardInput();
    }

    private void BindControl()
    {
        _control = new FigureControl(_input);
    }

    private void BindSpawn()
    {
        _spawn.Initialize(_control, _tileGrid);
    }

    private void BindGrid()
    {
        _tileGrid = new TileGrid(_score, _tilemap);
    }

    private void BindGameLoop()
    {
        _gameLoop.Initialize(_tileGrid, _control, _spawn);
    }
}
