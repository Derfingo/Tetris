using System;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public class SettingsView : ViewBase, ISettingsView
    {
        [SerializeField] private FPSCounter _fpsCounter;
        [SerializeField] private Button _mainMenuButton;
        [SerializeField] private Toggle _effectToggle;
        [SerializeField] private Toggle _musicToggle;
        [SerializeField] private Toggle _fpsToggle;

        public event Action<bool> OnShowFPSCounterEvent;
        public event Action<bool> OnChangeEffectEvent;
        public event Action<bool> OnChangeMusicEvent;

        public Button MainMenuButton => _mainMenuButton;
        public Toggle EffectToggle => _effectToggle;
        public Toggle MusicToggle => _musicToggle;

        public void Initialize()
        {
            _effectToggle.onValueChanged.AddListener(OnEffectToggleClick);
            _musicToggle.onValueChanged.AddListener(OnMusicToggleClick);
            _fpsToggle.onValueChanged.AddListener(OnFPSToggleClick);
        }

        public void SetMuteEffect(bool isMute)
        {
            _effectToggle.isOn = isMute;
        }

        public void SetMuteMusic(bool isMute)
        {
            _musicToggle.isOn = isMute;
        }

        public void SetViewFPSCounter(bool isView)
        {
            _fpsToggle.isOn = isView;
        }

        private void OnEffectToggleClick(bool isClick)
        {
            OnChangeEffectEvent?.Invoke(isClick);
        }

        private void OnMusicToggleClick(bool isClick)
        {
            OnChangeMusicEvent?.Invoke(isClick);
        }

        private void OnFPSToggleClick(bool isClick)
        {
            _fpsCounter.gameObject.SetActive(isClick);
            OnShowFPSCounterEvent?.Invoke(isClick);
        }
    }
}