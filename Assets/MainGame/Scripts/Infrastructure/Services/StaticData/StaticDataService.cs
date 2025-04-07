using MainGame.Scripts.Infrastructure.AssetManagment;
using MainGame.Scripts.StaticData;

namespace MainGame.Scripts.Infrastructure.Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        public PlayerStaticData PlayerStaticData { get; private set; }

        public StaticDataService(IAsset asset)
        {
            PlayerStaticData = asset.GetStaticData<PlayerStaticData>(AssetPath.PlayerStaticData);
        }
    }
}