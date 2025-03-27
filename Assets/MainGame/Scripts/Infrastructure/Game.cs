using MainGame.Scripts.Services.InputService;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class Game
    {
        public static IInputService InputService;

        public Game()
        {
            RegisterInputService();
        }

        private void RegisterInputService()
        {
            if (Application.isEditor)
            {
                InputService = new StandaloneInputService();
            }
            else
            {
                MobileJoystickHud mobileJoystickHud = Resources.Load<MobileJoystickHud>("UI/MobileJoystick/MobileJoystickHud");
                mobileJoystickHud = GameObject.Instantiate(mobileJoystickHud);
                InputService = new MobileInputService(mobileJoystickHud.FloatingJoystick);
            }
        }
    }
}