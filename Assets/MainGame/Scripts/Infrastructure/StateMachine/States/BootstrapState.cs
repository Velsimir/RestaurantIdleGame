using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.Infrastructure.Factory;
using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.Services.InputService;
using MainGame.Scripts.Infrastructure.Services.PersistentProgress;
using MainGame.Scripts.Infrastructure.Services.SaveLoad;
using MainGame.Scripts.UI;
using UnityEngine;
using YG;

namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public class BootstrapState : IState
    {
        private readonly GameStateMachine _gameStateMachine;
        private readonly SceneLoader _sceneLoader;
        private readonly AllServices _services;
        private readonly ICoroutineRunner _coroutineRunner;

        public BootstrapState(GameStateMachine gameStateMachine, SceneLoader sceneLoader, AllServices allServices, ICoroutineRunner coroutineRunner)
        {
            _gameStateMachine = gameStateMachine;
            _sceneLoader = sceneLoader;
            _services = allServices;
            _coroutineRunner = coroutineRunner;
            RegisterServices();
        }

        public void Enter()
        {
            YG2.onGetSDKData += LoadLoadedLevelWhenReady;
        }

        public void Exit()
        { }

        private void LoadLoadedLevelWhenReady()
        {
            YG2.onGetSDKData -= LoadLoadedLevelWhenReady;
            _sceneLoader.Load(SceneName.Initial, onLoaded: EnterLoadedLevel);
        }

        private void EnterLoadedLevel()
        {
            _gameStateMachine.Enter<LoadProgressState>();
        }

        private void RegisterServices()
        {
            _services.RegisterSingle<ICoroutineRunner>(_coroutineRunner);
            _services.RegisterSingle<IInputService>(RegisterInputService());
            _services.RegisterSingle<IAsset>(new Asset());
            _services.RegisterSingle<IPersistentProgressService>(new PersistentProgressService());
            _services.RegisterSingle<IGameFactory>(new GameFactory(_services.Single<IAsset>()));
            _services.RegisterSingle<ISaveLoadService>(new SaveLoadService(_services.Single<IPersistentProgressService>(), _services.Single<IGameFactory>()));
        }
        
        private IInputService RegisterInputService()
        {
            if (Application.isEditor == false)
            {
                return new StandaloneInputService();
            }
            else
            {
                return CreateMobileInput();
            }
        }

        private static IInputService CreateMobileInput()
        {
            MobileJoystickHud mobileJoystickHud = 
                Resources.Load<MobileJoystickHud>(AssetPath.MobileJoystickHud);
                
            mobileJoystickHud = Object.Instantiate(mobileJoystickHud);
                
            return new MobileInputService(mobileJoystickHud.FloatingJoystick);
        }
    }
}