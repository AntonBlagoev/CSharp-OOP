namespace PlanetWars.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Core.Contracts;
    using Models.MilitaryUnits;
    using Models.MilitaryUnits.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Models.Weapons;
    using Models.Weapons.Contracts;
    using Repositories;
    using Repositories.Contracts;
    using Utilities.Messages;

    public class Controller : IController
    {
        private readonly IRepository<IPlanet> planets;
        public Controller()
        {
            this.planets = new PlanetRepository();
        }

        public string CreatePlanet(string name, double budget)
        {
            IPlanet planet = new Planet(name, budget);
            if (planets.FindByName(name) != null)
            {
                return string.Format(OutputMessages.ExistingPlanet, name);
            }
            planets.AddItem(planet);
            return string.Format(OutputMessages.NewPlanet, name);
        }

        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Any(x => x.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit army;
            if (unitTypeName == nameof(AnonymousImpactUnit))
            {
                army = new AnonymousImpactUnit();
            }
            else if (unitTypeName == nameof(SpaceForces))
            { 
                army = new SpaceForces();
            }
            else if (unitTypeName == nameof(StormTroopers))
            {
                army = new StormTroopers();
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }
            
            planet.Spend(army.Cost);
            planet.AddUnit(army);
            return string.Format(OutputMessages.UnitAdded, unitTypeName, planetName);
        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null) // default
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Weapons.Any(x => x.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon;
            if (weaponTypeName == nameof(BioChemicalWeapon))
            {
                weapon = new BioChemicalWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(NuclearWeapon))
            {
                weapon = new NuclearWeapon(destructionLevel);
            }
            else if (weaponTypeName == nameof(SpaceMissiles))
            {
                weapon = new SpaceMissiles(destructionLevel);
            }
            else
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            planet.AddWeapon(weapon);
            planet.Spend(weapon.Price);
            return string.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);

        }

        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (planet.Army.Count == 0)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }

            planet.Spend(1.25);
            planet.TrainArmy();
            return string.Format(OutputMessages.ForcesUpgraded, planetName);
        }
        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet planet1 = planets.FindByName(planetOne);
            IPlanet planet2 = planets.FindByName(planetTwo);

            bool planet1ContainsNuclearWeapon = planet1.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));
            bool planet2ContainsNuclearWeapon = planet2.Weapons.Any(x => x.GetType().Name == nameof(NuclearWeapon));

            IPlanet winningPlanet;
            IPlanet losingPlanet;

            if (planet1.MilitaryPower > planet2.MilitaryPower)
            {
                winningPlanet = planet1;
                losingPlanet = planet2;
            }
            else if (planet1.MilitaryPower < planet2.MilitaryPower)
            {
                winningPlanet = planet2;
                losingPlanet = planet1;
            }
            else if ((planet1ContainsNuclearWeapon && planet2ContainsNuclearWeapon) || (!planet1ContainsNuclearWeapon && !planet2ContainsNuclearWeapon))
            {
                planet1.Spend(planet1.Budget / 2);
                planet2.Spend(planet2.Budget / 2);

                return string.Format(OutputMessages.NoWinner);
            }
            else if (planet1ContainsNuclearWeapon)
            {
                winningPlanet = planet1;
                losingPlanet = planet2;
            }
            else 
            {
                winningPlanet = planet2;
                losingPlanet = planet1;
            }

            winningPlanet.Spend(winningPlanet.Budget / 2);
            winningPlanet.Profit(losingPlanet.Budget / 2);

            winningPlanet.Profit(losingPlanet.Army.Sum(x => x.Cost));
            winningPlanet.Profit(losingPlanet.Weapons.Sum(x => x.Price));

            planets.RemoveItem(losingPlanet.Name);

            return string.Format(OutputMessages.WinnigTheWar, winningPlanet.Name, losingPlanet.Name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");

            foreach (var planet in this.planets.Models.OrderByDescending(x => x.MilitaryPower).ThenBy(x => x.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }


       
    }
}
