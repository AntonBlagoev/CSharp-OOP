namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using Contracts;
    using Models.Bookings.Contracts;
    public class BookingRepository : IRepository<IBooking>
    {
        private List<IBooking> bookings;
        public BookingRepository()
        {
            this.bookings = new List<IBooking>();
        }
        public void AddNew(IBooking booking)
        {
            this.bookings.Add(booking);
        }

        public IBooking Select(string bookingNumberToString)
           => this.bookings.FirstOrDefault(b => b.BookingNumber.ToString() == bookingNumberToString);

        public IReadOnlyCollection<IBooking> All() => this.bookings;

    }
}
