using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.InputService
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
        public abstract void Refresh();
    }
}