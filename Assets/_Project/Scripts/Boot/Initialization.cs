using Assets.Scripts.Tetris;
using System;
using UnityEngine;

public class Initialization : MonoBehaviour, IInitialization
{
    public event Action OnInitializedEvent;

    [SerializeField] private SpawnFigure _spawn;
    [SerializeField] private TileGrid _tileGrid;
    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private GameState _gameState;
    [SerializeField] private GamePresenter _presenter;
    [SerializeField] private ViewController _view;
    [SerializeField] private Score _score;

    private KeyboardInput _input;
    private FigureControl _control;
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
        BindGrid();
        BindSpawn();
        BindGameLoop();
        BindScore();
        BindGameState();
        BindView();
        BindPause();
        BindPresenter();
    }

    private void BindPause()
    {
        _pauseHandler = new PauseHandler();
        _pauseHandler.Register(_gameLoop);
        _pauseHandler.Register(_input);
    }

    private void BindView()
    {
        _view.Initialize();
    }

    private void BindPresenter()
    {
        _presenter.Initialize(_view, _pauseHandler);
    }

    private void BindGameState()
    {
        _gameState.Initialize(_tileGrid, _spawn, _score, _saveSystem, this);
    }

    private void BindSaveSystem()
    {
        _saveSystem = new BinarySaveSystem();
    }

    private void BindScore()
    {
        _score.Initialize();
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
        _tileGrid.Initialize(_score);
    }

    private void BindGameLoop()
    {
        _gameLoop.Initialize(_tileGrid, _control, _spawn);
    }
}
