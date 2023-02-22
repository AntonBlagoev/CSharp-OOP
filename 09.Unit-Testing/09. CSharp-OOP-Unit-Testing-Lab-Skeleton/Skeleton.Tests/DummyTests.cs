using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        private Dummy dummy;
        private int health;
        private int experience;

        [SetUp]
        public void Setup()
        {
            health = 10;
            experience = 15;

            dummy = new Dummy(health, experience);
        }
        [Test]
        public void DummyConstructorShouldSetDataCorrectly()
        {
            Assert.AreEqual(health, dummy.Health);
        }
        [Test]
        public void DummyLosesHealthIfAtacked()
        {
            dummy.TakeAttack(5);
            Assert.AreEqual(health - 5, dummy.Health);
        }

        [Test]
        public void DeadDummyThrowsAnExceptionIfAttacked()
        {
            dummy.TakeAttack(health);

            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.TakeAttack(1);
            });
        }

        [Test]
        public void DeadDummyCanGiveXP()
        {
            dummy.TakeAttack(10);
            var dummyExperience = dummy.GiveExperience();

            int expectedExperience = 15;
            Assert.AreEqual(expectedExperience, dummyExperience);

        }
        [Test]
        public void DeadDummyDoesNotGiveXP()
        {
            dummy.TakeAttack(9);
            Assert.Throws<InvalidOperationException>(() =>
            {
                dummy.GiveExperience();

            }, "Target is not dead.");
        }
    }
}