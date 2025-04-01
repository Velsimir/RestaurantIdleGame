using System;
using MainGame.Scripts.Infrastructure;

namespace MainGame.Scripts.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public readonly SceneName Level;
        public readonly Vector3Data Position;

        public PositionOnLevel(SceneName level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }

        public PositionOnLevel(SceneName initialLevel)
        {
            Level = initialLevel;
        }
    }
}