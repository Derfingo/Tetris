using System;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    [CreateAssetMenu(menuName = "My Assets/Game Config Provider")]
    public class GameConfigProvider : ScriptableObject
    {
        [Header("Board")]
        [SerializeField, Range(0, 20)] private int _columns;
        [SerializeField, Range(0, 30)] private int _rows;
        [Header("Difficulty")]
        [SerializeField, Range(0.010f, 0.020f)] private float _initialDecrease;
        [SerializeField, Range(0.010f, 0.002f)] private float _endDecrease;
        [Header("Loop")]
        [SerializeField, Range(1f, 0.4f)] private float _stepDelay;
        [SerializeField, Range(2f, 8f)] private float _accelerationDrop;
        [Header("Score")]
        [SerializeField, Range(200, 5000)] private int _scoreFactor;
        [Header("Spawn")]
        [SerializeField] Transform _spawnPoint;
        [SerializeField] private TetrominoData[] _figures;

        public GameConfig Get()
        {
            var config = new GameConfig()
            {
                Columns = _columns,
                Rows = _rows,
                InitialDecrease = _initialDecrease,
                EndDecrease = _endDecrease,
                StepDelay = _stepDelay,
                AccelerationDrop = _accelerationDrop,
                ScoreFactor = _scoreFactor,
                SpawnPosition = Vector3Int.RoundToInt(_spawnPoint.position),
                Figures = _figures
            };

            return config;
        }

        public GameConfig GetDefault()
        {
            var config = new GameConfig()
            {
                Columns = 10,
                Rows = 20,
                InitialDecrease = 0.015f,
                EndDecrease = 0.005f,
                StepDelay = 0.5f,
                AccelerationDrop = 4f,
                ScoreFactor = 500,
                SpawnPosition = new Vector3Int(4, 18, 0),
                Figures = _figures
            };

            return config;
        }
    }

    public struct GameConfig
    {
        public int Columns;
        public int Rows;
        public float InitialDecrease;
        public float EndDecrease;
        public float StepDelay;
        public float AccelerationDrop;
        public int ScoreFactor;
        public Vector3Int SpawnPosition;
        public TetrominoData[] Figures;
    }
}
