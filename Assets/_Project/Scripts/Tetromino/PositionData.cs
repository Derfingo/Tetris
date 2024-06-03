using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public static class PositionData
    {
        public static readonly Dictionary<FigureType, Vector2Int[]> Cells = new()
        {
            { FigureType.I, new Vector2Int[] { new (-1, 1), new ( 0, 1), new ( 1, 1), new ( 2, 1) } },
            { FigureType.J, new Vector2Int[] { new ( 1,-1), new (-1, 0), new ( 0, 0), new ( 1, 0) } },
            { FigureType.L, new Vector2Int[] { new (-1,-1), new (-1, 0), new ( 0, 0), new ( 1, 0) } },
            { FigureType.O, new Vector2Int[] { new ( 0, 1), new ( 1, 1), new ( 0, 0), new ( 1, 0) } },
            { FigureType.S, new Vector2Int[] { new ( 0, 1), new ( 1, 1), new (-1, 0), new ( 0, 0) } },
            { FigureType.T, new Vector2Int[] { new ( 0,-1), new (-1, 0), new ( 0, 0), new ( 1, 0) } },
            { FigureType.Z, new Vector2Int[] { new (-1, 1), new ( 0, 1), new ( 0, 0), new ( 1, 0) } },
        };
    }
}
