using System;
using System.Collections.Generic;
using System.Text;

namespace _04.PizzaCalories
{
    public class Dough
    {
        private const int BaseCaloriesPerGram = 2;

        Dictionary<string, double> flourTypes = new Dictionary<string, double>
        { ["white"] = 1.5, ["wholegrain"] = 1.0 };

        Dictionary<string, double> bakingTechniques = new Dictionary<string, double>
        { ["crispy"] = 0.9, ["chewy"] = 1.1, ["homemade"] = 1.0 };

        private string flourType;
        private string bakingTechnique;
        private int weight;

        public Dough(string flourType, string bakingTechnique, int weight)
        {
            this.FlourType = flourType;
            this.BakingTechnique = bakingTechnique;
            this.Weight = weight;
        }

        private string FlourType
        {
            get
            {
                return this.flourType;
            }

            set
            {
                if (!flourTypes.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                this.flourType = value;
            }
        }

        private string BakingTechnique
        {
            get
            {
                return this.bakingTechnique;
            }

            set
            {
                if (!bakingTechniques.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                this.bakingTechnique = value;
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
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }

                this.weight = value;
            }
        }
        public double Calories()
        {
            double ftModifier = flourTypes[flourType.ToLower()];
            double btModifier = bakingTechniques[bakingTechnique.ToLower()];
            return BaseCaloriesPerGram * weight * ftModifier * btModifier;
        }
    }
}
