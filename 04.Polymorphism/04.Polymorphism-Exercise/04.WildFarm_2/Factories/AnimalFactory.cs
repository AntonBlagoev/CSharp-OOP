namespace WildFarm.Factories
{
    using System;

    using WildFarm.Models.Animals;
    using WildFarm.Models.Animals.Bird;
    using WildFarm.Models.Animals.Mammals;
    public class AnimalFactory
    {
        public Animal CreateAnimal(string type, string name, double weight, string thirdParam, string fourthParam = null)
        {
            Animal animal;

            switch (type)
            {
                case "Owl":
                    animal = new Owl(name, weight, double.Parse(thirdParam));
                    break;
                case "Hen":
                    animal = new Hen(name, weight, double.Parse(thirdParam));
                    break;
                case "Mouse":
                    animal = new Mouse(name, weight, thirdParam);
                    break;
                case "Cat":
                    animal = new Cat(name, weight, thirdParam, fourthParam);
                    break;
                case "Dog":
                    animal = new Dog(name, weight, thirdParam);
                    break;
                case "Tiger":
                    animal = new Tiger(name, weight, thirdParam, fourthParam);
                    break;
                default:
                    throw new ArgumentException("Invalid type!");
            }

            return animal;
        }
    }
}
