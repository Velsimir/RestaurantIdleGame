using System;
using MainGame.Scripts.Services.InputService;
using MainGame.Scripts.UI;
using UnityEngine;

namespace MainGame.Scripts.Infrastructure
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
        }

        public void Enter()
        {
            RegisterServices();
            _sceneLoader.Load(SceneName.Initial, onLoaded: EnterLoadedLevel);
        }

        private void EnterLoadedLevel()
        {
            
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