namespace Raiding.Core
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Interfaces;
    using Factories;
    using IO.Interfaces;
    using Models;
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;

        private readonly HeroesFactory heroesFactory;
        private readonly List<BaseHero> heroes;
        public Engine()
        {
            heroesFactory = new HeroesFactory();
            heroes = new List<BaseHero>();
        }
        public Engine(IReader reader, IWriter writer) : this()
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            int n = int.Parse(this.reader.ReadLine());
            while (heroes.Count < n)
            {
                string heroName = this.reader.ReadLine();
                string heroType = this.reader.ReadLine();

                try
                {
                    BaseHero hero = heroesFactory.CreateHero(heroName, heroType);
                    heroes.Add(hero);
                }
                catch (Exception ex)
                {
                    this.writer.WriteLine(ex.Message);
                }
            }

            double bossPower = double.Parse(this.reader.ReadLine());

            foreach (var hero in heroes)
            {
                this.writer.WriteLine(hero.CastAbility());
            }

            int heroesPower = heroes.Sum(x => x.Power);
            if (heroesPower >= bossPower)
            {
                this.writer.WriteLine("Victory!");
            }
            else
            {
                this.writer.WriteLine("Defeat...");
            }
        }
    }
}
