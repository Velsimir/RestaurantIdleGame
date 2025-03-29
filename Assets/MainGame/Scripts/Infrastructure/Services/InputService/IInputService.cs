using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.InputService
{
    public interface IInputService : IService
    {
        Vector2 Axis { get; }
    }
}