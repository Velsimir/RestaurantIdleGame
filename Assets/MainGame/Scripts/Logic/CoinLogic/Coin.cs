using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic.CoinLogic
{
    [RequireComponent(typeof(Collider), 
        typeof(TossCoinEffect))]
    public class Coin : MonoBehaviour, ITakable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private TossCoinEffect _tossCoinEffect;
        
        public event Action<ISpawnable> Disappeared;

        public Collider Collider => _collider;
        public void Take(Transform parent, Vector3 position)
        {
            throw new NotImplementedException();
        }

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
