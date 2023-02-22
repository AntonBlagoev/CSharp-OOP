namespace Vehicles.Models
{
    using Interfaces;

    public abstract class Vehicle : IVehicle
    {
        protected Vehicle(double fuelQuantity, double fuelConsumption,
            double fuelConsumptionIncrement)
        {
            this.FuelQuantity = fuelQuantity;
            this.FuelConsumption = fuelConsumption + fuelConsumptionIncrement;
        }
        public double FuelQuantity { get; private set; }
        public double FuelConsumption { get; private set; }
        public string Drive(double distance)
        {
            double neededFuel = this.FuelConsumption * distance;
            if (neededFuel > this.FuelQuantity)
            {
                return $"{this.GetType().Name} needs refueling";
            }
            this.FuelQuantity -= neededFuel;
            return $"{this.GetType().Name} travelled {distance} km";
        }

        public virtual void Refuel(double liters)
        {
            this.FuelQuantity += liters;
        }
        public override string ToString()
        {
            return $"{GetType().Name}: {this.FuelQuantity:f2}"; 
        }
    }
}
