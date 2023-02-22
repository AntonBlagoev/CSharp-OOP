namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Booths.Contracts;
    internal class BoothRepository : IRepository<IBooth>
    {
        private List<IBooth> models;
        public BoothRepository()
        {
            this.models = new List<IBooth>();
        }
        public IReadOnlyCollection<IBooth> Models => this.models;

        public void AddModel(IBooth model)
        {
            this.models.Add(model);
        }
    }
}
