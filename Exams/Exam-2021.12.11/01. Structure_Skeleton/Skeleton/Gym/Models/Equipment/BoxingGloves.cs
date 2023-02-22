namespace Gym.Models.Equipment
{
    public class BoxingGloves : Equipment
    {
        private const double WeightsConst = 227;
        private const decimal PriceConst = 120;

        public BoxingGloves() : base(WeightsConst, PriceConst)
        {
        }
    }
}
