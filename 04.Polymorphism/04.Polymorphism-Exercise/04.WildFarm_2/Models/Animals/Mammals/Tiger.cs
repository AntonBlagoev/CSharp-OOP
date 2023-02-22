using System.Collections.Generic;
using System;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals.Mammals
{
    internal class Tiger : Feline
    {
        private const double TigerWeightMultiplier = 1.0;

        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        protected override double WeightMultiplier => TigerWeightMultiplier;
        protected override IReadOnlyCollection<Type> PreferredFoods
            => new List<Type> { typeof(Meat) }.AsReadOnly();

        public override string ProduceSound()
        {
            return "ROAR!!!";
        }
    }
}
