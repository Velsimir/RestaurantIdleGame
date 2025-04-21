using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;

namespace MainGame.Scripts.Logic.CoinLogic
{
    public class CoinSpawner
    {
        private readonly ObjectHoldPoint _holdPoint; 
        private readonly IGameFactory _factory;
        
        public CoinSpawner(ObjectHoldPoint holdPoint)
        {
            _holdPoint = holdPoint;
            _factory = AllServices.Container.Single<IGameFactory>();
        }

        public void SpawnCoin()
        {
            Coin coin = _factory.CreateCoin();
            
            coin.transform.position = _holdPoint.transform.position;
        }
    }
}