using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class ViewTransition : ViewBase
    {
        [SerializeField] private SettingsView _settingsView;
        [SerializeField] private PauseView _pauseView;
        [SerializeField] private MainView _mainView;

        public void Initialize()
        {
            _mainView.StartButton.onClick.AddListener(OnStartClick);
            _mainView.SettingsButton.onClick.AddListener(OnSettingsClick);
            _settingsView.MainMenuButton.onClick.AddListener(OnMainMenuClick);
        }

        private void OnStartClick()
        {
            _mainView.Hide();
        }

        private void OnSettingsClick()
        {
            _mainView.Hide();
            _settingsView.Show();
        }

        private void OnMainMenuClick()
        {
            _settingsView.Hide();
            _mainView.Show();
        }
    }
}
