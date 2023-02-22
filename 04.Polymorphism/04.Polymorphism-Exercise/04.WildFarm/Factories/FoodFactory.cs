namespace WildFarm.Factories
{
    using Exceptions;
    using Interfaces;
    using Models.Foods;
    using Models.Interfaces;

    public class FoodFactory : IFoodFactory
    {

        public IFood CreateFood(string type, int quantity)
        {
            IFood food;

            switch (type)
            {
                case "Fruit":
                    food = new Fruit(quantity);
                    break;
                case "Meat":
                    food = new Meat(quantity);
                    break;
                case "Seeds":
                    food = new Seeds(quantity);
                    break;
                case "Vegetable":
                    food = new Vegetable(quantity);
                    break;
                default:
                    throw new InvalidFoodTypeException();
            }

            return food;
        }
    }
}
