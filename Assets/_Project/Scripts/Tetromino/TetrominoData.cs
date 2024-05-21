using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    [System.Serializable]
    public struct TetrominoData
    {
        public Tile Tile;
        public Image Image;
        public FigureType Type;

        public Vector2Int[] Cells { get; private set; }
        public Vector2Int[,] WallKicks { get; private set; }

        public void Initialize()
        {
            Cells = PositionData.Cells[Type];
            WallKicks = RotationData.WallKicks[Type];
        }
    }

    public enum FigureType
    {
        I, J, L, S, T, Z, O
    }
}
