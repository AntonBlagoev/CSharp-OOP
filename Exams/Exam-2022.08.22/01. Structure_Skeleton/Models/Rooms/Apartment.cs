namespace BookingApp.Models.Rooms
{
    public class Apartment : Room
    {
        private const int BedCapacityOfApartment = 6;
        public Apartment() : base(BedCapacityOfApartment)
        {
        }
    }
}

