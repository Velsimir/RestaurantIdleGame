namespace MainGame.Scripts.Logic.PlayerLogic
{
    public interface IGiveble
    {
        public ITakable GetObject();

        bool HasObjects { get; }
    }
}