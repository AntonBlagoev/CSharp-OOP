namespace NavalVessels.Repositories
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Models.Contracts;
    using Repositories.Contracts;
    public class VesselRepository : IRepository<IVessel>
    {
        private List<IVessel> vessels;

        public VesselRepository()
        {
            this.vessels = new List<IVessel>();
        }
        public IReadOnlyCollection<IVessel> Models => this.vessels.AsReadOnly();

        public void Add(IVessel model)
        {
            this.vessels.Add(model);
        }

        public IVessel FindByName(string name)
        {
            return this.vessels.FirstOrDefault(x => x.Name == name);
        }

        public bool Remove(IVessel model)
        {
            return this.vessels.Remove(model);
        }
    }

}
