using System;
using MainGame.Scripts.Services.InputService;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;

        public BootstrapState(GameStateMachine gameStateMachine)
        {
            _gameStateMachine = gameStateMachine;
        }

        public void Enter()
        {
            RegisterServices();
        }

        public void Exit()
        {
        }

        private void RegisterServices()
        {
            Game.InputService = RegisterInputService();
        }
        
        private InputService RegisterInputService()
        {
            if (Application.isEditor)
            {
                return new StandaloneInputService();
            }
            else
            {
                MobileJoystickHud mobileJoystickHud = 
                    Resources.Load<MobileJoystickHud>("UI/MobileJoystick/MobileJoystickHud");
                
                mobileJoystickHud = GameObject.Instantiate(mobileJoystickHud);
                
                return new MobileInputService(mobileJoystickHud.FloatingJoystick);
            }
        }
    }
}