using System;
using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class FigureControl
    {
        public event Action OnLand;

        private readonly KeyboardInput _input;

        private Figure _activeFigure;

        public Figure ActiveFigure
        {
            get => _activeFigure;
            set => _activeFigure = value;
        }

        public FigureControl(KeyboardInput input)
        {
            _input = input;

            AddListeners();
        }

        public void MoveDown()
        {
            if (!_activeFigure.Move(Vector3Int.down))
            {
                OnLand?.Invoke();
            }
        }

        private void MoveHorizontal(int direction)
        {
            Vector3Int horizontal = new(direction, 0, 0);
            _activeFigure.Move(horizontal);
        }

        private void Rotate(int direction)
        {
            _activeFigure.Rotate(direction);
        }

        private void Drop()
        {
            while (true)
            {
                if (!_activeFigure.Move(Vector3Int.down))
                {
                    OnLand?.Invoke();
                    return;
                }
            }
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
    }
}
