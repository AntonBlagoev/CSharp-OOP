namespace FoodShortage.Models.Interfaces
{
    public interface IBuyer
    {
        string Name { get; }
        public int Food { get; }
        public void BuyFood();
    }
}
