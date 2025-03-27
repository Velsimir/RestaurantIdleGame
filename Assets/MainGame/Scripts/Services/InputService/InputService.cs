using UnityEngine;

namespace MainGame.Scripts.Services.InputService
{
    public abstract class InputService : IInputService
    {
        public abstract Vector2 Axis { get; }
    }
}