namespace Raiding.Factories.Interfaces
{
    using Models;
    public interface IHeroesFactory
    {
        public BaseHero CreateHero(string name, string type);
    }
}
