using MainGame.Scripts.StaticData;

namespace MainGame.Scripts.Infrastructure.Services.StaticData
{
    public interface IStaticDataService : IService
    {
        public PlayerStaticData PlayerStaticData { get; }
    }
}