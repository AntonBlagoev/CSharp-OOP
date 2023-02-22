using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Repositories;
using Easter.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Utilities.Messages;

namespace Easter.Core
{
    public class Controller : IController
    {
        private IRepository<IBunny> bunnies;
        private IRepository<IEgg> eggs;
        private Workshop workshop;
        public Controller()
        {
            this.bunnies = new BunnyRepository();
            this.eggs = new EggRepository();
            this.workshop = new Workshop();
        }
        public string AddBunny(string bunnyType, string bunnyName)
        {
            if (bunnyType != nameof(HappyBunny) && bunnyType != nameof(SleepyBunny))
            {
                throw new InvalidOperationException(string.Format("Invalid bunny type."));
            }
            IBunny bunny;
            if (bunnyType == nameof(HappyBunny))
            {
                bunny = new HappyBunny(bunnyName);
            }
            else
            {
                bunny = new SleepyBunny(bunnyName);
            }

            bunnies.Add(bunny);
            return string.Format($"Successfully added {bunnyType} named {bunnyName}.");

        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            IDye dye = new Dye(power);
            if (bunny == null)
            {
                throw new InvalidOperationException(string.Format("The bunny you want to add a dye to doesn't exist!"));
            }
            bunny.AddDye(dye);
            return string.Format($"Successfully added dye with power {power} to bunny {bunnyName}!");
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName, energyRequired);
            eggs.Add(egg);
            return string.Format($"Successfully added egg: {eggName}!");
        }

        public string ColorEgg(string eggName)
        {
            var sortedBunnies = bunnies.Models.OrderByDescending(x => x.Energy).TakeWhile(x => x.Energy >= 50);
            if (!sortedBunnies.Any())
            {
                throw new InvalidOperationException(string.Format("There is no bunny ready to start coloring!"));
            }
            var egg = eggs.FindByName(eggName);

            foreach (var bunny in sortedBunnies)
            {
                workshop.Color(egg, bunny);

                if (bunny.Energy == 0)
                {
                    bunnies.Remove(bunny);
                }
            }

            return string.Format($"Egg {eggName} is {(egg.IsDone() ? "done" : "not done")}.");
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{eggs.Models.Count(x => x.IsDone())} eggs are done!");
            sb.AppendLine($"Bunnies info:");

            foreach (var bunny in bunnies.Models)
            {
                sb.AppendLine(bunny.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
