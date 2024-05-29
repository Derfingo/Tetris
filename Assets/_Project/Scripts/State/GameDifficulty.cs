using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class GameDifficulty : IReset
    {
        private readonly IScore _score;
        private readonly IGameLoop _gameLoop;
        private readonly float _initialDecrease = 0.015f;
        private readonly float _endDecrease = 0.005f;
        private int _counter;

        public GameDifficulty(IScore score, IGameLoop gameLoop)
        {
            _gameLoop = gameLoop;
            _score = score;

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

                if (_counter < 15)
                {
                    _gameLoop.ChangeStepDelay(_initialDecrease);
                }
                else if (_counter % 5 == 0)
                {
                    Debug.Log("Difference");
                    _gameLoop.ChangeStepDelay(_endDecrease);
                }
            }
        }
    }
}
