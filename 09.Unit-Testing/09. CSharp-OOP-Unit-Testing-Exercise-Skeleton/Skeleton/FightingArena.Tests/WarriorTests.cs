namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        [Test]
        public void Test_WarriorConstructorWithCalidData()
        {
            Warrior warrior = new Warrior("Axil", 60, 80);

            Assert.AreEqual("Axil", warrior.Name);
            Assert.AreEqual(60, warrior.Damage);
            Assert.AreEqual(80, warrior.HP);

        }

        [TestCase("")]
        [TestCase("    ")]
        public void Test_WarriorNameShouldNotBeEmptyOrWhitespace(string value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warriorOne = new Warrior(value, 60, 80);
            }, "Name should not be empty or whitespace!");
        }

        [TestCase(0)]
        [TestCase(-1)]
        public void Test_DamageValueShouldBePositive(int value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warriorOne = new Warrior("Axil", value, 80);
            }, "Damage value should be positive!");
        }

        [TestCase(-1)]
        [TestCase(-100)]
        public void Test_HPValueShouldNotBeNegative(int value)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Warrior warriorOne = new Warrior("Axil", 60, value);
            }, "HP should not be negative!");
        }

        [TestCase(70)]
        [TestCase(80)]
        public void Test_WarriorOneHPIsGreaterThenWarriorTwoDamage(int warriorOneHP)
        {
            Warrior warriorOne = new Warrior("Axil", 90, warriorOneHP);
            Warrior warriorTwo = new Warrior("Paris", 70, 60);

            warriorOne.Attack(warriorTwo);

            int expected = warriorOneHP - warriorTwo.Damage; ;
            int actual = warriorOne.HP;

            Assert.AreEqual(expected, actual);

        }

        [TestCase(61)]
        [TestCase(80)]
        public void Test_WarriorOneDamageIsGreaterThenWarriorTwoHP(int warriorOneDamage)
        {
            Warrior warriorOne = new Warrior("Axil", warriorOneDamage, 80);
            Warrior warriorTwo = new Warrior("Paris", 70, 60);

            warriorOne.Attack(warriorTwo);

            int expected = 0; ;
            int actual = warriorTwo.HP;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_WarriorOneDamageIsNotGreaterThenWarriorTwoHP()
        {
            Warrior warriorOne = new Warrior("Axil", 75, 80);
            Warrior warriorTwo = new Warrior("Paris", 70, 80);

            warriorOne.Attack(warriorTwo);

            int expected = 5;
            int actual = warriorTwo.HP;

            Assert.AreEqual(expected, actual);
        }

        [TestCase(3)]
        [TestCase(30)]
        public void Test_WarriorOneHPIsTooLow(int value)
        {
            Warrior warriorOne = new Warrior("Axil", 80, value);
            Warrior warriorTwo = new Warrior("Paris", 70, 80);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warriorOne.Attack(warriorTwo);
            }, "Your HP is too low in order to attack other warriors!");
        }

        [TestCase(3)]
        [TestCase(30)]
        public void Test_WarriorTwoHPIsTooLow(int value)
        {
            Warrior warriorOne = new Warrior("Axil", 80, 80);
            Warrior warriorTwo = new Warrior("Paris", 70, value);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warriorOne.Attack(warriorTwo);
            }, "Enemy HP must be greater than 30 in order to attack him!");
        }

        [Test]
        public void Test_AttackTooStrongEnemy()
        {
            Warrior warriorOne = new Warrior("Axil", 80, 60);
            Warrior warriorTwo = new Warrior("Paris", 70, 70);

            Assert.Throws<InvalidOperationException>(() =>
            {
                warriorOne.Attack(warriorTwo);
            }, "You are trying to attack too strong enemy");
        }


    }
}