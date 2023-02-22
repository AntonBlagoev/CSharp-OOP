namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Delicacies.Contracts;
    public class DelicacyRepository : IRepository<IDelicacy>
    {
        private List<IDelicacy> models;
        public DelicacyRepository()
        {
            this.models = new List<IDelicacy>();
        }
        public IReadOnlyCollection<IDelicacy> Models => this.models;

        public void AddModel(IDelicacy model)
        {
            this.models.Add(model);
        }
    }
}
