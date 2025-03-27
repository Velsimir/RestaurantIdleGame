using MainGame.Scripts.ExtensionMethods;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Services.InputService;
using UnityEngine;

namespace MainGame.Scripts.Movement
{
    public class PlayerMover : IUpdatable
    {
        private readonly float _movementSpeed;
        private readonly CharacterController _characterController;
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        public PlayerMover(CharacterController characterController, Camera camera, float movementSpeed)
        {
            _inputService = Game.InputService;
            _characterController = characterController;
            _camera = camera;
            _movementSpeed = movementSpeed;
        }

        public void Update(float deltaTime)
        {
            Move();
        }

        private void Move()
        {
            Vector3 movementVector = Vector3.zero;

            if (_inputService.Axis.sqrMagnitude > Constants.Epsilon)
            {
                movementVector = _camera.transform.TransformDirection(_inputService.Axis);
                movementVector.y = 0;
                movementVector.Normalize();

                _characterController.transform.forward = movementVector;
            }

            movementVector += Physics.gravity;

            _characterController.Move(movementVector * (_movementSpeed * Time.deltaTime));
        }
    }
}