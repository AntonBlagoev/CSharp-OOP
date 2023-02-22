namespace Heroes.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Contracts;
    public class HeroRepository : IRepository<IHero>
    {
        private List<IHero> models;

        public HeroRepository()
        {
            this.models = new List<IHero>();
        }

        public IReadOnlyCollection<IHero> Models => this.models.AsReadOnly();

        public void Add(IHero model) => this.models.Add(model);

        public IHero FindByName(string name)
        {
            IHero model = this.models.FirstOrDefault(x => x.Name == name);
            if (model != null)
            {
                return model;
            }
            return null;
        }

        public bool Remove(IHero model)
        {
            if (this.models.FirstOrDefault(x => x.Name == model.Name) != null)
            {
                this.models.Remove(model);
                return true;
            }
            return false;
        }
    }
}
