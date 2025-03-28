using MainGame.Scripts.PlayerLogic.Animations;
using MainGame.Scripts.PlayerLogic.Movement;
using UnityEngine;

namespace MainGame.Scripts.PlayerLogic
{
    [RequireComponent(typeof(CharacterController))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float _movementSpeed;
        [SerializeField] private Animator _animator;
        
        private CharacterController _characterController;
        private PlayerMover _playerMover;
        private PlayerAnimator _playerAnimator;
        
        private void Awake()
        {
            _characterController = GetComponent<CharacterController>();
            
            _playerMover = new PlayerMover(_characterController, Camera.main, _movementSpeed);

            _playerAnimator = new PlayerAnimator(_animator, _characterController);
        }

        private void Update()
        {
            _playerAnimator.Update(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _playerMover.Update(Time.fixedDeltaTime);
        }
    }
}