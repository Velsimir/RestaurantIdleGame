using MainGame.Scripts.Data;
using MainGame.Scripts.Infrastructure.Services;

namespace MainGame.Scripts.Infrastructure.StateMachine
{
    public interface ISaveLoadService : IService
    {
        void Save();
        PlayerProgress Load();
    }
}