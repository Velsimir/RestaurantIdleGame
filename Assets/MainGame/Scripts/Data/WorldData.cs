using System;
using MainGame.Scripts.Infrastructure;
using UnityEngine;

namespace MainGame.Scripts.Data
{
    [Serializable]
    public class WorldData
    {
        public PositionOnLevel PositionOnLevel;
    }

    public class PositionOnLevel
    {
        public SceneName Level;
        public Vector3Data Position;

        public PositionOnLevel(SceneName level, Vector3Data position)
        {
            Level = level;
            Position = position;
        }
    }
}