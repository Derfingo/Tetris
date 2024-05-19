using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets.Scripts.Tetris
{
    public class SimpleButton : MonoBehaviour, IPointerDownHandler
    {
        private bool _isPressed = false;

        public event Action<bool> OnClickEvent;

        public void OnPointerDown(PointerEventData eventData)
        {
            _isPressed = !_isPressed;
            OnClickEvent?.Invoke(_isPressed);
        }
    }
}