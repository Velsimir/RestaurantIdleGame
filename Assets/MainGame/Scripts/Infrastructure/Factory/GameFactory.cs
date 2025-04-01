using System.Collections.Generic;
using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Logic.PlayerLogic.Movement;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Factory
{
    public class GameFactory : IGameFactory
    {
        private readonly IAsset _assets;

        public GameFactory(IAsset assets)
        {
            _assets = assets;
        }

        public List<ISavedProgressReader> ProgressReaders { get; } = new List<ISavedProgressReader>();
        public List<ISavedProgress> ProgressWriters { get; } = new List<ISavedProgress>();

        public GameObject CreateHero(GameObject at)
        {
            return InstantiateRegistered(AssetPath.PlayerPath, at.transform.position);
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
        
        private GameObject InstantiateRegistered(string prefabsPlayerPath)
        {
            GameObject gameObject = _assets.Instantiate(prefabsPlayerPath);
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