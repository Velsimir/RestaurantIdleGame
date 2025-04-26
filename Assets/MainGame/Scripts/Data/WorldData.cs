using System;
using MainGame.Scripts.Infrastructure;

namespace MainGame.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
        
        public WorldData(SceneName initialLevel)
        {
            PositionOnLevel = new PositionOnLevel(initialLevel);
        }
    }
}