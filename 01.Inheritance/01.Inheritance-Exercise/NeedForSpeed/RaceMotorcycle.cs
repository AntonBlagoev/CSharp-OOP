using System;
using System.Collections.Generic;
using System.Text;

namespace NeedForSpeed
{
    public class RaceMotorcycle : Motorcycle
    {
        public RaceMotorcycle(int horsePower, double fuel) : base(horsePower, fuel)
        {
        }
        public double DefaultFuelConsumption = 8;
        public override double FuelConsumption => DefaultFuelConsumption;
    }
}
