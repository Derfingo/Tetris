using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Tetris
{
    public class ViewController : MonoBehaviour, IPauseView, IShow
    {
        [SerializeField] private SimpleButton _pauseButton;
        [SerializeField] private TMP_Text _pauseText;
        [SerializeField] private Image _figureImage;

        public event Action<bool> OnPauseClickEvent;

        public void Initialize()
        {
            _pauseButton.OnClickEvent += OnClickPause;
            _pauseText.enabled = false;
        }

        public void Show(Image image)
        {
            _figureImage.sprite = image.sprite;
            _figureImage.color = image.color;
            _figureImage.SetNativeSize();
        }

        private void OnClickPause(bool isPressed)
        {
            OnPauseClickEvent?.Invoke(isPressed);
            _pauseText.enabled = !_pauseText.enabled;
        }
    }
}
