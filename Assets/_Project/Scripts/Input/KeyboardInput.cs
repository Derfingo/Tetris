﻿using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Tetris
{
    public class KeyboardInput : IPause, IInput
    {
        public event Action OnDrop;
        public event Action<bool> OnDropSlow;
        public event Action<int> OnRotate;
        public event Action<int> OnMoveHorizontal;

        private readonly InputDevices _input;

        public KeyboardInput()
        {
            _input = new InputDevices();
            _input.Enable();
            AddListeners();
        }

        public void Pause(bool isPaused)
        {
            if (isPaused)
            {
                _input.Disable();
            }
            else
            {
                _input.Enable();
            }
        }

        private void Drop(InputAction.CallbackContext context)
        {
            OnDrop?.Invoke();
        }

        private void DropSlow(InputAction.CallbackContext context)
        {
            if (context.started)
            {
                OnDropSlow?.Invoke(true);
            }

            if (context.canceled)
            {
                OnDropSlow?.Invoke(false);
            }
        }

        private void Rotate(InputAction.CallbackContext context)
        {
            OnRotate?.Invoke((int)context.ReadValue<Vector2>().x);
        }

        private void MoveHorizontal(InputAction.CallbackContext context)
        {
            OnMoveHorizontal?.Invoke((int)context.ReadValue<Vector2>().x);
        }

        private void AddListeners()
        {
            _input.Keyboard.Move.performed += MoveHorizontal;
            _input.Keyboard.DropSlow.started += DropSlow;
            _input.Keyboard.DropSlow.canceled += DropSlow;
            _input.Keyboard.Rotate.performed += Rotate;
            _input.Keyboard.Drop.performed += Drop;
        }

        private void RemoveListeners()
        {
            _input.Keyboard.Move.performed -= MoveHorizontal;
            _input.Keyboard.DropSlow.started -= DropSlow;
            _input.Keyboard.DropSlow.canceled -= DropSlow;
            _input.Keyboard.Rotate.performed -= Rotate;
            _input.Keyboard.Drop.performed -= Drop;
        }

        ~KeyboardInput()
        {
            RemoveListeners();
        }
    }
}
