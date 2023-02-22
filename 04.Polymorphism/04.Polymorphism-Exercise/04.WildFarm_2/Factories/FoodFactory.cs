namespace WildFarm.Factories
{
    using System;

    using WildFarm.Models.Foods;
    public class FoodFactory
    {

        public Food CreateFood(string type, int quantity)
        {
            Food food;

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
                    throw new ArgumentException("Invalid type!");
            }

            return food;
        }
    }
}
