using System;
using MainGame.Scripts.Infrastructure;

namespace MainGame.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public int MaxPizzaHoldCount;
        public int Coins;

        public PlayerProgress(SceneName initialLevel)
        {
            WorldData = new WorldData(SceneName.Game);
            MaxPizzaHoldCount = 2;
            Coins = 0;
        }
    }
}