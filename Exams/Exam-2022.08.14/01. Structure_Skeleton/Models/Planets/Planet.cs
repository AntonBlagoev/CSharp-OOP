namespace PlanetWars.Models.Planets
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    using MilitaryUnits.Contracts;
    using MilitaryUnits;
    using Planets.Contracts;
    using Utilities.Messages;
    using Weapons;
    using Weapons.Contracts;
    using Repositories;

    public class Planet : IPlanet
    {
        private string name;
        private double budget;

        private UnitRepository army;
        private WeaponRepository weapons;

        public Planet(string name, double budget)
        {
            this.Name = name;
            this.Budget = budget;
            this.army = new UnitRepository();
            this.weapons = new WeaponRepository();
        }
        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.InvalidPlanetName));
                }
                name = value;
            }
        }
        public double Budget
        {
            get => budget;
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value;
            }
        }
        public double MilitaryPower => Math.Round(this.TotalAmount(), 3);

        public IReadOnlyCollection<IMilitaryUnit> Army => this.army.Models;

        public IReadOnlyCollection<IWeapon> Weapons => this.weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            this.army.AddItem(unit);
        }
        public void AddWeapon(IWeapon weapon)
        {
            this.weapons.AddItem(weapon);
        }
        public void TrainArmy()
        {
            foreach (var item in this.Army)
            {
                item.IncreaseEndurance();
            }
        }
        public void Spend(double amount)
        {
            if (amount > this.budget)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnsufficientBudget));
            }
            this.Budget -= amount;
        }
        public void Profit(double amount)
        {
            this.Budget += amount;
        }
        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Planet: {this.Name}");
            sb.AppendLine($"--Budget: {this.Budget} billion QUID");

            sb.Append($"--Forces: ");
            if (army.Models.Count > 0)
            {
                var units = new List<string>();
                foreach (var unit in this.army.Models)
                {
                    units.Add(unit.GetType().Name);
                }
                sb.AppendLine(string.Join(", ", units));
            }
            else
            {
                sb.AppendLine("No units");
            }

            sb.Append($"--Combat equipment: ");
            if (weapons.Models.Count > 0)
            {
                var items = new List<string>();
                foreach (var item in this.weapons.Models)
                {
                    items.Add(item.GetType().Name);
                }
                sb.AppendLine(string.Join(", ", items));
            }
            else
            {
                sb.AppendLine("No weapons");
            }

            sb.AppendLine($"--Military Power: {this.MilitaryPower}");

            return sb.ToString().TrimEnd();
        }
        private double TotalAmount()
        {
            double totalAmount = this.army.Models.Sum(x => x.EnduranceLevel) + this.weapons.Models.Sum(x => x.DestructionLevel);

            if (this.army.Models.Any(x => x.GetType().Name == nameof(AnonymousImpactUnit)))
            {
                totalAmount *= 1.3;
            }
            if (this.weapons.Models.Any(x => x.GetType().Name == nameof(NuclearWeapon)))
            {
                totalAmount *= 1.45;
            }
            return totalAmount;
        }
    }
}
