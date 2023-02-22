namespace Robots.Tests
{
    using NUnit.Framework;
    using System;

    public class RobotsTests
    {
        [Test]
        public void Test_Robot_Ctor()
        {
            Robot robot = new Robot("iRob", 97);

            Assert.IsNotNull(robot);
            Assert.That(robot.Name, Is.EqualTo("iRob"));
            Assert.That(robot.MaximumBattery, Is.EqualTo(97));
            Assert.That(robot.Battery, Is.EqualTo(97));
        }
        [Test]
        public void Test_RobotManager_Ctor()
        {
            RobotManager robots = new RobotManager(13);

            Assert.That(robots.Capacity, Is.EqualTo(13));
            Assert.That(robots.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_RobotManager_Capacity_Exception()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                RobotManager robots = new RobotManager(-1);
            }, "Invalid capacity!");
        }

        [Test]
        public void Test_RobotManager_Add_Robot()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot1 = new Robot("iPesho", 97);
            Robot robot2 = new Robot("iBob", 86);

            robots.Add(robot1);
            robots.Add(robot2);

            Assert.That(robots.Count, Is.EqualTo(2));
        }

        [Test]
        public void Test_RobotManager_Add_Robot_That_Exist()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot1 = new Robot("iPesho", 97);
            Robot robot2 = new Robot("iBob", 86);

            robots.Add(robot1);
            robots.Add(robot2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robots.Add(robot2);
            }, "There is already a robot with name iBob!");
        }

        [Test]
        public void Test_RobotManager_Add_Robot_Capacity_Exception()
        {
            RobotManager robots = new RobotManager(2);
            Robot robot1 = new Robot("iPesho", 97);
            Robot robot2 = new Robot("iBob", 86);
            Robot robot3 = new Robot("iMan", 16);


            robots.Add(robot1);
            robots.Add(robot2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robots.Add(robot3);
            }, "Not enough capacity!");
        }

        [Test]
        public void Test_RobotManager_Remove_Robot()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot1 = new Robot("iPesho", 97);
            Robot robot2 = new Robot("iBob", 86);

            robots.Add(robot1);
            robots.Add(robot2);

            robots.Remove("iBob");

            Assert.That(robots.Count, Is.EqualTo(1));
        }

        [Test]
        public void Test_RobotManager_Remove_Robot_That_Not_Exist()
        {
            RobotManager robots = new RobotManager(2);
            Robot robot1 = new Robot("iPesho", 97);
            Robot robot2 = new Robot("iBob", 86);


            robots.Add(robot1);
            robots.Add(robot2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robots.Remove("bai Ivan");
            }, "Robot with the name bai Ivan doesn't exist!");
        }


        [Test]
        public void Test_RobotManager_Work()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot1 = new Robot("iPesho", 97);
            Robot robot2 = new Robot("iBob", 86);

            robots.Add(robot1);
            robots.Add(robot2);

            robots.Work("iBob", ".....", 36);

            Assert.That(50, Is.EqualTo(robot2.Battery));
        }

        [Test]
        public void Test_RobotManager_Work_Robot_Not_Exist()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot1 = new Robot("iPesho", 97);

            robots.Add(robot1);

            Assert.Throws<InvalidOperationException>(()=>
            {
                robots.Work("iBob", ".....", 36);
            }, "Robot with the name iBob doesn't exist");            
        }

        [Test]
        public void Test_RobotManager_Work_Not_Enought_Battery()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot1 = new Robot("iBob", 67);

            robots.Add(robot1);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robots.Work("iBob", ".....", 80);
            }, "iBob doesn't have enough battery!");
        }


        [Test]
        public void Test_RobotManager_Charge()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot = new Robot("iBob", 86);

            robots.Add(robot);
            robots.Work("iBob", ".....", 80);

            robots.Charge("iBob");

            Assert.That(86, Is.EqualTo(robot.Battery));
        }
        [Test]
        public void Test_RobotManager_Charge_Robot_That_Not_Exist()
        {
            RobotManager robots = new RobotManager(13);
            Robot robot = new Robot("iBob", 86);

            robots.Add(robot);
            robots.Work("iBob", ".....", 80);

            Assert.Throws<InvalidOperationException>(() =>
            {
                robots.Charge("iPesho");
            }, "Robot with the name iPesho doesn't exist!");
        }


    }
}
