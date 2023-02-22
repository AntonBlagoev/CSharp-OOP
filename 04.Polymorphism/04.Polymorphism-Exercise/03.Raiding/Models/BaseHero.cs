
using Raiding.Models.Interfaces;

namespace Raiding.Models
{
    public abstract class BaseHero : IBaseHero
    {
        protected BaseHero(string name, int power)
        {
            Name = name;
            Power = power;
        }
        public abstract string Name { get; set; }
        public virtual int Power { get; set; }
        public abstract string CastAbility();
    }
}
