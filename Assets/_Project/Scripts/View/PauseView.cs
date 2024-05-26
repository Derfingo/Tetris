using System;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class PauseView : ViewBase, IPauseView
    {
        [SerializeField] private SimpleButton _pauseButton;
        [SerializeField] private Transform _pausePanel;

        public event Action<bool> OnPauseClickEvent;

        public void Initialize()
        {
            _pauseButton.OnClickEvent += OnPauseClick;
            _pausePanel.gameObject.SetActive(false);
        }

        private void OnPauseClick(bool isPressed)
        {
            OnPauseClickEvent?.Invoke(isPressed);
            _pausePanel.gameObject.SetActive(isPressed);
        }
    }
}
