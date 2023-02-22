using FrontDeskApp;
using NUnit.Framework;
using System;

namespace BookigApp.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void HotelCtor_SetsNameAndCategoryCorrectly()
        {
            var hotel = new Hotel("Kavaler", 3);

            string expectedName = "Kavaler";
            int expectedCategory = 3;
            var expectedTurnover = 0;

            Assert.That(hotel.FullName, Is.EqualTo(expectedName));
            Assert.That(hotel.Category, Is.EqualTo(expectedCategory));
            Assert.That(hotel.Turnover, Is.EqualTo(expectedTurnover));
        }

        [Test]
        public void HotelCtor_ThrowsExceptionForInvalidNameAndCategory()
        {
            Assert.Throws<ArgumentNullException>(() => new Hotel(" ", 5));
            Assert.Throws<ArgumentException>(() => new Hotel("NewHotel", 6));
            Assert.Throws<ArgumentException>(() => new Hotel("NewHotel", 0));
        }

        [Test]
        public void AddRoom_AddsRoomsCorrectly()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(3, 57);

            hotel.AddRoom(room);

            Assert.That(hotel.Rooms.Count, Is.EqualTo(1));
        }
        [Test]
        public void BookRoom_ThrowsForAdults()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(2, 53);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(0, 0, 3, 200));
        }

        [Test]
        public void BookRoom_ThrowsForChildren()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(3, 53);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, -1, 3, 200));
        }

        [Test]
        public void BookRoom_ThrowsForDuration()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(3, 53);
            hotel.AddRoom(room);

            Assert.Throws<ArgumentException>(() => hotel.BookRoom(2, 1, 0, 200));
        }

        [Test]
        public void BookRoom_NoBookingForNotEnoughBeds()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(3, 53);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 1, 2, 200);

            Assert.That(hotel.Turnover.Equals(0));
        }

        [Test]
        public void BookRoom_WorksProperly()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(5, 53);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 1, 2, 106);
            double expectedTurnover = 106;

            Assert.AreEqual(expectedTurnover, hotel.Turnover);
            Assert.That(hotel.Bookings.Count.Equals(1));
            Assert.That(hotel.Rooms.Count.Equals(1));
        }

        [Test]
        public void BookRoom_NoBookingIfTooLowBudget()
        {
            var hotel = new Hotel("HotelName", 5);
            var room = new Room(5, 53);
            hotel.AddRoom(room);

            hotel.BookRoom(4, 1, 2, 100);
            double expectedTurnover = 0;

            Assert.AreEqual(expectedTurnover, hotel.Turnover);
            Assert.That(hotel.Bookings.Count.Equals(0));
            Assert.That(hotel.Rooms.Count.Equals(1));
        }


    }
}