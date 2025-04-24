using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Logic;
using MainGame.Scripts.Logic.CoinLogic;
using MainGame.Scripts.Logic.Npc;
using MainGame.Scripts.Logic.PlayerLogic;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateHero(Transform at);
        void Cleanup();
        Pizza CreatePizza();
        Customer CreateCustomer();
        Coin CreateCoin();
        void CreateHUD(PlayerWallet getComponent);
    }
}