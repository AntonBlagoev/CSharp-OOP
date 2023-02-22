namespace CarRacing.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Cars;
    using Models.Cars.Contracts;
    using Models.Maps;
    using Models.Racers;
    using Models.Racers.Contracts;
    using Repositories;
    using Repositories.Contracts;
    public class Controller : IController
    {
        private IRepository<ICar> cars;
        private IRepository<IRacer> racers;
        private Map map;
        public Controller()
        {
            this.cars = new CarRepository();
            this.racers = new RacerRepository();
            this.map = new Map();
        }

        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if (type != nameof(SuperCar) && type != nameof(TunedCar))
            {
                throw new ArgumentException("Invalid car type!");
            }

            ICar car;
            if (type == nameof(SuperCar))
            {
                car = new SuperCar(make, model, VIN, horsePower);
            }
            else
            {
                car = new TunedCar(make, model, VIN, horsePower);
            }
            cars.Add(car);

            return $"Successfully added car {make} {model} ({VIN}).";
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            ICar car = cars.FindBy(carVIN);
            if (car == null)
            {
                throw new ArgumentException("Car cannot be found!");
            }
            else if (type != nameof(ProfessionalRacer) && type != nameof(StreetRacer))
            {
                throw new ArgumentException("Invalid racer type!");
            }

            IRacer racer;
            if (type == nameof(ProfessionalRacer))
            {
                racer = new ProfessionalRacer(username, car);
            }
            else
            {
                racer = new StreetRacer(username, car);
            }
            racers.Add(racer);

            return $"Successfully added racer {username}.";
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = racers.FindBy(racerOneUsername);
            IRacer racerTwo = racers.FindBy(racerTwoUsername);

            if (racerOne == null)
            {
                throw new ArgumentException($"Racer {racerOneUsername} cannot be found!");
            }
            else if (racerTwo == null)
            {
                throw new ArgumentException($"Racer {racerTwoUsername} cannot be found!");
            }
            else
            {
                return map.StartRace(racerOne, racerTwo);
            }
        }

        public string Report()
        {
            var orderedRacers = racers.Models.OrderByDescending(x => x.DrivingExperience).ThenBy(x => x.Username);
            StringBuilder sb = new StringBuilder();

            foreach (var racer in orderedRacers)
            {
                sb.AppendLine($"{racer.GetType().Name}: {racer.ToString()}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
