using NUnit.Framework;
using System;

namespace PlanetWars.Tests
{
    public class Tests
    {
        [TestFixture]
        public class PlanetWarsTests
        {
            [Test]
            public void Constructor_Should_SetNameCorrectly()
            {
                var planet = new Planet("Venus", 120);
                var expectedName = "Venus";

                Assert.That(planet.Name, Is.EqualTo(expectedName));
            }
            [Test]
            public void Constructor_ThrowsException_InvalidPlanetName()
            {                
                Assert.Throws<ArgumentException>(
                () => new Planet(null, 120),
                $"Invalid planet name.");
            }

            [Test]
            public void Constructor_ThrowsException_InvalidBudget()
            {
                Assert.Throws<ArgumentException>(
                () => new Planet("Venus", -1),
                $"Budget cannot drop below Zero!");
            }
            [Test]
            public void Constructor_Correctly_CreatesCollectionOfWeapons()
            {
                var planet = new Planet("Venus", 120);

                Assert.That(planet.Weapons.Count, Is.EqualTo(0));
            }
            [Test]
            public void Constructor_Weapon_Correctly_CreatesNewWeapon()
            {
                var weapon = new Weapon("Nuclear", 120, 9);

                Assert.That(weapon.DestructionLevel, Is.EqualTo(9));
                Assert.That(weapon.Price, Is.EqualTo(120));
                Assert.That(weapon.Name, Is.EqualTo("Nuclear"));
            }
            [Test]
            public void AddWeapon_WeaponIsAddedToPlanetCollectionOfWeapons()
            {
                var planet = new Planet("Venus", 120);
                var weapon = new Weapon("Nuclear", 100, 3);

                planet.AddWeapon(weapon);

                Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            }

            [Test]
            public void AddWeapon_AlreadyAddedWeapon()
            {
                var planet = new Planet("Venus", 120);
                var weapon = new Weapon("Nuclear", 20, 12);

                planet.AddWeapon(weapon);

                Assert.Throws<InvalidOperationException>(
                () => planet.AddWeapon(weapon),
                $"There is already a {weapon.Name} weapon");
            }

            [Test]
            public void DestructionLevel_DestructionLevelIsReturned()
            {
                var planet = new Planet("Venus", 80);
                var weapon = new Weapon("Nuclear", 30, 3);
                var weaponTwo = new Weapon("SpaceMissiles", 20, 2);

                planet.AddWeapon(weapon);
                planet.AddWeapon(weaponTwo);

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(5));
            }

            [Test]
            public void Profit_BudgetIncreasesWithGivenAmount()
            {
                var planet = new Planet("Venus", 80);
                planet.Profit(33);

                Assert.That(planet.Budget, Is.EqualTo(113));
            }

            [Test]
            public void SpendFunds_BudgetDecreasesWithGivenAmount()
            {
                var planet = new Planet("Venus", 80);
                planet.SpendFunds(33);

                Assert.That(planet.Budget, Is.EqualTo(47));
            }

            [Test]
            public void SpendFunds_BudgetCannotDropBelowZero()
            {
                var planet = new Planet("Venus", 30);

                Assert.Throws<InvalidOperationException>(
                 () => planet.SpendFunds(33),
                 $"Not enough funds to finalize the deal.");
            }

            [Test]
            public void Weapon_IncreaseDestructionLevel_WorksProperly()
            {
                var weapon = new Weapon("Gun", 15, 2);
                weapon.IncreaseDestructionLevel();

                Assert.That(weapon.DestructionLevel, Is.EqualTo(3));
            }

            [Test]
            public void Weapon_IsNuclear_WorksProperly()
            {
                var weaponNuclear = new Weapon("Nuclear", 1500, 11);
                var weaponGun = new Weapon("Gun", 20, 2);

                Assert.That(weaponNuclear.IsNuclear, Is.EqualTo(true));
                Assert.That(weaponGun.IsNuclear, Is.EqualTo(false));
            }

            [Test]
            public void SellWeapon_WorksProperly()
            {
                var planet = new Planet("NewPlanet", 1500);
                var weaponOne = new Weapon("WeaponOne", 20, 2);
                var weaponTwo = new Weapon("WeaponTwo", 20, 3);

                planet.AddWeapon(weaponOne);
                planet.AddWeapon(weaponTwo);

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(5));

                planet.RemoveWeapon("WeaponOne");

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(3));
                Assert.That(planet.Weapons.Count, Is.EqualTo(1));

            }
            [Test]
            public void UpgradeWeapon_WorksProperly()
            {
                var planet = new Planet("NewPlanet", 1500);
                var weaponOne = new Weapon("WeaponOne", 20, 2);

                planet.AddWeapon(weaponOne);
                planet.UpgradeWeapon("WeaponOne");

                Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(3));
            }

            [Test]
            public void UpgradeWeapon_WeaponDoesNotExist()
            {
                var planet = new Planet("NewPlanet", 1500);

                Assert.Throws<InvalidOperationException>(() => planet.UpgradeWeapon("NotAddedWeapon"),
                    $"NotAddedWeapon does not exist in the weapon repository of {planet.Name}");
            }
            [Test]
            public void DestructOpponent_Throws_IfOpponentIsTooStrong()
            {
                var planetOne = new Planet("PlanetOne", 1500);
                var planetTwo = new Planet("PlanetTwo", 2000);

                var weaponOne = new Weapon("WeaponOne", 20, 2);
                var weaponTwo = new Weapon("WeaponTwo", 30, 5);
                var weaponThree = new Weapon("WeaponThree", 20, 2);


                planetOne.AddWeapon(weaponOne);
                planetOne.AddWeapon(weaponThree);
                planetTwo.AddWeapon(weaponTwo);

                Assert.Throws<InvalidOperationException>(() => planetOne.DestructOpponent(planetTwo),
                    $"{planetTwo.Name} is too strong to declare war to!");
            }

            [Test]
            public void DestructOpponent_WorksProperly()
            {
                var planetOne = new Planet("PlanetOne", 1500);
                var planetTwo = new Planet("PlanetTwo", 2000);

                var weaponOne = new Weapon("WeaponOne", 20, 2);
                var weaponTwo = new Weapon("WeaponTwo", 30, 5);
                var weaponThree = new Weapon("WeaponThree", 20, 4);


                planetOne.AddWeapon(weaponOne);
                planetOne.AddWeapon(weaponThree);
                planetTwo.AddWeapon(weaponTwo);

                var expectedResult = "PlanetTwo is destructed!";

                Assert.That(planetOne.DestructOpponent(planetTwo), Is.EqualTo(expectedResult));
            }
            [Test]
            public void Weapon_PriceCannotBeNagative()
            {


                Assert.Throws<ArgumentException>(() => new Weapon("Weapon", -5, 8), "Price can not be negative.");
            }
        }
    }
}
