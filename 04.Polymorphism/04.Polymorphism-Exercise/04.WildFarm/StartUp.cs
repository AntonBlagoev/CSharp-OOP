namespace WildFarm
{
    using Core;
    using IO;
    using IO.Interfaces;
    using Factories.Interfaces;
    using Factories;

    internal class StartUp
    {
        static void Main(string[] args)
        {
            IReader reader = new ConsoleReader();
            IWriter writer = new ConsoleWriter();

            IAnimalFactory animalFactory = new AnimalFactory();
            IFoodFactory foodFactory = new FoodFactory();

            Engine engine = new Engine(reader, writer, animalFactory, foodFactory);
            engine.Run();
        }
    }
}
