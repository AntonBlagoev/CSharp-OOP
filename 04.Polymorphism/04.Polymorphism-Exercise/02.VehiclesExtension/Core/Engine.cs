namespace Vehicles.Core
{
    using System;

    using Interfaces;
    using IO;
    using IO.Interfaces;
    using Models;

    public class Engine : IEngine
    {
        IReader reader = new ConsoleReader();
        IWriter writer = new ConsoleWriter();

        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            string[] carArgs = this.reader.ReadLine().Split(' ');
            string[] truckArgs = this.reader.ReadLine().Split(' ');
            string[] busArgs = this.reader.ReadLine().Split(' ');

            Vehicle car = new Car(double.Parse(carArgs[1]), double.Parse(carArgs[2]), double.Parse(carArgs[3]));
            Vehicle truck = new Truck(double.Parse(truckArgs[1]), double.Parse(truckArgs[2]), double.Parse(truckArgs[3]));
            Vehicle bus = new Bus(double.Parse(busArgs[1]), double.Parse(busArgs[2]), double.Parse(busArgs[3]));


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
                else if (type == "Truck")
                {
                    Command(truck, command, value);
                }
                else if (type == "Bus")
                {
                    Command(bus, command, value);
                }
            }

            this.writer.WriteLine(car.ToString());
            this.writer.WriteLine(truck.ToString());
            this.writer.WriteLine(bus.ToString());
        }
        private void Command(Vehicle vehicle, string command, double value)
        {
            if (command == "Drive")
            {
                this.writer.WriteLine(vehicle.Drive(value, false));
            }
            else if (command == "DriveEmpty")
            {
                this.writer.WriteLine(vehicle.Drive(value, true));
            }
            else if (command == "Refuel")
            {

                try
                {
                    vehicle.Refuel(value);
                }
                catch (ArgumentException ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }

        }
    }
}
