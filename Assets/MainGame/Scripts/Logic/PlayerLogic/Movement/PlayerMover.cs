using System;
using MainGame.Scripts.Data;
using MainGame.Scripts.ExtensionMethods;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.InputService;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MainGame.Scripts.Logic.PlayerLogic.Movement
{
    public class PlayerMover : IUpdatable, ISavedProgress
    {
        private readonly float _movementSpeed;
        private readonly CharacterController _characterController;
        private readonly IInputService _inputService;
        private readonly Camera _camera;

        public PlayerMover(CharacterController characterController, Camera camera, float movementSpeed)
        {
            _inputService = AllServices.Container.Single<IInputService>();
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

        public void LoadProgress(PlayerProgress progress)
        {
            if (GetCurrentLevel() == progress.WorldData.PositionOnLevel.Level)
            {
                var savedPosition = progress.WorldData.PositionOnLevel.Position;

                if (savedPosition != null)
                {
                    Warp(to: savedPosition);
                }
            }
        }

        private void Warp(Vector3Data to)
        {
            _characterController.enabled = false;
            _characterController.transform.position = to.AsUnityVector3();
            _characterController.enabled = true;
        }

        private static SceneName GetCurrentLevel()
        {
            string scene = SceneManager.GetActiveScene().name;

            if (Enum.TryParse(scene, out SceneName sceneName))
            {
                return sceneName;
            }
            else
            {
                throw new ArgumentException($"Scene {scene} was not found");
            }
        }

        public void UpdateProgress(PlayerProgress progress)
        {
            progress.WorldData.PositionOnLevel = new PositionOnLevel
                (GetCurrentLevel(),_characterController.transform.position.AsVector3Data());
        }
    }
}