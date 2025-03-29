using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public interface IGameFactory
    {
        GameObject CreateHero(GameObject at);
    }
}