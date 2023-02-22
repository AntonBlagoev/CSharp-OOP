namespace Heroes.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Heroes.Models.Contracts;

    internal class WeaponRepository : IRepository<IWeapon>
    {
        private List<IWeapon> models;

        public WeaponRepository()
        {
            this.models = new List<IWeapon>();
        }
        public IReadOnlyCollection<IWeapon> Models => this.models.AsReadOnly();

        public void Add(IWeapon model) => this.models.Add(model);

        public IWeapon FindByName(string name)
        {
            IWeapon model = this.models.FirstOrDefault(x => x.Name == name);
            if (model != null)
            {
                return model;
            }
            return null;
        }

        public bool Remove(IWeapon model)
        {
            if (this.models.FirstOrDefault(x => x.Name == model.Name) != null)
            {
                models.Remove(model);
                return true;
            }
            return false;
        }
    }
}
