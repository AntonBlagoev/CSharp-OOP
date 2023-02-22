namespace Gym.Models.Gyms
{
    public class BoxingGym : Gym
    {
        private const int CurrentCapacity = 15;
        public BoxingGym(string name) : base(name, CurrentCapacity)
        {
        }
    }
}
