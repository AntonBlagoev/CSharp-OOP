namespace BookingApp.Repositories
{
    using System.Collections.Generic;
    using System.Linq;

    using BookingApp.Models.Rooms.Contracts;
    using Contracts;

    public class RoomRepository : IRepository<IRoom>
    {
        private List<IRoom> rooms;
        public RoomRepository()
        {
            this.rooms = new List<IRoom>();
        }
      
        public void AddNew(IRoom room)
        {
            this.rooms.Add(room);
        }

        public IRoom Select(string roomTypeName)
            => this.rooms.FirstOrDefault(r => r.GetType().Name == roomTypeName);


        public IReadOnlyCollection<IRoom> All() => this.rooms;

    }

}
