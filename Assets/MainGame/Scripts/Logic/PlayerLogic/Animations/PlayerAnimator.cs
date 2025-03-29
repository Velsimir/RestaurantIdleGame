using MainGame.Scripts.Logic.PlayerLogic.Movement;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic.Animations
{
    public class PlayerAnimator : IUpdatable
    {
        public readonly int Speed = Animator.StringToHash(nameof(Speed));
        
        private readonly CharacterController _characterController;
        private readonly Animator _animator;

        public PlayerAnimator(Animator animator, CharacterController characterController)
        {
            _characterController = characterController;
            _animator = animator;
        }

        public void Update(float deltaTime)
        {
            _animator.SetFloat(Speed, _characterController.velocity.magnitude);
        }
    }
}