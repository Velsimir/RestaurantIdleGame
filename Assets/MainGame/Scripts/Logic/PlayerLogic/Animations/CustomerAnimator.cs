using MainGame.Scripts.Logic.PlayerLogic.Movement;
using Pathfinding;
using UnityEngine;

namespace MainGame.Scripts.Logic.PlayerLogic.Animations
{
    public class CustomerAnimator : IUpdatable
    {
        public readonly int Speed = Animator.StringToHash(nameof(Speed));
        
        private readonly AIPath _aIPath;
        private readonly Animator _animator;

        public CustomerAnimator(Animator animator, AIPath aIPath)
        {
            _aIPath = aIPath;
            _animator = animator;
        }

        public void Update(float deltaTime)
        {
            _animator.SetFloat(Speed, _aIPath.velocity.magnitude);
        }
    }
}