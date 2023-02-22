namespace BookingApp.Models.Rooms
{
    public class DoubleBed : Room
    {
        private const int BedCapacityOfDoubleBed = 2;
        public DoubleBed() : base(BedCapacityOfDoubleBed)
        {
        }

    }
}
