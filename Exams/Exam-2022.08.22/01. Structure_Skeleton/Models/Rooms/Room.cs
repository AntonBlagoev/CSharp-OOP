namespace BookingApp.Models.Rooms
{
    using System;

    using Contracts;
    using Utilities.Messages;
    public abstract class Room : IRoom
    {
        private int bedCapacity;

        private double pricePerNight = 0;
        protected Room(int bedCapacity)
        {
            this.bedCapacity = bedCapacity;
        }
        public int BedCapacity => this.bedCapacity; // !!!

        public double PricePerNight
        {
            get { return this.pricePerNight; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(string.Format(ExceptionMessages.PricePerNightNegative));
                }
                this.pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            this.PricePerNight = price;
        }

    }
}
