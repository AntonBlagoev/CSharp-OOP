namespace Raiding.Models
{
    public class Druid : BaseHero
    {
        private const int DroidPpower = 80;
        public Druid(string name) : base(name, DroidPpower)
        {
        }
        public override string Name { get; set; }

        public override string CastAbility()
        {
            return $"{GetType().Name} - {this.Name} healed for {this.Power}";
        }
    }
}
