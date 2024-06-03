using System;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class FigureControl
    {
        public event Action OnLandEvent;

        private readonly IInput _input;
        private Figure _activeFigure;
        public Figure Active => _activeFigure;

        public FigureControl(IInput input)
        {
            _input = input;

            AddListeners();
        }

        public void SetActive(Figure figure)
        {
            if (figure != null)
            {
                _activeFigure = figure;
            }
            else
            {
                throw new NullReferenceException("figure is null");
            }
        }

        public void MoveDown()
        {
            if (!_activeFigure.TryMove(Vector3Int.down, true))
            {
                OnLandEvent?.Invoke();
            }
        }

        private void MoveHorizontal(int direction)
        {
            Vector3Int horizontal = new(direction, 0, 0);
            _activeFigure.TryMove(horizontal, true);
        }

        private void Rotate(int direction)
        {
            _activeFigure.Rotate(direction);
        }

        private void Drop()
        {
            while (_activeFigure.TryMove(Vector3Int.down, true))
            {
                continue;
            }

            OnLandEvent?.Invoke();
        }

        private void AddListeners()
        {
            _input.OnMoveHorizontal += MoveHorizontal;
            _input.OnRotate += Rotate;
            _input.OnDrop += Drop;
        }

        private void RemoveListeners()
        {
            _input.OnMoveHorizontal -= MoveHorizontal;
            _input.OnRotate -= Rotate;
            _input.OnDrop -= Drop;
        }

        ~FigureControl()
        {
            RemoveListeners();
        }
    }
}
