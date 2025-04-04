using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic.Npc
{
    public class Customer : MonoBehaviour, ISpawnable
    {
        public event Action<ISpawnable> Dissapear;

        private void OnDisable()
        {
            Dissapear?.Invoke(this);
        }
    }
}