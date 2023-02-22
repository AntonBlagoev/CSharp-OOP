using System;
using System.Collections.Generic;
using System.Text;

namespace _04.PizzaCalories
{
    public class Topping
    {
        private const int BaseCaloriesPerGram = 2;

        Dictionary<string, double> toppingTypes = new Dictionary<string, double>
        { ["meat"] = 1.2, ["veggies"] = 0.8, ["cheese"] = 1.1, ["sauce"] = 0.9 };

        private string toppingType;
        private int weight;

        public Topping(string toppingType, int weight)
        {
            this.ToppingType = toppingType;
            this.Weight = weight;
        }

        private string ToppingType
        {
            get
            {
                return this.toppingType;
            }

            set
            {
                if (!toppingTypes.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                this.toppingType = value;
            }
        }

        private int Weight
        {
            get
            {
                return this.weight;
            }

            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{this.ToppingType} weight should be in the range [1..50].");
                }

                this.weight = value;
            }
        }

        public double Calories()
        {
            double ttModifier = toppingTypes[toppingType.ToLower()];

            return BaseCaloriesPerGram * ttModifier * this.Weight;
        }
    }
}
