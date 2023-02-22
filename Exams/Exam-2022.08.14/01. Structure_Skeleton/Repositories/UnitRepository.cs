namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.MilitaryUnits.Contracts;

    public class UnitRepository : IRepository<IMilitaryUnit>
    {
        private List<IMilitaryUnit> models;
        public UnitRepository()
        {
            this.models = new List<IMilitaryUnit>();
        }
        public IReadOnlyCollection<IMilitaryUnit> Models => this.models;

        public IMilitaryUnit FindByName(string name)
        {
            var searchedName = this.models.FirstOrDefault(x => x.GetType().Name == name);
            if (searchedName != null)
            {
                return searchedName;
            }
            return null;
        }
        public void AddItem(IMilitaryUnit model)
        {
            this.models.Add(model);
        }


        public bool RemoveItem(string name)
        {
            var searchedName = this.models.FirstOrDefault(x => x.GetType().Name == name);
            if (searchedName != null)
            {
                this.models.Remove(searchedName);
                return true;
            }
            return false;
        }
    }
}
