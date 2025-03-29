using UnityEngine;

namespace MainGame.Scripts.Infrastructure.Services.InputService
{
    public class MobileInputService : InputService
    {
        private readonly FloatingJoystick _floatingJoystick;
        
        public MobileInputService(FloatingJoystick floatingJoystick)
        {
            _floatingJoystick = floatingJoystick;
        }

        public override Vector2 Axis =>
            new(_floatingJoystick.Horizontal,
                _floatingJoystick.Vertical);
    }
}