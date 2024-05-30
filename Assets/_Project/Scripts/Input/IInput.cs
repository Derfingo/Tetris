using System;

namespace Assets.Scripts.Tetris
{
    public interface IInput
    {
        public event Action OnDrop;
        public event Action<bool> OnDropSlow;
        public event Action<int> OnRotate;
        public event Action<int> OnMoveHorizontal;
    }
}