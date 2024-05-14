using Assets.Scripts.Tetris;
using System;
using UnityEngine;

public class Initialization : MonoBehaviour
{
    [SerializeField] private SpawnFigure _spawn;
    [SerializeField] private TileGrid _tileGrid;
    [SerializeField] private GameLoop _gameLoop;
    [SerializeField] private Score _score;

    private KeyboardInput _input;
    private FigureControl _control;

    private void Awake()
    {
        Compose();
    }

    private void Compose()
    {
        BindInput();
        BindControl();
        BindGrid();
        BindSpawn();
        BindGameLoop();
        BindScore();
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
        _gameLoop.Initialize(_tileGrid, _control, _spawn, _score);
    }
}
