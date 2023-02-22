using System.Collections.Generic;
using System;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals.Mammals
{
    internal class Dog : Mammal
    {
        private const double DogWeightMultiplier = 0.40;

        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        protected override double WeightMultiplier => DogWeightMultiplier;
        protected override IReadOnlyCollection<Type> PreferredFoods
            => new List<Type> { typeof(Meat) }.AsReadOnly();
        public override string ProduceSound()
        {
            return "Woof!";
        }
    }
}
