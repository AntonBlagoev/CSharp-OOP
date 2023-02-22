namespace Gym.Models.Gyms
{
    public class WeightliftingGym : Gym
    {
        private const int CurrentCapacity = 20;
        public WeightliftingGym(string name) : base(name, CurrentCapacity)
        {
        }
    }
}
