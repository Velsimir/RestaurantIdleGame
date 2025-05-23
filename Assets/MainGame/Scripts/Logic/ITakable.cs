using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public interface ITakable : ISpawnable
    {
        public Collider Collider { get; }
        public void Take(Transform parent, Vector3 position);
    }
}