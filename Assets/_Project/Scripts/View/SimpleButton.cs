using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Tetris
{
    public class SimpleButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerClickHandler
    {
        private bool _isPressed = false;

        public event Action<bool> OnClickEvent;
        public event Action<bool> OnUpClickEvent;
        public event Action<bool> OnDownClickEvent;

        public void OnPointerClick(PointerEventData eventData)
        {
            _isPressed = !_isPressed;
            OnClickEvent?.Invoke(_isPressed);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnDownClickEvent?.Invoke(true);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnUpClickEvent?.Invoke(false);
        }
    }
}