namespace SpaceStation.Models.Mission
{
    using System.Linq;
    using System.Collections.Generic;

    using Astronauts.Contracts;
    using Contracts;
    using Planets.Contracts;
    public class Mission : IMission
    {

        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (var astronaut in astronauts)
            {              
                while (astronaut.CanBreath && planet.Items.Any())
                {
                    var item = planet.Items.FirstOrDefault();
                    astronaut.Bag.Items.Add(item);
                    planet.Items.Remove(item);
                    astronaut.Breath();
                }

                if (!planet.Items.Any())
                {
                    break;
                }
            }
        }
    }
}
