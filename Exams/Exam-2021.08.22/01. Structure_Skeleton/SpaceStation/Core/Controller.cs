namespace SpaceStation.Core
{
    using System;
    using System.Linq;
    using System.Text;

    using Contracts;
    using Models.Astronauts;
    using Models.Astronauts.Contracts;
    using Models.Mission;
    using Models.Mission.Contracts;
    using Models.Planets;
    using Models.Planets.Contracts;
    using Repositories;
    using Repositories.Contracts;
    public class Controller : IController
    {
        private readonly IRepository<IAstronaut> astronautRepo;
        private readonly IRepository<IPlanet> planetRepo;
        private readonly IMission mission;
        private int exploredPlanetsCount;

        public Controller()
        {
            this.astronautRepo = new AstronautRepository();
            this.planetRepo = new PlanetRepository();
            this.mission = new Mission();
        }
        public string AddAstronaut(string type, string astronautName)
        {
            IAstronaut astronaut;
            if (type == nameof(Biologist))
            {
                astronaut = new Biologist(astronautName);
            }
            else if (type == nameof(Geodesist))
            {
                astronaut = new Geodesist(astronautName);
            }
            else if (type == nameof(Meteorologist))
            {
                astronaut = new Meteorologist(astronautName);
            }
            else
            {
                throw new InvalidOperationException(string.Format($"Astronaut type doesn't exists!"));
            }
            this.astronautRepo.Add(astronaut);
            return $"Successfully added {type}: {astronautName}!";
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach (var item in items)
            {
                planet.Items.Add(item);
            }
            planetRepo.Add(planet);
            return $"Successfully added Planet: {planetName}!";
        }
        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronautRepo.FindByName(astronautName);
            if (astronaut == null)
            {
                throw new InvalidOperationException(string.Format($"Astronaut {astronautName} doesn't exists!"));
            }
            astronautRepo.Remove(astronaut);
            return $"Astronaut {astronautName} was retired!";

        }
        public string ExplorePlanet(string planetName)
        {

            var planet = planetRepo.FindByName(planetName);
            var astronauts = astronautRepo.Models.Where(x => x.Oxygen > 60).ToList();
            if (!astronauts.Any())
            {
                throw new InvalidOperationException(string.Format("You need at least one astronaut to explore the planet"));
            }
            this.mission.Explore(planet, astronauts);
            exploredPlanetsCount++;
            var deadAstronauts = astronautRepo.Models.Where(x => x.CanBreath == false).ToList();

            return $"Planet: {planetName} was explored! Exploration finished with {deadAstronauts.Count} dead astronauts!";

        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"{exploredPlanetsCount} planets were explored!");
            sb.AppendLine($"Astronauts info:");
            foreach (var item in astronautRepo.Models)
            {
                sb.AppendLine(item.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
