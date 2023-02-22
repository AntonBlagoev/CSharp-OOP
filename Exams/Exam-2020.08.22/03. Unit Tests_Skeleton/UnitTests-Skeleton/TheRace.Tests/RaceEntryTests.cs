using NUnit.Framework;
using System;
using System.Runtime.ConstrainedExecution;
using TheRace;

namespace TheRace.Tests
{
    public class RaceEntryTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_UnitCar()
        {
            UnitCar car = new UnitCar("MyCar", 72, 2000);

            Assert.IsNotNull(car);
            Assert.That(car.Model, Is.EqualTo("MyCar"));
            Assert.That(car.HorsePower, Is.EqualTo(72));
            Assert.That(car.CubicCentimeters, Is.EqualTo(2000));
        }


        [Test]
        public void Test_UnitDriver()
        {
            UnitCar car = new UnitCar("MyCar", 72, 2000);
            UnitDriver driver = new UnitDriver("Pesho", car);

            Assert.IsNotNull(driver);
            Assert.That(driver.Name, Is.EqualTo("Pesho"));
            Assert.That(driver.Car, Is.EqualTo(car));
        }
        [Test]
        public void Test_UnitDriver_Exception()
        {
            UnitCar car = new UnitCar("MyCar", 72, 2000);

            Assert.Throws<ArgumentNullException>(() =>
            {
                UnitDriver driver = new UnitDriver(null, car);
            }, "Name cannot be null!");
        }

        [Test]
        public void Test_RaceEntry()
        {
            RaceEntry race = new RaceEntry();

            Assert.IsNotNull(race);
            Assert.That(race.Counter, Is.EqualTo(0));
        }

        [Test]
        public void Test_RaceEntry_AddDriver()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("MyCar", 72, 2000);
            UnitDriver driver = new UnitDriver("Pesho", car);

            race.AddDriver(driver);

            Assert.That(race.Counter, Is.EqualTo(1));

        }

        [Test]
        public void Test_RaceEntry_AddDriver_Null_Exception()
        {
            RaceEntry race = new RaceEntry();

            Assert.Throws<InvalidOperationException>(() =>
            {
                race.AddDriver(null);
            }, "Driver cannot be null.");
        }
        [Test]
        public void Test_RaceEntry_AddDriver_That_Exist()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car = new UnitCar("MyCar", 72, 2000);
            UnitDriver driver = new UnitDriver("Pesho", car);

            race.AddDriver(driver);

            Assert.Throws<InvalidOperationException>(() =>
            {
                race.AddDriver(driver);
            }, "Driver Pesho is already added.");
        }

        [Test]
        public void Test_CalculateAverageHorsePower()
        {
            RaceEntry race = new RaceEntry();

            UnitCar car1 = new UnitCar("MyCar", 60, 2000);
            UnitCar car2 = new UnitCar("MyCar", 70, 2000);
            UnitCar car3 = new UnitCar("MyCar", 80, 2000);

            UnitDriver driver1 = new UnitDriver("Pesho", car1);
            UnitDriver driver2 = new UnitDriver("Gosho", car2);
            UnitDriver driver3 = new UnitDriver("Misho", car3);

            race.AddDriver(driver1);
            race.AddDriver(driver2);
            race.AddDriver(driver3);

            var actual = race.CalculateAverageHorsePower();
            var expected = 70;

            Assert.That(actual, Is.EqualTo(expected));


        }

        [Test]
        public void Test_CalculateAverageHorsePower_Exception()
        {
            RaceEntry race = new RaceEntry();
            UnitCar car1 = new UnitCar("MyCar", 60, 2000);
            UnitDriver driver1 = new UnitDriver("Pesho", car1);

            race.AddDriver(driver1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                race.CalculateAverageHorsePower();
            }, "The race cannot start with less than 2 participants.");


        }


    }
}