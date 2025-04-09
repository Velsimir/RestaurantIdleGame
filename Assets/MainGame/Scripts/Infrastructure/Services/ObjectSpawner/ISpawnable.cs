using System;

namespace MainGame.Scripts.Infrastructure.Services.ObjectSpawner
{
    public interface ISpawnable
    {
        public event Action<ISpawnable> Disappeared;
        public void Disappear();
    }
}