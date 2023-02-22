using EasterRaces.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
namespace EasterRaces.Repositories.Entities
{
    public abstract class Repository<T> : IRepository<T>
    {
        private List<T> models;

        protected Repository()
        {
            models = new List<T>();
        }
        // public IReadOnlyCollection<T> Models => models;
        public void Add(T model)
        {
            models.Add(model);
        }
        public bool Remove(T model)
        {
            return models.Remove(model);
        }
        public abstract T GetByName(string name);

        public IReadOnlyCollection<T> GetAll()
        {
            return models.AsReadOnly();
        }



    }
}
