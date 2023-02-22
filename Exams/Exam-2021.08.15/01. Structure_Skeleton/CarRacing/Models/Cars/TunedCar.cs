using System;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double CurrAvailableFuel = 65;
        private const double CurrFuelConsumptionPerRace = 7.5;
        public TunedCar(string make, string model, string vin, int horsePower) 
            : base(make, model, vin, horsePower, CurrAvailableFuel, CurrFuelConsumptionPerRace)
        {
        }
        public override void Drive()
        {
            base.Drive();
            //this.HorsePower -= (int)(this.HorsePower * 0.03);
            this.HorsePower = (int)Math.Round(this.HorsePower * 0.97);
        }
    }
}
