namespace Formula1.Models
{
    using System;
    using System.Text;

    using Models.Contracts;
    using Utilities;
    public class Pilot : IPilot
    {
        private string fullName;
        private IFormulaOneCar car; 

        public Pilot(string fullName)
        {
            this.FullName = fullName;
        }

        public string FullName
        {
            get => fullName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPilot, value));
                }
                this.fullName = value;
            }
        }
        public bool CanRace { get; private set; } = false;
        public IFormulaOneCar Car
        {
            get => car;
            private set
            {
                if (value == null)
                {
                    throw new NullReferenceException(string.Format(ExceptionMessages.InvalidCarForPilot));
                }

                this.car = value;
            }
        }

        public int NumberOfWins { get; private set; }


        public void AddCar(IFormulaOneCar car)
        {
            this.Car = car;
            this.CanRace = true;
        }

        public void WinRace()
        {
            this.NumberOfWins++; ;
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {this.FullName} has {this.NumberOfWins} wins.");

            return sb.ToString().TrimEnd();
        }
    }
}
