using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [SetUp]
            public void Setup()
            {

            }
            [Test]
            public void Test_PlanetConstructorWithValidData()
            {
                Planet planet = new Planet("Mars", 500);

                Assert.AreEqual("Mars", planet.Name);
                Assert.AreEqual(500, planet.Budget);
                Assert.AreEqual(0, planet.Weapons.Count);
            }

            [TestCase(null)]
            [TestCase("")]
            public void Test_PlanetNametWithIncorrectData(string name)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planet = new Planet(name, 500);
                }, "Invalid planet Name");
            }

            [TestCase(-1)]
            [TestCase(-10.0)]
            public void Test_PlanetBudgetWithIncorrectData(double budget)
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Planet planet = new Planet("Mars", budget);
                }, "Budget cannot drop below Zero!");
            }

            [Test]
            public void Test_Profit()
            {
                Planet planet = new Planet("Mars", 500);

                planet.Profit(250);

                Assert.AreEqual(750, planet.Budget);
            }

            [Test]
            public void Test_SpendFundsWithValidData()
            {
                Planet planet = new Planet("Mars", 500);

                planet.SpendFunds(200);

                Assert.AreEqual(300, planet.Budget);
            }

            [Test]
            public void Test_SpendFundsWithInValidData()
            {
                Planet planet = new Planet("Mars", 500);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.SpendFunds(700);
                }, $"Not enough funds to finalize the deal.");
            }

            [Test]
            public void Test_AddWeaponWithValidData()
            {
                Planet planet = new Planet("Mars", 500);
                Weapon bomb = new Weapon("Bomb", 200, 100);
                Weapon rockets = new Weapon("Rockets", 100, 50);

                planet.AddWeapon(bomb);
                planet.AddWeapon(rockets);

                Assert.AreEqual(2, planet.Weapons.Count);

            }

            [Test]
            public void Test_AddWeaponThatAlreadyAdded()
            {
                Planet planet = new Planet("Mars", 500);
                Weapon weapon = new Weapon("Bomb", 200, 100);

                planet.AddWeapon(weapon);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.AddWeapon(weapon);
                }, $"There is already a {weapon.Name} weapon.");

            }

            [Test]
            public void Test_RemoveWeapon()
            {
                Planet planet = new Planet("Mars", 500);
                Weapon bomb = new Weapon("Bomb", 200, 100);
                Weapon rockets = new Weapon("Rockets", 100, 50);

                planet.AddWeapon(bomb);
                planet.AddWeapon(rockets);
                planet.RemoveWeapon("Bomb");

                Assert.AreEqual(1, planet.Weapons.Count);
            }

            [Test]
            public void Test_UpgradeWeaponWithValidData()
            {
                Planet planet = new Planet("Mars", 500);
                Weapon weapon = new Weapon("Bomb", 200, 100);

                planet.AddWeapon(weapon);
                planet.UpgradeWeapon(weapon.Name);

                Weapon actuualWeapon = planet.Weapons.Where(x => x.Name == weapon.Name).FirstOrDefault();

                Assert.AreEqual(101, actuualWeapon.DestructionLevel);
            }

            [Test]
            public void Test_UpgradeWeaponNotExist()
            {
                Planet planet = new Planet("Mars", 500);
                Weapon weapon = new Weapon("Bomb", 200, 100);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planet.UpgradeWeapon("Bomb");
                }, $"Bomb does not exist in the weapon repository of Mars");
            }


            [Test]
            public void Test_DestructOpponentWithValidData()
            {
                Planet planetMars = new Planet("Mars", 500);
                Weapon weaponMars = new Weapon("Bomb", 200, 100);
                planetMars.AddWeapon(weaponMars);

                Planet planetEarth = new Planet("Earth", 400);
                Weapon weaponEarth = new Weapon("Bomb", 200, 50);
                planetEarth.AddWeapon(weaponEarth);

                planetMars.DestructOpponent(planetEarth);

                string expectedResult = "Earth is destructed!";

                Assert.That(planetMars.DestructOpponent(planetEarth), Is.EqualTo(expectedResult));
            }

            [Test]
            public void Test_DestructOpponentWithNotValidData()
            {
                Planet planetMars = new Planet("Mars", 500);
                Weapon weaponMars = new Weapon("Bomb", 200, 100);
                planetMars.AddWeapon(weaponMars);

                Planet planetEarth = new Planet("Earth", 400);
                Weapon weaponEarth = new Weapon("Bomb", 200, 50);
                planetEarth.AddWeapon(weaponEarth);

                Assert.Throws<InvalidOperationException>(() =>
                {
                    planetEarth.DestructOpponent(planetMars);
                }, $"{planetMars.Name} is too strong to declare war to!");

            }

            [Test]
            public void Test_WeaponIsNuclear()
            {
                var weaponNuclear = new Weapon("Nuclear", 1500, 11);
                var weaponGun = new Weapon("Gun", 20, 2);

                Assert.That(weaponNuclear.IsNuclear, Is.EqualTo(true));
                Assert.That(weaponGun.IsNuclear, Is.EqualTo(false));
            }

            [Test]
            public void Test_WeaponPriseCannotBeNegative()
            {
                Assert.Throws<ArgumentException>(() =>
                {
                    Weapon weapon = new Weapon("Bomb", -200, 100);
                }, "Price cannot be negative.");
                
            }

            [Test]
            public void Test_WeaponPrise()
            {
                Weapon weapon = new Weapon("Bomb", 200, 100);

                Assert.AreEqual(200, weapon.Price);

            }
           
        }
    }
}
