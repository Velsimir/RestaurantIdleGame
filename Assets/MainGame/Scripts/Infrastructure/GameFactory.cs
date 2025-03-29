using MainGame.Scripts.Infrastructure.AssetManagment;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class GameFactory : IGameFactory
    {
        private const string PlayerPath = "Prefabs/Player/Player";
        
        private readonly AssetProvider _assets;

        public GameFactory(AssetProvider assets)
        {
            _assets = assets;
        }

        public GameObject CreateHero(GameObject at)
        {
            return _assets.Instantiate(PlayerPath, at.transform.position);
        }
    }
}