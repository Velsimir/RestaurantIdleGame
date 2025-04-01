using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Logic.PlayerLogic.Movement;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Factory
{
    public interface IGameFactory : IService
    {
        List<ISavedProgressReader> ProgressReaders { get; }
        List<ISavedProgress> ProgressWriters { get; }
        GameObject CreateHero(GameObject at);
        void Cleanup();
    }
}