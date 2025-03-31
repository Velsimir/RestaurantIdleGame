namespace MainGame.Scripts.Infrastructure.StateMachine.States
{
    public interface IPayloadState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
}