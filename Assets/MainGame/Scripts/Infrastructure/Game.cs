using MainGame.Scripts.Infrastructure.Services;
using MainGame.Scripts.Infrastructure.StateMachine;
using MainGame.Scripts.UI;

namespace MainGame.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;

        public Game(ICoroutineRunner coroutineRunner, Curtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
        }
    }
}