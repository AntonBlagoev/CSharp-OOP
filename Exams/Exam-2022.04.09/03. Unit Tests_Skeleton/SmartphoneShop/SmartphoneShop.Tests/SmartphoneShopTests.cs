namespace SmartphoneShop.Tests
{
    using NUnit.Framework;
    using System;
    [TestFixture]
    public class SmartphoneShopTests
    {
        [TestCase(20)]
        [TestCase(1)]
        [TestCase(100)]
        public void Test_Shop_Constructor(int capacity)
        {
            Shop shop = new Shop(capacity);

            Assert.IsNotNull(shop);
            Assert.AreEqual(capacity, shop.Capacity);
            Assert.AreEqual(0, shop.Count);
        }

        [TestCase(-1)]
        [TestCase(-10)]
        public void Test_Shop_Constructor_Invalid_Capacity(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Shop shop = new Shop(capacity);
            }, "Invalid capacity.");
        }

        [Test]
        public void Test_Add_Smartphone()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);

            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.AreEqual(2, shop.Count);

            Assert.AreEqual("Nokia", smartphone1.ModelName);
            Assert.AreEqual(97, smartphone1.MaximumBatteryCharge);
            Assert.AreEqual(97, smartphone1.CurrentBateryCharge);

            Assert.AreEqual("Xiaomi", smartphone2.ModelName);
            Assert.AreEqual(65, smartphone2.MaximumBatteryCharge);
            Assert.AreEqual(65, smartphone2.CurrentBateryCharge);
        }

        [Test]
        public void Test_Add_Smartphone_That_Exist()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);

            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphone1);
            }, "The phone model Nokia already exist.");
        }

        [Test]
        public void Test_Add_Smartphone_Shop_Capacity_Is_Full()
        {
            Shop shop = new Shop(1);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);

            shop.Add(smartphone1);


            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphone2);
            }, "The shop is full.");
        }
        [Test]
        public void Test_Add_Smartphone_Shop_Capacity_Is_Null()
        {
            Shop shop = new Shop(0);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Add(smartphone1);
            }, "The shop is full.");
        }
        [Test]
        public void Test_Count()
        {
            Shop shop = new Shop(10);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);

            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.AreEqual(2, shop.Count);
        }

        [Test]
        public void Test_Remove_Smatphone()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);

            shop.Add(smartphone1);
            shop.Add(smartphone2);

            shop.Remove("Nokia");

            Assert.AreEqual(1, shop.Count);
        }

        [Test]
        public void Test_Remove_Not_Existing_Smatphone()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);

            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("Samsung");
            }, $"The phone model Samsung doesn't exist.");
        }

        [Test]
        public void Test_Remove_Smatphone_From_Empty_Shop()
        {
            Shop shop = new Shop(0);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.Remove("Samsung");
            }, $"The phone model Samsung doesn't exist.");
        }

        [Test]
        public void Test_TestPhone()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            shop.TestPhone("Nokia", 42);

            Assert.AreEqual(55, smartphone1.CurrentBateryCharge);
        }
        [Test]
        public void Test_TestPhone_That_Not_Exist()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Samsung", 42);
            }, "The phone model Samsung doesn't exist.");
        }
        [Test]
        public void Test_TestPhone_That_Not_Exist_Null()
        {
            Shop shop = new Shop(12);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Samsung", 42);
            }, "The phone model Samsung doesn't exist.");
        }

        [TestCase(18)]
        [TestCase(100)]
        public void Test_TestPhone_With_Low_Battery(int batteryUsage)
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 17);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.TestPhone("Nokia", batteryUsage);
            }, "The phone model Nokia is low on batery.");
        }

        [Test]
        public void Test_ChargePhone()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            shop.ChargePhone("Nokia");
            shop.ChargePhone("Xiaomi");

            Assert.AreEqual(97, smartphone1.CurrentBateryCharge);
            Assert.AreEqual(97, smartphone1.MaximumBatteryCharge);

            Assert.AreEqual(65, smartphone2.CurrentBateryCharge);
            Assert.AreEqual(65, smartphone2.MaximumBatteryCharge);


        }

        [Test]
        public void Test_ChargePhone_Phone_Not_Exist()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            Smartphone smartphone2 = new Smartphone("Xiaomi", 65);
            shop.Add(smartphone1);
            shop.Add(smartphone2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("Samsung");
            }, "The phone model Samsung doesn't exist.");
        }

        [Test]
        public void Test_ChargePhone_Phone_Not_Exist_Null()
        {
            Shop shop = new Shop(12);

            Assert.Throws<InvalidOperationException>(() =>
            {
                shop.ChargePhone("Samsung");
            }, "The phone model Samsung doesn't exist.");
        }
        [Test]
        public void Test_Create_Smartphone()
        {
            Smartphone smartphone1 = new Smartphone("Nokia", 97);

            Assert.AreEqual("Nokia", smartphone1.ModelName);
            Assert.AreEqual(97, smartphone1.CurrentBateryCharge);
            Assert.AreEqual(97, smartphone1.MaximumBatteryCharge);


        }
        // ---------------



        [Test]
        public void Test_Test_And_Charge_Phone()
        {
            Shop shop = new Shop(12);
            Smartphone smartphone1 = new Smartphone("Nokia", 97);
            shop.Add(smartphone1);

            shop.TestPhone("Nokia", 42);
            shop.ChargePhone("Nokia");

            Assert.AreEqual(97, smartphone1.CurrentBateryCharge);
            Assert.AreEqual(97, smartphone1.MaximumBatteryCharge);

        }
    }
}