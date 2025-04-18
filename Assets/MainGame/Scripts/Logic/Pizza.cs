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

        public event Action<ISpawnable> Disappeared;

        public Bounds Bounds => _collider.bounds;

        private void OnDisable()
        {
            Disappeared?.Invoke(this);
        }

        public void SetParent(Transform holdPizzaPoint)
        {
            _transform.SetParent(holdPizzaPoint);
        }

        public void Disappear()
        {
            _transform.SetParent(null);
            _transform.gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}