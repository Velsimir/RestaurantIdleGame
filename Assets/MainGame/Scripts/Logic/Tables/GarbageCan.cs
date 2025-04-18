using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic
{
    public class GarbageCan : MonoBehaviour
    {
        [SerializeField] private Transform _transform;

        public void TakeOutGarbage(ISpawnable garbage)
        {
            garbage.Disappear();
        }
    }
}