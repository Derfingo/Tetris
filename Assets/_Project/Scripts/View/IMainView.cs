using System;

namespace Assets.Scripts.Tetris
{
    public interface IMainView
    {
        public event Action OnSettingsClickEvent;
        public event Action OnStartClickEvent;

        void ShowButtons();
    }
}