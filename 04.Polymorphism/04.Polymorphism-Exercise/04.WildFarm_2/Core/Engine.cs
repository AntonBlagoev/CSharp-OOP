
namespace WildFarm.Core
{
    using System;
    using System.Collections.Generic;

    using Exeptions;
    using Factories;
    using Models.Animals;
    using Models.Foods;
    public class Engine
    {
        private readonly AnimalFactory animalsFactory;
        private readonly FoodFactory foodFactory;

        private readonly List<Animal> animals;

        public Engine()
        {
            animalsFactory = new AnimalFactory();
            foodFactory = new FoodFactory();
            animals = new List<Animal>();
        }

        public void Run()
        {
            string command = string.Empty;
            while ((command = Console.ReadLine()) != "End")
            {
                string[] inputAnimal = command.Split(' ');
                string[] inputFood = Console.ReadLine().Split(' ');

                try
                {
                    Animal animal = BuildAnimalUsingFactory(inputAnimal);
                    Food food = foodFactory.CreateFood(inputFood[0], int.Parse(inputFood[1]));

                    Console.WriteLine(animal.ProduceSound());

                    this.animals.Add(animal);

                    animal.Eat(food);

                }
                catch (InvalidFactoryTypeException ifte)
                {
                    Console.WriteLine(ifte.Message);
                }
                catch (FoodNotPreferredException fnpe)
                {
                    Console.WriteLine(fnpe.Message);
                }
                catch (InvalidOperationException ioe)
                {
                    Console.WriteLine(ioe.Message);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal);
            }

        }

        private Animal BuildAnimalUsingFactory(string[] inputAnimal)
        {
            Animal animal;

            if (inputAnimal.Length == 4)
            {
                string type = inputAnimal[0];
                string name = inputAnimal[1];
                double weight = double.Parse(inputAnimal[2]);
                string thirdParam = inputAnimal[3];

                animal = animalsFactory.CreateAnimal(type, name, weight, thirdParam);
            }
            else if (inputAnimal.Length == 5)
            {
                string type = inputAnimal[0];
                string name = inputAnimal[1];
                double weight = double.Parse(inputAnimal[2]);
                string thirdParam = inputAnimal[3];
                string fourthParam = inputAnimal[4];

                animal = animalsFactory.CreateAnimal(type, name, weight, thirdParam, fourthParam);
            }
            else
            {
                throw new ArgumentException("Invalid input!");
            }
            return animal;
        }
    }
}
