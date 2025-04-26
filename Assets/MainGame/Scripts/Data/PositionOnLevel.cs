using System;
using MainGame.Scripts.Infrastructure;

namespace MainGame.Scripts.Data
{
    [Serializable]
    public class PositionOnLevel
    {
        public SceneName Level;
        public Vector3Data Position;

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