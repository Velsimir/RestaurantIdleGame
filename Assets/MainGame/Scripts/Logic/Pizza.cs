using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public class Pizza : MonoBehaviour, ISpawnable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _transform;

        public event Action<ISpawnable> Dissapear;
        
        public Bounds Bounds => _collider.bounds;

        private void OnDisable()
        {
            Dissapear?.Invoke(this);
        }

        public void SetParent(Transform holdPizzaPoint)
        {
            _transform.SetParent(holdPizzaPoint);
        }
    }
}