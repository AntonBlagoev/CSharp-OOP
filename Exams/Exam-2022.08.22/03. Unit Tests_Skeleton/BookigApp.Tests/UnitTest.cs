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
        public void Test_ConstructorWithValidData()
        {
            Hotel hotel = new Hotel("Leon", 5);

            string expectedName = "Leon";
            int expectedCategory = 5;

            Assert.AreEqual(expectedName, hotel.FullName);
            Assert.AreEqual(expectedCategory, hotel.Category);
            Assert.AreEqual(0, hotel.Turnover);

        }

        [TestCase("")]
        [TestCase(" ")]
        [TestCase("    ")]
        [TestCase(null)]
        public void Test_ConstructorNameWithInvalidData(string hotelName)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                new Hotel(hotelName, 5);
            });
        }


        [TestCase(-10)]
        [TestCase(0)]
        [TestCase(6)]
        [TestCase(100)]
        public void Test_ConstructorCategoryWithInvalidData(int hotelCategory)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                new Hotel("Leon", hotelCategory);
            });
        }

        [Test]
        public void Test_AddRoomWithValidData()
        {
            Hotel hotel = new Hotel("Leon", 5);
            Room room = new Room(2, 130.0);

            hotel.AddRoom(room);

            int expected = 1;
            int actual = hotel.Rooms.Count;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(-10)]
        [TestCase(0)]
        public void Test_AddRoomWithInvalidBedCapacity(int bedCapacity)
        {
            Hotel hotel = new Hotel("Leon", 5);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.AddRoom(new Room(bedCapacity, 130.0));
            });
        }

        [TestCase(-130)]
        [TestCase(0)]
        public void Test_AddRoomWithInvalidPricePerNight(double pricePerNight)
        {
            Hotel hotel = new Hotel("Leon", 5);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.AddRoom(new Room(2, pricePerNight));
            });
        }

        [Test]
        public void Test_BookRoomAndTurnOverWithValidData()
        {
            Hotel hotel = new Hotel("Leon", 5);
            Room room = new Room(2, 130.0);
            hotel.AddRoom(room);

            Booking booking = new Booking(123, room, 1);
            hotel.BookRoom(1, 0, 1, 130);

            int expected = 1;
            int actual = hotel.Bookings.Count;

            double expectedTurnOver = 130;

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(expectedTurnOver, hotel.Turnover);
        }

        [TestCase(-10)]
        [TestCase(0)]
        public void Test_BookRoomWithInvalidAdults(int adults)
        {
            Hotel hotel = new Hotel("Leon", 5);
            Room room = new Room(2, 130.0);
            hotel.AddRoom(room);
            Booking booking = new Booking(123, room, 1);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(adults, 0, 1, 130);
            });
        }

        [TestCase(-10)]
        [TestCase(-1)]
        public void Test_BookRoomWithInvalidChildren(int children)
        {
            Hotel hotel = new Hotel("Leon", 5);
            Room room = new Room(2, 130.0);
            hotel.AddRoom(room);
            Booking booking = new Booking(123, room, 1);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(1, children, 1, 130);
            });
        }

        [TestCase(-10)]
        [TestCase(-0)]
        public void Test_BookRoomWithInvalidResidenceDuration(int residenceDuration)
        {
            Hotel hotel = new Hotel("Leon", 5);
            Room room = new Room(2, 130.0);
            hotel.AddRoom(room);
            Booking booking = new Booking(123, room, 1);

            Assert.Throws<ArgumentException>(() =>
            {
                hotel.BookRoom(1, 0, residenceDuration, 130);
            });
        }

    }
}