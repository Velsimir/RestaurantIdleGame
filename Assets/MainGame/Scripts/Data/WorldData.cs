using System;
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
        public string LevelName;
        public Vector3Data Position;

        public PositionOnLevel(string levelName, Vector3Data position)
        {
            LevelName = levelName;
            Position = position;
        }
    }
}