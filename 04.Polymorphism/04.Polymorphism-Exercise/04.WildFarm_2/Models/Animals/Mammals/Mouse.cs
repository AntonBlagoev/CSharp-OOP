using System.Collections.Generic;
using System;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals.Mammals
{
    internal class Mouse : Mammal
    {
        private const double MouseWeightMultiplier = 0.1;

        public Mouse(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        protected override double WeightMultiplier => MouseWeightMultiplier;
        protected override IReadOnlyCollection<Type> PreferredFoods
            => new List<Type> { typeof(Fruit), typeof(Vegetable) }.AsReadOnly();

        public override string ProduceSound()
        {
            return "Squeak";
        }
    }
}
