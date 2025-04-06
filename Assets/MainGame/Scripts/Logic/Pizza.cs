using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    [RequireComponent(typeof(Collider))]
    public class Pizza : MonoBehaviour, ISpawnable
    {
        private Transform _transform;
        private Collider _collider;

        public event Action<ISpawnable> Dissapear;
        
        public Bounds Bounds => _collider.bounds;

        private void Awake()
        {
            _transform = transform;
            _collider = GetComponent<Collider>();
        }

        private void OnDisable()
        {
            Dissapear?.Invoke(this);
        }

        public void SetParent(Transform holdPizzaPoint)
        {
            _transform.SetParent(holdPizzaPoint);
            _transform.localPosition = Vector3.zero;
        }
    }
}