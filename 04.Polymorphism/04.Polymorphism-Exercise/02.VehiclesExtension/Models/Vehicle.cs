namespace Vehicles.Models
{
    using Interfaces;
    using System;

    public abstract class Vehicle : IVehicle
    {
        public Vehicle(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            this.TankCapacity = tankCapacity;
            this.FuelQuantity = fuelQuantity > tankCapacity ? 0 : fuelQuantity;
            this.FuelConsumption = fuelConsumption;
        }
        public double FuelQuantity { get; protected set; }
        public double FuelConsumption { get; private set; }
        public double TankCapacity { get; private set; }
        protected virtual double FuelConsumptionModifier { get; }
        public string Drive(double distance, bool driveEmptyBus)
        {
            if (driveEmptyBus && this.GetType().Name == "Bus")
            {
                this.FuelConsumption -= 1.4;
            }
            double fuelNeeded = distance * (this.FuelConsumption + this.FuelConsumptionModifier);
            if (fuelNeeded > this.FuelQuantity)
            {
                return $"{this.GetType().Name} needs refueling";
            }
            this.FuelQuantity -= fuelNeeded;
            return $"{this.GetType().Name} travelled {distance} km";

        }

        public virtual void Refuel(double liters)
        {
            if (liters <= 0)
            {
                throw new ArgumentException($"Fuel must be a positive number");
            }
            if (liters + this.FuelQuantity > this.TankCapacity)
            {
                throw new ArgumentException($"Cannot fit {liters} fuel in the tank");
            }
            this.FuelQuantity += liters;
        }
        public override string ToString()
        {
            return $"{GetType().Name}: {this.FuelQuantity:f2}";
        }
    }
}
