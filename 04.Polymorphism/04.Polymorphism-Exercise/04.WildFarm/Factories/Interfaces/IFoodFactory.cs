namespace WildFarm.Factories.Interfaces
{
    using WildFarm.Models.Interfaces;
    public interface IFoodFactory
    {
        IFood CreateFood(string type, int quantity);
    }
}
