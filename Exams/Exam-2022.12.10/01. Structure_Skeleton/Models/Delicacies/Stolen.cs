namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen : Delicacy
    {
        private const double StolenPrice = 3.5;
        public Stolen(string name) : base(StolenPrice, name)
        {
        }
    }
}
