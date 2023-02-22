namespace Vehicles.Models.Interfaces
{
    public interface IVehicle
    {
        public double FuelQuantity { get;}
        public double FuelConsumption { get; }
        public double TankCapacity { get; }
        public string Drive(double distance, bool driveEmptyBus);
        public void Refuel(double fuelAmount);
    }
}
