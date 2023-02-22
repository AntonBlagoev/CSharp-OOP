using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        [TestFixture]
        public class RepairsShopTests
        {
            [SetUp]
            public void Setup()
            {

            }
            [Test]
            public void Test_Garage_Constructor_With_Valid_Data()
            {
                Garage garage = new Garage("Best", 3);

                Assert.IsNotNull(garage);
                Assert.AreEqual("Best", garage.Name);
                Assert.AreEqual(3, garage.MechanicsAvailable);
                Assert.AreEqual(0, garage.CarsInGarage);

            }

            [TestCase(null)]
            [TestCase("")]
            public void Test_Garage_Constructor_With_Invalid_Name(string name)
            {
                Assert.Throws<ArgumentNullException>(() =>
                {
                    Garage garage = new Garage(name, 3);
                }, "Invalid garage name.");
            }

            [TestCase(0)]
            [TestCase(-1)]
            public void Test_Garage_Constructor_With_Negativ_Numbers(int number)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Garage garage = new Garage("Best", number);
                }, "At least one mechanic must work in the garage.");
            }
            [Test]
            public void Test_Add_Car_And_Count()
            {
                Garage garage = new Garage("Best", 3);
                Car car = new Car("BMW", 15);

                garage.AddCar(car);

                Assert.AreEqual(1, garage.CarsInGarage);

                Assert.IsNotNull(car);
                Assert.AreEqual("BMW", car.CarModel);
                Assert.AreEqual(15, car.NumberOfIssues);
                Assert.AreEqual(false, car.IsFixed);

            }
            [Test]
            public void Test_No_Mechanic_Available()
            {
                Garage garage = new Garage("Best", 1);
                Car car1 = new Car("BMW", 15);
                Car car2 = new Car("VW", 12);

                garage.AddCar(car1);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.AddCar(car2);

                }, "No mechanic available.");
            }

            [Test]
            public void Test_FixCar()
            {
                Garage garage = new Garage("Best", 2);
                Car car1 = new Car("BMW", 15);
                Car car2 = new Car("VW", 12);
                garage.AddCar(car1);
                garage.AddCar(car2);

                garage.FixCar("BMW");

                Assert.AreEqual(0, car1.NumberOfIssues);
                Assert.AreEqual(true, car1.IsFixed);

            }
            [Test]
            public void Test_FixCar_Exceptions()
            {
                Garage garage = new Garage("Best", 2);
                Car car1 = new Car("BMW", 15);
                Car car2 = new Car("VW", 12);
                garage.AddCar(car1);
                garage.AddCar(car2);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.FixCar("Audi");

                }, $"The car Audi doesn't exist.");
            }

            [Test]
            public void Test_RemoveFixedCar()
            {
                Garage garage = new Garage("Best", 2);
                Car car1 = new Car("BMW", 15);
                Car car2 = new Car("VW", 12);
                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.FixCar("BMW");

                garage.RemoveFixedCar();

                Assert.AreEqual(1, garage.CarsInGarage);
            }

            [Test]
            public void Test_RemoveFixedCar_Exceptions()
            {
                Garage garage = new Garage("Best", 2);
                Car car1 = new Car("BMW", 15);
                Car car2 = new Car("VW", 12);
                garage.AddCar(car1);
                garage.AddCar(car2);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    garage.RemoveFixedCar();

                }, $"No fixed cars available.");
            }
            [Test]
            public void Test_Report()
            {
                Garage garage = new Garage("Best", 3);
                Car car1 = new Car("BMW", 15);
                Car car2 = new Car("VW", 12);
                Car car3 = new Car("Audi", 1);

                garage.AddCar(car1);
                garage.AddCar(car2);
                garage.AddCar(car3);

                garage.FixCar("Audi");
                string expected = "There are 2 which are not fixed: BMW, VW.";
                string actual = garage.Report();

                Assert.AreEqual(expected, actual);
                Assert.That(actual, Is.EqualTo(expected));
            }

            [Test]
            public void Test_Report_Null()
            {
                Garage garage = new Garage("Best", 3);

                string expected = "There are 0 which are not fixed: .";
                string actual = garage.Report();

                Assert.AreEqual(expected, actual);
            }
        }
    }
}