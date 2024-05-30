using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameLoop : MonoBehaviour, IPause, IGameLoop, IReset
    {
        [SerializeField] private float _stepDelay;

        private IInput _input;
        private FigureControl _control;
        private SpawnFigure _spawn;
        private TileGrid _grid;

        private float _timeStep;
        private bool _isPause = true;

        public void Initialize(TileGrid grid, FigureControl control, SpawnFigure spawn, IInput input)
        {
            _control = control;
            _spawn = spawn;
            _input = input;
            _grid = grid;

            AddListeners();
        }

        void IReset.Reset()
        {
            _timeStep = 0;
            _stepDelay = 0.5f;
        }

        public void ChangeStepDelay(float decrease)
        {
            _stepDelay -= decrease;
            Debug.Log(_stepDelay);
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
            _grid.SetFigure(_control.ActiveFigure);
            _grid.ClearFullLines();
            _spawn.Spawn();
        }

        private void ChangeStepDelay(bool isDrop)
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
            _control.OnLand += UpdateCycle;
            _input.OnDropSlow += ChangeStepDelay;
        }

        private void RemoveListeners()
        {
            _control.OnLand -= UpdateCycle;
            _input.OnDropSlow -= ChangeStepDelay;
        }
    }
}
