using System;

namespace WildFarm.Factories
{
    using Exceptions;
    using Interfaces;
    using Models.Animals;
    using Models.Interfaces;

    public class AnimalFactory : IAnimalFactory
    {
        public IAnimal CreateAnimal(string[] cmdArgs)
        {
            string type = cmdArgs[0];
            string name = cmdArgs[1];
            double weight = double.Parse(cmdArgs[2]);
            string fourthArg = cmdArgs[3];

            IAnimal animal;

            switch (type)
            {
                case "Owl":
                    animal = new Owl(name, weight, double.Parse(fourthArg));
                    break;
                case "Hen":
                    animal = new Hen(name, weight, double.Parse(fourthArg));
                    break;
                case "Mouse":
                    animal = new Mouse(name, weight, fourthArg);
                    break;
                case "Cat":
                    animal = new Cat(name, weight, fourthArg, cmdArgs[4]);
                    break;
                case "Dog":
                    animal = new Dog(name, weight, fourthArg);
                    break;
                case "Tiger":
                    animal = new Tiger(name, weight, fourthArg, cmdArgs[4]);
                    break;
                default:
                    throw new InvalidAnimalTypeException();
            }

            return animal;
        }
    }
}
