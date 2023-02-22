namespace Heroes.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Contracts;
    using Models.Heroes;
    using Models.Map;
    using Models.Weapons;
    using Repositories;
    using Repositories.Contracts;

    public class Controller : IController
    {
        private readonly IRepository<IHero> heroes;
        private readonly IRepository<IWeapon> weapons;

        public Controller()
        {
            this.heroes = new HeroRepository();
            this.weapons = new WeaponRepository();
        }
        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format($"The hero {name} already exists."));
            }

            IHero hero = type switch
            {
                nameof(Knight) => new Knight(name, health, armour),
                nameof(Barbarian) => new Barbarian(name, health, armour),
                _ => throw new InvalidOperationException(string.Format($"Invalid hero type."))
            };

            this.heroes.Add(hero);

            var heroAllias = type == nameof(Knight)
                ? $"Sir {hero.Name}"
                : $"{nameof(Barbarian)} {hero.Name}";

            return string.Format($"Successfully added {heroAllias} to the collection.");

            //if (heroes.Models.Any(x => x.Name == name))
            //    throw new InvalidOperationException($"The hero {name} already exists.");
            //if (type != "Knight" && type != "Barbarian")
            //    throw new InvalidOperationException("Invalid hero type.");

            //IHero hero;
            //string output;
            //if (type == "Knight")
            //{
            //    hero = new Knight(name, health, armour);
            //    output = $"Successfully added Sir {name} to the collection.";
            //}
            //else
            //{
            //    hero = new Barbarian(name, health, armour);
            //    output = $"Successfully added Barbarian {name} to the collection.";
            //}
            //heroes.Add(hero);
            //return output;
        }
        public string CreateWeapon(string type, string name, int durability)
        {
            if (weapons.FindByName(name) != null)
            {
                throw new InvalidOperationException(string.Format($"The weapon {name} already exists."));
            }

            IWeapon weapon = type switch
            {
                nameof(Mace) => new Mace(name, durability),
                nameof(Claymore) => new Claymore(name, durability),
                _ => throw new InvalidOperationException(string.Format($"Invalid weapon type."))
            };

            this.weapons.Add(weapon);

            return string.Format($"A {type.ToLower()} {name} is added to the collection.");

            //if (weapons.Models.Any(x => x.Name == name))
            //    throw new InvalidOperationException($"The weapon {name} already exists.");
            //if (type != "Claymore" && type != "Mace")
            //    throw new InvalidOperationException("Invalid weapon type.");

            //IWeapon weapon;
            //if (type == "Mace")
            //    weapon = new Mace(name, durability);
            //else
            //    weapon = new Claymore(name, durability);

            //weapons.Add(weapon);
            //return $"A {type.ToLower()} {name} is added to the collection.";
        }


        public string AddWeaponToHero(string weaponName, string heroName)
        {
            var hero = heroes.FindByName(heroName);
            var weapon = weapons.FindByName(weaponName);

            if (hero == null)
            {
                throw new InvalidOperationException(string.Format($"Hero {heroName} does not exist."));
            }
            if (weapon == null)
            {
                throw new InvalidOperationException(string.Format($"Weapon {weaponName} does not exist."));
            }
            if (hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format($"Hero {heroName} is well-armed."));
            }

            hero.AddWeapon(weapon);

            this.weapons.Remove(weapon);

            var weaponType = weapon.GetType().Name;

            return string.Format($"Hero {heroName} can participate in battle using a {weaponType.ToLower()}.");


            //IWeapon weapon = weapons.FindByName(weaponName);
            //IHero hero = heroes.FindByName(heroName);

            //if (hero == null)
            //    throw new InvalidOperationException($"Hero {heroName} does not exist.");
            //if (weapon == null)
            //    throw new InvalidOperationException($"Weapon {weaponName} does not exist.");
            //if (hero.Weapon != null)
            //    throw new InvalidOperationException($"Hero {heroName} is well-armed.");

            //hero.AddWeapon(weapon);
            //weapons.Remove(weapon);
            //return $"Hero {heroName} can participate in battle using a {weapon.GetType().Name.ToLower()}.";
        }

        public string StartBattle()
        {
            Map map = new Map();

            var heroesReadyToBattle = this.heroes.Models.Where(h => h.IsAlive && h.Weapon != null).ToList(); // !!!

            return map.Fight(heroesReadyToBattle);
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            var sortedHeroes = heroes.Models
                .OrderBy(x => x.GetType().Name)
                .ThenByDescending(x => x.Health)
                .ThenBy(x => x.Name);

            foreach (var hero in sortedHeroes)
            {
                sb.AppendLine(hero.ToString());

            }
            return sb.ToString().TrimEnd();
        }
    }
}
