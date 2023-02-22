namespace Heroes.Models.Map
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Heroes;
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            //var knights = players.OfType<Knight>().Where(x => x.IsAlive).ToList();
            //var barbarians = players.OfType<Barbarian>().Where(x => x.IsAlive).ToList();

            var knights = new List<Knight>();
            var barbarians = new List<Barbarian>();

            foreach (var player in players)
            {
                if (player.IsAlive)
                {
                    if (player is Knight knight)
                    {
                        knights.Add(knight);
                    }
                    else if (player is Barbarian barbarian)
                    {
                        barbarians.Add(barbarian);
                    }
                    else
                    {
                        throw new InvalidOperationException("Not hero Player");
                    }
                }
            }

            var continueBattle = true;
            while (continueBattle)
            {
                var allKnightsAreDead = true;
                var allBarbariansAreDead = true;

                int aliveKnights = 0;
                int aliveBarbarians = 0;

                foreach (var knight in knights.Where(x => x.IsAlive && x.Weapon != null))
                {
                    if (knight.IsAlive)
                    {
                        allKnightsAreDead = false;
                        aliveKnights++;

                        foreach (var barbarian in barbarians.Where(x => x.IsAlive && x.Weapon != null))
                        {
                            var weaponDamage = knight.Weapon.DoDamage();
                            barbarian.TakeDamage(weaponDamage);
                        }
                    }
                }

                foreach (var barbarian in barbarians.Where(x => x.IsAlive && x.Weapon != null))
                {
                    if (barbarian.IsAlive)
                    {
                        allBarbariansAreDead = false;
                        aliveBarbarians++;

                        foreach (var knight in knights.Where(x => x.IsAlive && x.Weapon != null))
                        {
                            var weaponDamage = barbarian.Weapon.DoDamage();
                            knight.TakeDamage(weaponDamage);
                        }
                    }
                }

                if (allKnightsAreDead)
                {
                    var deadBarberians = barbarians.Count - aliveBarbarians;
                    return $"The barbarians took {deadBarberians} casualties but won the battle.";
                }
                if (allBarbariansAreDead)
                {
                    var deadKnights = knights.Count - aliveKnights;
                    return $"The knights took {deadKnights} casualties but won the battle.";
                }
            }
            throw new InvalidOperationException("The map logic has a bug!");
        }
    }
}
