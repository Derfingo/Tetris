using System;

namespace Assets.Scripts.Tetris
{
    public interface IGameState
    {
        public event Action OnReadyToStartEvent;
        void StartOver();
    }
}