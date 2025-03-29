using UnityEngine;

namespace MainGame.Scripts.Services.InputService
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
    }
}