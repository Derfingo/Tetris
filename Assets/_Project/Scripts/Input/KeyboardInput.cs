using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Assets.Scripts.Tetris
{
    public class KeyboardInput : IPause
    {
        public event Action OnDrop;
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
            _input.Keyboard.Rotate.performed += Rotate;
            _input.Keyboard.Drop.performed += Drop;
        }

        private void RemoveListeners()
        {
            _input.Keyboard.Move.performed -= MoveHorizontal;
            _input.Keyboard.Rotate.performed -= Rotate;
            _input.Keyboard.Drop.performed -= Drop;
        }
    }
}
