using System;
using MainGame.Scripts.Infrastructure;
using MainGame.Scripts.StaticData;

namespace MainGame.Scripts.Data
{
    [Serializable]
    public class PlayerProgress
    {
        public WorldData WorldData;
        public int MaxPizzaHoldCount = 4;

        public PlayerProgress(SceneName initialLevel)
        {
            WorldData = new WorldData(initialLevel);
        }
    }
}