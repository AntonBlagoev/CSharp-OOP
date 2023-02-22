namespace Vehicles.Core
{
    using Interfaces;
    using Vehicles.IO.Interfaces;
    using Vehicles.Models;

    internal class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] carArgs = this.reader.ReadLine().Split(' ');
            string[] trackArgs = this.reader.ReadLine().Split(' ');

            Vehicle car = new Car(double.Parse(carArgs[1]), double.Parse(carArgs[2]));
            Vehicle truck = new Truck(double.Parse(trackArgs[1]), double.Parse(trackArgs[2]));

            int n = int.Parse(this.reader.ReadLine());
            for (int i = 0; i < n; i++)
            {
                string[] tokens = this.reader.ReadLine().Split(' ');

                string type = tokens[1];
                string command = tokens[0];
                double value = double.Parse(tokens[2]);

                if (type == "Car")
                {
                    Command(car, command, value);
                }
                else
                {
                    Command(truck, command, value);
                }
            }

            this.writer.WriteLine(car.ToString());
            this.writer.WriteLine(truck.ToString());
        }

        private void Command(Vehicle vehicle, string command, double value)
        {
            if (command == "Drive")
            {
                this.writer.WriteLine(vehicle.Drive(value));
            }
            else if (command == "Refuel")
            {
                vehicle.Refuel(value);
            }

        }
    }
}
