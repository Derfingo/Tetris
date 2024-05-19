using System;
using TMPro;
using UnityEngine;

namespace Assets.Scripts.Tetris
{
    public class ViewController : MonoBehaviour, IPauseView
    {
        [SerializeField] private SimpleButton _pauseButton;
        [SerializeField] private TMP_Text _pauseText;

        public event Action<bool> OnPauseClickEvent;

        public void Initialize()
        {
            _pauseButton.OnClickEvent += OnClickPause;
            _pauseText.enabled = false;
        }

        private void OnClickPause(bool isPressed)
        {
            OnPauseClickEvent?.Invoke(isPressed);
            _pauseText.enabled = !_pauseText.enabled;
        }
    }
}
