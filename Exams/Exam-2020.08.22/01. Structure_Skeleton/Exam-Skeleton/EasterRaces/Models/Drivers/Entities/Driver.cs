namespace EasterRaces.Models.Drivers.Entities
{
    using System;

    using Cars.Contracts;
    using Drivers.Contracts;
    using Utilities.Messages;
    public class Driver : IDriver
    {
        private string name;

        public Driver(string name)
        {
            Name = name;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidName, name, 5));
                }
                name = value;
            }
        }

        public ICar Car { get; private set; }

        public int NumberOfWins { get; private set; }

        public bool CanParticipate => Car != null;

        public void AddCar(ICar car)
        {
            if (car == null)
            {
                throw new ArgumentNullException(string.Format(ExceptionMessages.CarInvalid));
            }

            Car = car;
        }

        public void WinRace()
        {
            NumberOfWins++;
        }
    }
}
