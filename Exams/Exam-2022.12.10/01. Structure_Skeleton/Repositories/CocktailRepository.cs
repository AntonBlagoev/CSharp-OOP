namespace ChristmasPastryShop.Repositories
{
    using System.Collections.Generic;

    using Contracts;
    using Models.Cocktails.Contracts;
    public class CocktailRepository : IRepository<ICocktail>
    {
        private List<ICocktail> models;
        public CocktailRepository()
        {
            this.models = new List<ICocktail>();
        }
        public IReadOnlyCollection<ICocktail> Models => this.models;

        public void AddModel(ICocktail model)
        {
            this.models.Add(model);
        }
    }
}
