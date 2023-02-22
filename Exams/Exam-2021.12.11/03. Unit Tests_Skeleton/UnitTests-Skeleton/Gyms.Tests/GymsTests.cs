namespace Gyms.Tests
{
    using System;
    using NUnit.Framework;
    public class GymsTests
    {
        [Test]
        public void Test_Constructor()
        {
            Gym gym = new Gym("By Pesho", 12);

            Assert.IsNotNull(gym);
            Assert.AreEqual("By Pesho", gym.Name);
            Assert.AreEqual(12, gym.Capacity);
            Assert.AreEqual(0, gym.Count);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Constructor_InvalidName(string name)
        {
            Assert.Throws<ArgumentNullException>(() =>
            {
                Gym gym = new Gym(name, 12);
            }, "Invalid gym name.");
        }

        [TestCase(-1)]
        [TestCase(-1000)]
        public void Test_Constructor_InvalidCapacity(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Gym gym = new Gym("By Pesho", capacity);
            }, "Invalid gym capacity.");
        }

        [Test]
        public void Test_AddAthlete()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            Assert.IsNotNull(athlete1);
            Assert.IsNotNull(athlete2);

            Assert.AreEqual(2, gym.Count);
        }

        [Test]
        public void Test_AddAthlete_Exceptions()
        {
            Gym gym = new Gym("By Pesho", 2);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");
            Athlete athlete3 = new Athlete("Nasko");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.AddAthlete(athlete3);
            }, "The gym is full.");
        }

        [Test]
        public void Test_RemoveAthlete()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");
            Athlete athlete3 = new Athlete("Nasko");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.RemoveAthlete("Gosho");

            Assert.AreEqual(2, gym.Count);
        }

        [Test]
        public void Test_RemoveAthlete_Exceptions()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.RemoveAthlete("Soni");
            }, "The athlete Soni doesn't exist.");
        }

        [Test]
        public void Test_InjureAthlete()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            gym.AddAthlete(athlete1);

            gym.InjureAthlete("Pesho");

            bool expected = true;
            bool actual = athlete1.IsInjured;

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_InjureAthlete_Exceptions()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");
            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);

            Assert.Throws<InvalidOperationException>(() =>
            {
                gym.InjureAthlete("Stancho");
            }, "The athlete Stancho doesn't exist.");
        }

        [Test]
        public void Test_Report()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");
            Athlete athlete3 = new Athlete("Stancho");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            gym.RemoveAthlete("Stancho");
            gym.InjureAthlete("Gosho");

            string expected = $"Active athletes at By Pesho: Pesho";
            string actual = gym.Report();

            Assert.AreEqual(expected, actual);
            Assert.AreEqual(2, gym.Count);
        }
        [Test]
        public void Test_Report_Null()
        {
            Gym gym = new Gym("By Pesho", 0);

            string expected = $"Active athletes at By Pesho: ";
            string actual = gym.Report();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_Create_Athlete()
        {
            Athlete athlete = new Athlete("Pesho");

            Assert.AreEqual("Pesho", athlete.FullName);
            Assert.AreEqual(false, athlete.IsInjured);

        }

        [Test]
        public void Test_Count()
        {
            Gym gym = new Gym("By Pesho", 12);
            Athlete athlete1 = new Athlete("Pesho");
            Athlete athlete2 = new Athlete("Gosho");
            Athlete athlete3 = new Athlete("Stancho");

            gym.AddAthlete(athlete1);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);

            Assert.AreEqual(3, gym.Count);
        }


        // ============================

        [Test]
        public void Test_Gym_Injure_Athlete_Works()
        {
            Gym gym = new Gym("Gym", 3);

            var athlete = new Athlete("Gosho Bavniq");
            var athlete2 = new Athlete("Jorko Bavniq");
            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);

            var returnedAthlete = gym.InjureAthlete(athlete.FullName);

            Assert.AreEqual(true, athlete.IsInjured);
            Assert.AreSame(athlete, returnedAthlete);
        }

        
    }
}
