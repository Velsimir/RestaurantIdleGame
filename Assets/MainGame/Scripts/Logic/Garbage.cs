using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class Garbage : MonoBehaviour, ITakable
    {
        [SerializeField] private Collider _collider;
        [SerializeField] private Transform _transform;
        
        public event Action<ISpawnable> Disappeared;

        public Collider Collider => _collider;

        public void Take(Transform parent, Vector3 position)
        {
            _transform.SetParent(parent);
            _transform.position = position;
        }

        public void Disappear()
        {
            _transform.SetParent(null);
            _transform.gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}