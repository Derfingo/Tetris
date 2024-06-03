using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameDifficulty : IReset
    {
        private readonly IScore _score;
        private readonly IGameLoop _gameLoop;
        private readonly float _initialDecrease;
        private readonly float _endDecrease;
        private const int INITIAL_LINES = 15;
        private const int END_STEP_LINES = 5;
        private int _counter;

        public GameDifficulty(IScore score, IGameLoop gameLoop, float initialDecrease, float endDecrease)
        {
            _gameLoop = gameLoop;
            _score = score;

            _initialDecrease = initialDecrease;
            _endDecrease = endDecrease;

            _score.ChangeLinesScoreEvent += OnDecreaseDelay;
        }

        public void Reset()
        {
            _counter = 0;
        }

        private void OnDecreaseDelay(int lines)
        {
            if(lines > 0)
            {
                _counter++;

                if (_counter < INITIAL_LINES)
                {
                    _gameLoop.ChangeStepDelay(_initialDecrease);
                }
                else if (_counter % END_STEP_LINES == 0)
                {
                    _gameLoop.ChangeStepDelay(_endDecrease);
                }
            }
        }

        ~GameDifficulty()
        {
            _score.ChangeLinesScoreEvent -= OnDecreaseDelay;
        }
    }
}
