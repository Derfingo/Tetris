using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public class SettingsView : ViewBase, ISettingsView
    {
        [SerializeField] private Button _mainMenuButton;

        public Button MainMenuButton => _mainMenuButton;
    }
}