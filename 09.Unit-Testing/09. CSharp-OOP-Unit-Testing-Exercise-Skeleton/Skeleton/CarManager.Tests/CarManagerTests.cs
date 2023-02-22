namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        private string make;
        private string model;
        private double fuelConsumption;
        private double fuelCapacity;

        private Car car;

        [SetUp]
        public void SetUp()
        {
            this.make = "AUDI";
            this.model = "A6";
            this.fuelConsumption = 10.0;
            this.fuelCapacity = 60.0;
        }

        [Test]
        public void Test_ConstructorWithValidData()
        {
            car = new Car(make, model, fuelConsumption, fuelCapacity);

            Assert.AreEqual(make, car.Make);
            Assert.AreEqual(model, car.Model);
            Assert.AreEqual(fuelConsumption, car.FuelConsumption);
            Assert.AreEqual(fuelCapacity, car.FuelCapacity);
            Assert.AreEqual(0, car.FuelAmount);
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_MakeCannotBeNullOrEmpty(string input)
        {
            make = input;
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(make, model, fuelConsumption, fuelCapacity);
            }, "Make cannot be null or empty!");
        }

        [TestCase("")]
        [TestCase(null)]
        public void Test_ModelCannotBeNullOrEmpty(string input)
        {
            model = input;
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(make, model, fuelConsumption, fuelCapacity);
            }, "Model cannot be null or empty!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_FuelConsumptionCannotBeZeroOrNegative(double fuel)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(make, model, fuel, fuelCapacity);
            }, "Fuel consumption cannot be zero or negative!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_FuelCapacityCannotBeZeroOrNegative(double capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car = new Car(make, model, fuelConsumption, capacity);
            }, "Fuel capacity cannot be zero or negative!");
        }

        [TestCase(1)]
        [TestCase(20)]
        [TestCase(60)]
        public void Test_RefuelCarWithValidData(double fuelToRefuel)
        {
            car = new Car(make, model, fuelConsumption, fuelCapacity);
            car.Refuel(fuelToRefuel);

            Assert.AreEqual(fuelToRefuel, car.FuelAmount);
        }

        [TestCase(61)]
        [TestCase(200)]
        public void Test_RefuelCarWithWithMoreFuel(double fuelToRefuel)
        {
            car = new Car(make, model, fuelConsumption, fuelCapacity);
            car.Refuel(fuelToRefuel);

            Assert.AreEqual(60, car.FuelAmount);
        }
        [TestCase(0)]
        [TestCase(-1)]
        public void Test_RefuelCarWithZeroOrNegativValue(double fuelToRefuel)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                car.Refuel(fuelToRefuel);
            }, "Fuel amount cannot be zero or negative!");
        }

        [TestCase(1)]
        [TestCase(600)]
        public void Test_DriveCarWithValidData(double distance)
        {
            car = new Car(make, model, fuelConsumption, fuelCapacity);

            car.Refuel(60.0);
            double fuelAmount = 60.0;
            car.Drive(distance);
            double fuelNeeded = (distance / 100) * this.fuelConsumption;

            Assert.AreEqual(fuelAmount - fuelNeeded, car.FuelAmount);
        }

        [TestCase(101)]
        [TestCase(1000)]
        public void Test_DriveCarWithNotEnoughFuel(double distance)
        {
            car.Refuel(10);
            Assert.Throws<InvalidOperationException>(() =>
            {
                car.Drive(distance);
            }, "You don't have enough fuel to drive!");
        }

    }
}