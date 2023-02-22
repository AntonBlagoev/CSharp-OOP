namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Hotels.Contacts;
    public class HotelRepository : IRepository<IHotel>
    {
        private List<IHotel> hotels;

        public HotelRepository()
        {
            this.hotels = new List<IHotel>();
        }
        public void AddNew(IHotel hotel)
        {
            this.hotels.Add(hotel);
        }

        public IHotel Select(string hotelName)
            => this.hotels.FirstOrDefault(h => h.FullName == hotelName);

        public IReadOnlyCollection<IHotel> All() => this.hotels;

    }
}
