namespace PlanetWars.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Weapons.Contracts;

    public class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> models;
        public WeaponRepository()
        {
            this.models = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => this.models;

        public IWeapon FindByName(string name)
        {
            var searchedName = this.models.FirstOrDefault(x => x.GetType().Name == name);
            if (searchedName != null)
            {
                return searchedName;
            }
            return null;
        }
        public void AddItem(IWeapon model)
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
