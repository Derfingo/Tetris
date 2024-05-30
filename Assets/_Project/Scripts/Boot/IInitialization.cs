using System;

namespace Assets.Scripts.Tetris
{
    public interface IInitialization
    {
        public event Action OnInitializedEvent;
    }
}