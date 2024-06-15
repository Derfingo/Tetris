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
            AddListeners();
            _startButton.gameObject.SetActive(false);
            _settingsButton.gameObject.SetActive(false);
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

        private void AddListeners()
        {
            _startButton.onClick.AddListener(OnStartClick);
            _settingsButton.onClick.AddListener(OnSettingsClick);
        }

        private void RemoveListeners()
        {
            _startButton.onClick.RemoveAllListeners();
            _settingsButton.onClick.RemoveAllListeners();
        }

        ~MainView()
        {
            RemoveListeners();
        }
    }
}
