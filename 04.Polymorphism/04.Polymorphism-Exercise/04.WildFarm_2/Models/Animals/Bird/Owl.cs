using System.Collections.Generic;
using System;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals.Bird
{
    public class Owl : Bird
    {
        private const double OwlWeightMultiplier = 0.25;
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }
        protected override double WeightMultiplier => OwlWeightMultiplier;
        protected override IReadOnlyCollection<Type> PreferredFoods
            => new List<Type> { typeof(Meat) }.AsReadOnly();
        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }
    }
}
