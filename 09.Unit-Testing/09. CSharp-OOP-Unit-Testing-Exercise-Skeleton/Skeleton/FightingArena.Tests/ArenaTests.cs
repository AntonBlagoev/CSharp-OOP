namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Threading;

    [TestFixture]
    public class ArenaTests
    {
        private Arena arena;

        [SetUp]
        public void Setup()
        {
            this.arena = new Arena();
        }

        [Test]
        public void Test_Constructor()
        {
            Arena arena = new Arena();
            Assert.IsNotNull(arena.Warriors);
        }

        [Test]
        public void CountShouldReturnEnrolledWarriorsCount()
        {
            Warrior warrior = new Warrior("Axil", 80, 100);

            this.arena.Enroll(warrior);

            int expectedCount = 1;
            int actualCount = this.arena.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void EnrollWarrierToArena()
        {
            Warrior warrior = new Warrior("Axil", 80, 100);
            this.arena.Enroll(warrior);

            int expected = 1;
            int actual = arena.Count;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void WarrierIsAlreadyEnrolledToArena()
        {
            Warrior warrior = new Warrior("Axil", 80, 100);
            this.arena.Enroll(warrior);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Enroll(warrior);
            }, "Warrior is already enrolled for the fights!");
        }

        [TestCase("Gosho", "Axil")]
        [TestCase("Pesho", "Gosho")]
        [TestCase("Paris", "Pesho")]
        public void Test_MissingNameOfFighter(string warriorOneName, string warriorTwoName)
        {
            Warrior warriorOne = new Warrior("Axil", 80, 100);
            Warrior warriorTwo = new Warrior("Paris", 80, 100);

            this.arena.Enroll(warriorOne);
            this.arena.Enroll(warriorTwo);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.arena.Fight(warriorOneName, warriorTwoName);
            }, $"There is no fighter with name {warriorOneName} enrolled for the fights!");
        }


        [Test]
        public void Test_Fight()
        {
            this.arena = new Arena();
            Warrior attacker = new Warrior("Axil", 90, 80);
            Warrior defender = new Warrior("Paris", 70, 60);

            this.arena.Enroll(attacker);
            this.arena.Enroll(defender);

            this.arena.Fight(attacker.Name, defender.Name);

            Assert.AreEqual(10, attacker.HP);
            Assert.AreEqual(0, defender.HP);


        }

    }
}
