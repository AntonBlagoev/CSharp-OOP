using System.Collections.Generic;
using System;
using WildFarm.Models.Foods;

namespace WildFarm.Models.Animals.Mammals
{
    internal class Cat : Feline
    {
        private const double CatWeightMultiplier = 0.30;

        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }
        protected override double WeightMultiplier => CatWeightMultiplier;

        protected override IReadOnlyCollection<Type> PreferredFoods
            => new List<Type> { typeof(Vegetable), typeof(Meat) }.AsReadOnly();

        public override string ProduceSound()
        {
            return "Meow";
        }
    }
}
