namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double CurrAvailableFuel = 80;
        private const double CurrFuelConsumptionPerRace = 10;

        public SuperCar(string make, string model, string vin, int horsePower) 
            : base(make, model, vin, horsePower, CurrAvailableFuel, CurrFuelConsumptionPerRace)
        {
        }
    }
}
