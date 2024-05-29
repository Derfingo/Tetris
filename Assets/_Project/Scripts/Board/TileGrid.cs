using System;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Assets.Scripts.Tetris
{
    public class TileGrid : IReset
    {
        private readonly IScore _score;
        private readonly Tilemap _tilemap;

        private readonly int _rows;
        private readonly int _columns;

        public TileGrid(IScore score, Tilemap tilemap)
        {
            _tilemap = tilemap;
            _score = score;
            _columns = 10;
            _rows = 20;
        }

        public void Reset()
        {
            ClearGrid();
        }

        public void SetFigure(Figure figure)
        {
            for (int i = 0; i < figure.Cells.Length; i++)
            {
                Vector3Int tilePosition = figure.Cells[i] + figure.Position;
                _tilemap.SetTile(tilePosition, figure.Data.Tile);
            }
        }

        public void ClearFigure(Figure figure)
        {
            for (int i = 0; i < figure.Cells.Length; i++)
            {
                Vector3Int tilePosition = figure.Cells[i] + figure.Position;
                _tilemap.SetTile(tilePosition, null);
            }
        }

        public void ClearFullLines()
        {
            int cleared = 0;

            for (int row = 0; row < _rows; row++)
            {
                if (IsRowFull(row))
                {
                    ClearRow(row);
                    cleared++;
                }
                else if (cleared > 0)
                {
                    MoveRowDown(row, cleared);
                }
            }

            UpdateScore(cleared);
        }

        public bool IsValidPosition(Figure figure, Vector3Int offset)
        {
            for (int i = 0; i < figure.Cells.Length; i++)
            {
                Vector3Int tilePosition = figure.Cells[i] + offset;

                if (!IsInside(tilePosition.y, tilePosition.x) || _tilemap.HasTile(tilePosition))
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsInside(int row, int column)
        {
            return row >= 0 && row < _rows && column >= 0 && column < _columns;
        }

        private void ClearRow(int row)
        {
            for (int column = 0; column < _columns; column++)
            {
                _tilemap.SetTile(new Vector3Int(column, row, 0), null);
            }
        }

        private bool IsRowFull(int row)
        {
            for (int column = 0; column < _columns; column++)
            {
                if (_tilemap.GetTile(new Vector3Int(column, row, 0)) == null)
                {
                    return false;
                }
            }

            return true;
        }

        private void MoveRowDown(int row, int numCleared)
        {
            for (int column = 0; column < _columns; column++)
            {
                var tile = _tilemap.GetTile(new Vector3Int(column, row, 0));
                _tilemap.SetTile(new Vector3Int(column, row - numCleared, 0), tile);
                _tilemap.SetTile(new Vector3Int(column, row, 0), null);
            }
        }

        private void UpdateScore(int count)
        {
            if (count > 0)
            {
                _score.Add(count);
                _score.AddLines(count);
            }
        }

        private void ClearGrid()
        {
            for (int row = 0; row < _rows; row++)
            {
                for (int column = 0; column < _columns; column++)
                {
                    var tilePosition = new Vector3Int(column, row, 0);

                    if (_tilemap.HasTile(tilePosition))
                    {
                        _tilemap.SetTile(tilePosition, null);
                    }
                }
            }
        }
    }
}
