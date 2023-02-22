namespace EasterRaces.Repositories.Entities
{
    using System.Linq;

    using Models.Races.Contracts;
    public class RaceRepository : Repository<IRace>
    {
        public override IRace GetByName(string name)
        {
            return GetAll().FirstOrDefault(x => x.Name == name);
        }
    }
}
