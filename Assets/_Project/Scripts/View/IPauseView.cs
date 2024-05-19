using System;

namespace Assets.Scripts.Tetris
{
    public interface IPauseView
    {
        public event Action<bool> OnPauseClickEvent;
    }
}