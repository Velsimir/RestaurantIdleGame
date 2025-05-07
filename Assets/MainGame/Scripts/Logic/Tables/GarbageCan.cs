using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic.Tables
{
    public class GarbageCan : MonoBehaviour, IActivatable
    {
        [SerializeField] private Transform _transform;

        public void TakeOutGarbage(ISpawnable garbage)
        {
            garbage.Disappear();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
        }
    }
}