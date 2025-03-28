using MainGame.Scripts.Services.InputService;

namespace MainGame.Scripts.Infrastructure
{
    public class Game
    {
        public GameStateMachine StateMachine;
        public static IInputService InputService;

        public Game(ICoroutineRunner coroutineRunner)
        {
            StateMachine = new GameStateMachine(new SceneLoader(coroutineRunner));
        }
    }
}