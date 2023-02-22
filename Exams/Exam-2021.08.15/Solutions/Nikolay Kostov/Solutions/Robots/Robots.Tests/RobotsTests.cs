namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class RobotsTests
    {
        [Test]
        public void ConstructorShouldSetCapacity()
        {
            var robots = new RobotManager(4);
            Assert.AreEqual(4, robots.Capacity);
        }

        [Test]
        public void ConstructorShouldThrowAnExceptionForNegativeCapacity()
        {
            Assert.Throws<ArgumentException>(() => new RobotManager(-3));
        }

        [Test]
        public void EmptyRobotManagerShouldHaveCountOfZero()
        {
            var robots = new RobotManager(100);
            Assert.AreEqual(0, robots.Count);
        }

        [Test]
        public void RobotManagerShouldHaveProperCount()
        {
            var robots = new RobotManager(100);
            robots.Add(new Robot("r1", 100));
            robots.Add(new Robot("r2", 100));
            robots.Add(new Robot("r3", 100));
            Assert.AreEqual(3, robots.Count);
        }

        [Test]
        public void AddShouldThrowAnExceptionForRobotsWithTheSameName()
        {
            var robots = new RobotManager(100);
            robots.Add(new Robot("r1", 100));
            Assert.Throws<InvalidOperationException>(
                () => robots.Add(new Robot("r1", 1000)));
        }

        [Test]
        public void AddShouldThrowAnExceptionWhenCapacityIsReached()
        {
            var robots = new RobotManager(2);
            robots.Add(new Robot("r1", 100));
            robots.Add(new Robot("r2", 100));
            Assert.Throws<InvalidOperationException>(
                () => robots.Add(new Robot("r3", 1000)));
        }

        [Test]
        public void RemoveShouldWorkProperly()
        {
            var robots = new RobotManager(2);
            robots.Add(new Robot("r1", 100));
            robots.Add(new Robot("r2", 100));
            robots.Remove("r2");
            Assert.AreEqual(1, robots.Count);
            robots.Remove("r1");
            Assert.AreEqual(0, robots.Count);
        }

        [Test]
        public void RemoveShouldThrowAnExceptionWhenRobotIsNotFound()
        {
            var robots = new RobotManager(2);
            robots.Add(new Robot("r1", 100));
            Assert.Throws<InvalidOperationException>(() => robots.Remove("r2"));
        }

        [Test]
        public void WorkShouldWorkCorrectly()
        {
            var robots = new RobotManager(2);
            var robot = new Robot("r1", 100);
            robots.Add(robot);
            robots.Work("r1", "...", 40);
            Assert.AreEqual(60, robot.Battery);
        }

        [Test]
        public void WorkShouldThrowAnExceptionWhenRobotIsNotFound()
        {
            var robots = new RobotManager(2);
            robots.Add(new Robot("r1", 100));
            Assert.Throws<InvalidOperationException>(
                () => robots.Work("r2", "...", 20));
        }

        [Test]
        public void WorkShouldThrowAnExceptionWhenRobotIsExhausted()
        {
            var robots = new RobotManager(2);
            robots.Add(new Robot("r1", 20));
            Assert.Throws<InvalidOperationException>(
                () => robots.Work("r1", "...", 30));
        }

        [Test]
        public void ChargeShouldWorkProperly()
        {
            var robots = new RobotManager(2);
            var robot = new Robot("r1", 100);
            robots.Add(robot);
            robots.Work("r1", "...", 60);
            robots.Charge("r1");
            Assert.AreEqual(100, robot.Battery);
        }

        [Test]
        public void ChargeShouldThrowAnExceptionWhenRobotIsNotFound()
        {
            var robots = new RobotManager(2);
            robots.Add(new Robot("r1", 100));
            Assert.Throws<InvalidOperationException>(
                () => robots.Charge("fsfsdfssfd"));
        }
    }
}
