namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Planets.Contracts;

    public class PlanetRepository : IRepository<IPlanet>
    {
        private List<IPlanet> models;
        public PlanetRepository()
        {
            this.models = new List<IPlanet>();
        }
        public IReadOnlyCollection<IPlanet> Models => this.models;

        public IPlanet FindByName(string name) => this.models.FirstOrDefault(x => x.Name == name);
        public void AddItem(IPlanet model)
        {
            this.models.Add(model);
        }

        public bool RemoveItem(string name)
        {
            var searchedName = this.models.FirstOrDefault(x => x.Name == name);
            if (searchedName != null)
            {
                this.models.Remove(searchedName);
                return true;
            }
            return false;
        }
    }
}
