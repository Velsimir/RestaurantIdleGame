using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace MainGame.Scripts.Movement
{
    public class InputHandler : IDisposable
    {
        private InputSystem _pcInputSystem;
        private MobileInput _mobileInput;
        private bool _isMobilePlatform;
        
        public Vector3 MovementVector { get; private set; }

        public void Initialize(FloatingJoystick floatingJoystick)
        {
            _isMobilePlatform = Application.isMobilePlatform;
            
            if (_isMobilePlatform)
            {
                ActivateMobileInput(floatingJoystick);
            }
            else
            {
                ActivatePcInput();
            }
        }

        private void ActivatePcInput()
        {
            _pcInputSystem = new InputSystem();
            _pcInputSystem.Enable();
            _pcInputSystem.Player.Move.started += OnMovePc;
            _pcInputSystem.Player.Move.canceled += OnMovePc;
        }
        
        private void DeactivatePcInput()
        {
            _pcInputSystem.Player.Move.started -= OnMovePc;
            _pcInputSystem.Player.Move.canceled -= OnMovePc;
            _pcInputSystem.Disable();
        }

        private void ActivateMobileInput(FloatingJoystick floatingJoystick)
        {
            _mobileInput = new MobileInput(floatingJoystick);
            _mobileInput.InputChanged += OnMoveMobile;
        }

        private void DeactivateMobileInput()
        {
            _mobileInput.InputChanged -= OnMoveMobile;
        }

        private void OnMoveMobile(Vector3 direction)
        {
            MovementVector = direction;
        }

        private void OnMovePc(InputAction.CallbackContext obj)
        {
            MovementVector = obj.ReadValue<Vector3>();
        }

        public void Dispose()
        {
            if (_isMobilePlatform)
            {
                DeactivateMobileInput();
            }
            else
            {
                DeactivatePcInput();
            }
        }
    }
}