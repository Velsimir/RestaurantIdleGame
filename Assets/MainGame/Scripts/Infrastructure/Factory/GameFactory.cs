using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Logic.Npc;
using MainGame.Scripts.Logic.PlayerLogic.Movement;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _assets;

        private readonly ISpawnerService<Customer> _customerSpawner;
        private readonly ISpawnerService<Pizza> _pizzaSpawner;

        public GameFactory(IAsset assets)
        {
            _assets = assets;
            _customerSpawner = new SpawnerService<Customer>(_assets.GetPrefab<Customer>(AssetPath.Customer));
            _pizzaSpawner = new SpawnerService<Pizza>(_assets.GetPrefab<Pizza>(AssetPath.Pizza));
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject CreateHero(Transform at)
        {
            return InstantiateRegistered(AssetPath.Player, at.position);
        }

        public Pizza CreatePizza(Transform at)
        {
            return _pizzaSpawner.Spawn();
        }

        public void Cleanup()
        {
            ProgressReaders.Clear();
            ProgressWriters.Clear();
        }

        private GameObject InstantiateRegistered(string prefabsPlayerPath, Vector3 transformPosition)
        {
            GameObject gameObject = _assets.Instantiate(prefabsPlayerPath, transformPosition);
            RegisterProgressWatchers(gameObject);
            return gameObject;
        }

        private void RegisterProgressWatchers(GameObject gameObject)
        {
            foreach (ISavedProgressReader progressReader in gameObject.GetComponentsInChildren<ISavedProgressReader>())
            {
                Register(progressReader);
            }
        }

        private void Register(ISavedProgressReader progressReader)
        {
            if (progressReader is ISavedProgress progressWriter)
            {
                ProgressWriters.Add(progressWriter);
            }

            ProgressReaders.Add(progressReader);
        }
    }
}