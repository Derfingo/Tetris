using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameLoop : MonoBehaviour
    {
        [SerializeField] private float _stepDelay = 1f;

        private float _timeStep;

        private FigureControl _control;
        private SpawnFigure _spawn;
        private TileGrid _grid;

        private bool _isPause;

        public void Initialize(TileGrid grid, FigureControl control, SpawnFigure spawn)
        {
            _control = control;
            _spawn = spawn;
            _grid = grid;

            AddListeners();
        }

        private void Start()
        {
            StartOver();
        }

        private void Update()
        {
            if (!_isPause)
            {
                _timeStep += Time.deltaTime;

                if (_timeStep > _stepDelay)
                {
                    _control.MoveDown();
                    _timeStep = 0f;
                }
            }
        }

        private void UpdateCycle()
        {
            _grid.SetFigure(_control.ActiveFigure);
            _grid.ClearFullLines();
            _spawn.Spawn();
        }

        private void StartOver()
        {
            _grid.ClearGrid();
            _spawn.Spawn();
            _timeStep = 0f;
        }

        private void GameOver()
        {
            _grid.ClearGrid();
            _spawn.Spawn();
            _timeStep = 0f;
        }

        private void AddListeners()
        {
            _control.OnLand += UpdateCycle;
            _spawn.OnGameOver += StartOver;
        }

        private void RemoveListeners()
        {
            _control.OnLand -= UpdateCycle;
            _spawn.OnGameOver -= StartOver;
        }
    }
}
