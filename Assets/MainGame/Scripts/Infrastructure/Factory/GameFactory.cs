using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Infrastructure.Services;
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

        public GameObject CreateHero(GameObject at)
        {
            return _assets.Instantiate(AssetPath.PlayerPath, at.transform.position);
        }
    }
}