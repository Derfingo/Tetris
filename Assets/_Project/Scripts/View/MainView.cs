using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public class MainView : ViewBase, IMainView
    {
        [SerializeField] private Button _startButton;
        [SerializeField] private Button _settingsButton;

        public event Action OnSettingsClickEvent;
        public event Action OnStartClickEvent;

        public Button StartButton => _startButton;
        public Button SettingsButton => _settingsButton;

        public void Initialize()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
            _startButton.gameObject.SetActive(false);
            _settingsButton.gameObject.SetActive(false);
            gameObject.SetActive(true);
        }

        public void ShowButtons()
        {
            _startButton.gameObject.SetActive(true);
            _settingsButton.gameObject.SetActive(true);
        }

        private void OnStartClick()
        {
            OnStartClickEvent?.Invoke();
        }

        private void OnSettingsClick()
        {
            OnSettingsClickEvent?.Invoke();
        }
    }
}
