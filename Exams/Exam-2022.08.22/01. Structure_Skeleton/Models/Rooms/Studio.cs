using BookingApp.Models.Rooms.Contracts;

namespace BookingApp.Models.Rooms
{
    public class Studio : Room, IRoom
    {
        private const int BedCapacityOfStudio = 4;
        public Studio() : base(BedCapacityOfStudio)
        {
        }
    }
}
