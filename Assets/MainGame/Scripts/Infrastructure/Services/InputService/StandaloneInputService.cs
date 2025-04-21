using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.InputService
{
    public class StandaloneInputService : InputService
    {
        private readonly InputActions _inputAction;
        
        public StandaloneInputService()
        {
            _inputAction = new InputActions();
            _inputAction.Enable();
        }

        public override Vector2 Axis =>
            new(_inputAction.Player.Move.ReadValue<Vector2>().x,
                _inputAction.Player.Move.ReadValue<Vector2>().y);

        public override void Refresh()
        {
            _inputAction.Player.Move.Reset();
        }
    }
}