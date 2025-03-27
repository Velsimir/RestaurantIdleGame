using UnityEngine;

namespace MainGame.Scripts.Services.InputService
{
    public class StandaloneInputService : InputService
    {
        private readonly PcInputSystem _pcInputSystem;
        
        public StandaloneInputService()
        {
            _pcInputSystem = new PcInputSystem();
            _pcInputSystem.Enable();
        }

        public override Vector2 Axis =>
            new(_pcInputSystem.Player.Move.ReadValue<Vector2>().x,
                _pcInputSystem.Player.Move.ReadValue<Vector2>().y);
    }
}