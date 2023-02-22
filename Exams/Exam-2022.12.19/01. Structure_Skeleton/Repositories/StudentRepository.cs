using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityCompetition.Models.Contracts;
using UniversityCompetition.Repositories.Contracts;

namespace UniversityCompetition.Repositories
{
    public class StudentRepository : IRepository<IStudent>
    {
        private List<IStudent> models;

        public StudentRepository()
        {
            this.models = new List<IStudent>();
        }
        public IReadOnlyCollection<IStudent> Models => this.models.AsReadOnly();

        public void AddModel(IStudent model)
        {
            models.Add(model);
        }

        public IStudent FindById(int id)
        {
            return models.FirstOrDefault(x => x.Id == id);
        }

        public IStudent FindByName(string name)
        {
            string[] input = name.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string firstName = input[0];    
            string lastName = input[1];

            return models.FirstOrDefault(x => x.FirstName == firstName && x.LastName == lastName);
        }
    }
}
