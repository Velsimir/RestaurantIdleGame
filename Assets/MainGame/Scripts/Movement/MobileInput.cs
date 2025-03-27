using System;
using UnityEngine;

namespace MainGame.Scripts.Movement
{
    public class MobileInput : IDisposable
    {
        private readonly FloatingJoystick _floatingJoystick;
        private Vector3 _movementVector;
        
        public MobileInput(FloatingJoystick floatingJoystick)
        {
            _floatingJoystick = floatingJoystick;
            _floatingJoystick.Draged += SetNewMovementVector;
        }

        public event Action<Vector3> InputChanged;

        private void SetNewMovementVector()
        {
            _movementVector = new Vector3(_floatingJoystick.Horizontal, 0, _floatingJoystick.Vertical);
            InputChanged?.Invoke(_movementVector);
        }

        public void Dispose()
        {
            _floatingJoystick.Draged -= SetNewMovementVector;
        }
    }
}