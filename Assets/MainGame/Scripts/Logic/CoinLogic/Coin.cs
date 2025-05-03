using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic.CoinLogic
{
    [RequireComponent(typeof(Collider), 
        typeof(TossCoinEffect))]
    public class Coin : MonoBehaviour, ISpawnable, IStackable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private TossCoinEffect _tossCoinEffect;
        
        public event Action<ISpawnable> Disappeared;

        public Collider Collider => _collider;

        private void OnEnable()
        {
            _tossCoinEffect.EffectEnded += Disappear;
        }
        
        private void OnDisable()
        {
            _tossCoinEffect.EffectEnded -= Disappear;
        }

        public void Interact()
        {
            _tossCoinEffect.Toss();
        }

        public void Disappear()
        {
            gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}
