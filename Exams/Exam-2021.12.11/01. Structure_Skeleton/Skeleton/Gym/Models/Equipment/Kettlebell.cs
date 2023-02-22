namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const double WeightsConst = 10000;
        private const decimal PriceConst = 80;
        public Kettlebell() : base(WeightsConst, PriceConst)
        {
        }
    }
}
