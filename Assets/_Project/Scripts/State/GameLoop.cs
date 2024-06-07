using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameLoop : MonoBehaviour, IPause, IGameLoop, IReset
    {
        private IInput _input;
        private FigureControl _control;
        private SpawnFigure _spawn;
        private TileGrid _grid;

        private float _stepDelay;
        private float _defaultStepDelay;
        private float _timeStep;

        private bool _isPause = true;

        public void Initialize(TileGrid grid, FigureControl control, SpawnFigure spawn, IInput input, float stepDelay)
        {
            _control = control;
            _spawn = spawn;
            _input = input;
            _grid = grid;

            _defaultStepDelay = stepDelay;
            _timeStep = stepDelay;

            AddListeners();
        }

        void IReset.Reset()
        {
            _timeStep = 0;
            _stepDelay = _defaultStepDelay;
        }

        public void ChangeStepDelay(float decrease)
        {
            _stepDelay -= decrease;
        }

        public void Pause(bool isPaused)
        {
            _isPause = isPaused;
        }

        private void Update()
        {
            if (_isPause == false)
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
            _grid.SetFigure(_control.Active);
            _grid.ClearFullLines();
            _spawn.Spawn();
        }

        private void OnAccelerateDrop(bool isDrop)
        {
            if (isDrop)
            {
                Time.timeScale = 4f;
            }
            else
            {
                Time.timeScale = 1f;
            }
        }

        private void AddListeners()
        {
            _control.OnLandEvent += UpdateCycle;
            _input.OnDropSlow += OnAccelerateDrop;
        }

        private void RemoveListeners()
        {
            _control.OnLandEvent -= UpdateCycle;
            _input.OnDropSlow -= OnAccelerateDrop;
        }

        ~GameLoop()
        {
            RemoveListeners();
        }
    }
}
