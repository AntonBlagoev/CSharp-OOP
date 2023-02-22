using System;

namespace Vehicles.Models
{
    public class Truck : Vehicle
    {
        private const double TruckFuelConsumptionIncrement = 1.6;
        private const double RefuelingCoefficient = 0.95;

        public Truck(double fuelQuantity, double fuelConsumption, double tankCapacity) 
            : base(fuelQuantity, fuelConsumption, tankCapacity)
        {
        }
        protected override double FuelConsumptionModifier
            => TruckFuelConsumptionIncrement;
        public override void Refuel(double liters)
        {
            base.Refuel(liters);
            this.FuelQuantity -= (1 - RefuelingCoefficient) * liters;
        }
    }
}
