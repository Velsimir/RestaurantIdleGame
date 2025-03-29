using MainGame.Scripts.Services.InputService;
using MainGame.Scripts.UI;

namespace MainGame.Scripts.Infrastructure
{
    public class Game
    {
        public readonly GameStateMachine StateMachine;
        public static IInputService InputService;

        public Game(ICoroutineRunner coroutineRunner, Curtain curtain)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner), curtain);
        }
    }
}