using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameLoop : MonoBehaviour, IPause
    {
        [SerializeField] private float _stepDelay = 1f;

        private float _timeStep;

        private FigureControl _control;
        private SpawnFigure _spawn;
        private TileGrid _grid;

        private bool _isPause = false;

        public void Initialize(TileGrid grid, FigureControl control, SpawnFigure spawn)
        {
            _control = control;
            _spawn = spawn;
            _grid = grid;

            AddListeners();
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

        private void AddListeners()
        {
            _control.OnLand += UpdateCycle;
        }

        private void RemoveListeners()
        {
            _control.OnLand -= UpdateCycle;
        }
    }
}
