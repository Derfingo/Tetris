using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class Figure
    {
        public TetrominoData Data { get; private set; }
        public Vector3Int[] Cells { get; private set; }
        public Vector3Int Position { get; private set; }

        private int _rotationIndex;
        private TileGrid _grid;

        public Figure(TetrominoData data, Vector3Int position, TileGrid grid)
        {
            Data = data;
            Position = position;
            _grid = grid;
            _rotationIndex = 0;
            FillCells();
        }

        public bool Move(Vector3Int direction)
        {
            _grid.ClearFigure(this);
            Vector3Int offset = Position + direction;
            bool isValid = _grid.IsValidPosition(this, offset);

            if (isValid)
            {
                Position = offset;
            }

            _grid.SetFigure(this);
            return isValid;
        }

        public void Rotate(int direction)
        {
            _grid.ClearFigure(this);

            int rotationIndex = _rotationIndex;
            _rotationIndex = Mod(_rotationIndex + direction, 4);
            ApplyRotationMatrix(direction);

            if (!TestWallKicks(_rotationIndex, direction))
            {
                _rotationIndex = rotationIndex;
                ApplyRotationMatrix(-direction);
            }

            _grid.SetFigure(this);
        }

        private bool TestWallKicks(int rotationIndex, int direcion)
        {
            int wallKickIndex = GetWallKickIndex(rotationIndex, direcion);

            for (int i = 0; i < Data.WallKicks.GetLength(1); i++)
            {
                Vector2Int translation = Data.WallKicks[wallKickIndex, i];

                if (Move((Vector3Int)translation))
                {
                    return true;
                }
            }

            return false;
        }

        private int GetWallKickIndex(int rotationIndex, int direcion)
        {
            int wallKickIndex = rotationIndex * 2;

            if (direcion < 0)
            {
                wallKickIndex--;
            }

            return Mod(wallKickIndex, Data.WallKicks.GetLength(0));
        }

        private void ApplyRotationMatrix(int direction)
        {
            float[] matrix = RotationData.RotationMatrix;

            for (int i = 0; i < Cells.Length; i++)
            {
                Vector3 cell = Cells[i];
                int x, y;

                if (Data.Type == FigureType.I || Data.Type == FigureType.O)
                {
                    cell.x -= 0.5f;
                    cell.y -= 0.5f;

                    x = Mathf.CeilToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.CeilToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                }
                else
                {
                    x = Mathf.RoundToInt((cell.x * matrix[0] * direction) + (cell.y * matrix[1] * direction));
                    y = Mathf.RoundToInt((cell.x * matrix[2] * direction) + (cell.y * matrix[3] * direction));
                }

                Cells[i] = new Vector3Int(x, y, 0);
            }
        }

        private int Mod(int x, int m = 4)
        {
            return (x % m + m) % m;
        }

        private void FillCells()
        {
            Cells ??= new Vector3Int[4];

            for (int i = 0; i < Cells.Length; i++)
            {
                Cells[i] = (Vector3Int)Data.Cells[i];
            }
        }
    }
}
