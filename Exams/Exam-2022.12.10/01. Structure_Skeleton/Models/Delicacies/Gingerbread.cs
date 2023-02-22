namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy
    {
        private const double GignerbreadPrice = 4.0;
        public Gingerbread(string name) : base(GignerbreadPrice, name)
        {
        }
    }
}
