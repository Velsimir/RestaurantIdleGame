namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public interface IState : IExitableState
    {
        void Enter();
    }
}