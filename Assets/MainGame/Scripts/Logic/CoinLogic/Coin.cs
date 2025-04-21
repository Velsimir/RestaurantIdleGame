using System;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using UnityEngine;

namespace MainGame.Scripts.Logic.CoinLogic
{
    public class Coin : MonoBehaviour, ISpawnable
    {
        public event Action<ISpawnable> Disappeared;
    
        public void Disappear()
        {
            gameObject.SetActive(false);
            Disappeared?.Invoke(this);
        }
    }
}
