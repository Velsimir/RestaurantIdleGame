using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Infrastructure.Services.ObjectSpawner;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Logic;
using MainGame.Scripts.Logic.CoinLogic;
using MainGame.Scripts.Logic.Npc;
using MainGame.Scripts.Logic.PlayerLogic;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _assets;

        private readonly ISpawnerService<Customer> _customerSpawner;
        private readonly ISpawnerService<Pizza> _pizzaSpawner;
        private readonly ISpawnerService<Coin> _coinSpawner;

        public GameFactory(IAsset assets)
        {
            _assets = assets;
            _customerSpawner = new SpawnerService<Customer>(_assets.GetPrefab<Customer>(AssetPath.Customer));
            _pizzaSpawner = new SpawnerService<Pizza>(_assets.GetPrefab<Pizza>(AssetPath.Pizza));
            _coinSpawner = new SpawnerService<Coin>(_assets.GetPrefab<Coin>(AssetPath.Coin));
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject CreateHero(Transform at)
        {
            return InstantiateRegistered(AssetPath.Player, at.position);
        }

        public Pizza CreatePizza()
        {
            return _pizzaSpawner.Spawn();
        }

        public Customer CreateCustomer(Transform at)
        {
            return _customerSpawner.Spawn(at); 
        }

        public Coin CreateCoin()
        {
            return _coinSpawner.Spawn();
        }

        public void CreateHUD(PlayerWallet playerWallet)
        {
            InstantiateRegistered(AssetPath.PlayerHUD, Vector3.zero).TryGetComponent(out PlayerWalletUi playerWalletUi);
            
            playerWalletUi.Initialize(playerWallet);
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