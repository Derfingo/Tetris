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
        private Score _score;
        private ISaveSystem _saveSystem;
        private SaveData _saveData;

        private bool _isPause;

        public void Initialize(TileGrid grid, FigureControl control, SpawnFigure spawn, Score score, ISaveSystem save)
        {
            _control = control;
            _score = score;
            _spawn = spawn;
            _grid = grid;
            _saveSystem = save;

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
            _timeStep = 1f;
            LoadScore();
            _score.Reset();
        }

        private void GameOver()
        {
            _timeStep = 0f;
            SaveScore();
            StartOver();
        }

        private void SaveScore()
        {
            if (_score.Amount > _saveData.Score)
            {
                var data = new SaveData
                {
                    Score = _score.Amount
                };
                _saveSystem.Save(data);
            }
        }

        private void LoadScore()
        {
            _saveData = _saveSystem.Load();
            _score.SetTop(_saveData.Score);
        }

        private void AddListeners()
        {
            _control.OnLand += UpdateCycle;
            _spawn.OnGameOver += GameOver;
        }

        private void RemoveListeners()
        {
            _control.OnLand -= UpdateCycle;
            _spawn.OnGameOver -= GameOver;
        }
    }
}
