using NUnit.Framework;
using System;
using System.Linq;
using System.Numerics;

namespace FootballTeam.Tests
{
    public class Tests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        

        [Test]
        public void Test_Player_Constructor()
        {
            FootballPlayer player = new FootballPlayer("Gonzo", 8, "Goalkeeper");

            Assert.That(player.Name, Is.EqualTo("Gonzo"));
            Assert.That(player.PlayerNumber, Is.EqualTo(8));
            Assert.That(player.Position, Is.EqualTo("Goalkeeper"));
            Assert.That(player.ScoredGoals, Is.EqualTo(0));
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Player_Name_Exceptions(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballPlayer player = new FootballPlayer(name, 8, "Goalkeeper");
            }, "Name cannot be null or empty!");
        }

        [TestCase(0)]
        [TestCase(22)]
        public void Test_Player_Number_Exceptions(int number)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballPlayer player = new FootballPlayer("Gonzo", number, "Goalkeeper");
            }, "Player number must be in range [1,21]");
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase("hjdhfjd")]

        public void Test_Player_Position_Exceptions(string position)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballPlayer player = new FootballPlayer("Gonzo", 8, position);
            }, "Invalid Position");
        }

        [Test]
        public void Test_Player_Score_And_ScoredGoals()
        {
            FootballPlayer player = new FootballPlayer("Gonzo", 8, "Goalkeeper");

            player.Score();

            Assert.That(player.ScoredGoals, Is.EqualTo(1));
        }

        // ----------

        [Test]
        public void Test_Constructor()
        {
            FootballTeam team = new FootballTeam("DreamTeam", 22);

            Assert.IsNotNull(team);
            Assert.That(team.Name, Is.EqualTo("DreamTeam"));
            Assert.That(team.Capacity, Is.EqualTo(22));
            Assert.That(team.Players.Count, Is.EqualTo(0));

        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_Constructor_Name_Exceptions(string name)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballTeam team = new FootballTeam(name, 22);
            }, "Name cannot be null or empty!");
        }

        [TestCase(-20)]
        [TestCase(14)]
        public void Test_Constructor_Capacity_Exceptions(int capacity)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                FootballTeam team = new FootballTeam("DreamTeam", capacity);
            }, "Capacity min value = 15");
        }

        [Test]
        public void Test_Team_AddPlayer()
        {
            FootballTeam team = new FootballTeam("DreamTeam", 22);
            FootballPlayer player = new FootballPlayer("Gonzo", 8, "Goalkeeper");

            team.AddNewPlayer(player);

            FootballPlayer actual = team.Players.FirstOrDefault(x => x.Name == "Gonzo");

            Assert.That(actual, Is.EqualTo(player));

        }

        [Test]
        public void Test_Team_AddPlayer_Result()
        {
            FootballTeam team = new FootballTeam("DreamTeam", 22);
            FootballPlayer player = new FootballPlayer("Gonzo", 8, "Goalkeeper");

            var expected = "Added player Gonzo in position Goalkeeper with number 8";
            var result = team.AddNewPlayer(player);

            Assert.That(result, Is.EqualTo(expected));

        }

        [Test]
        public void Test_Team_AddPlayer_Exceptions()
        {
            FootballTeam team = new FootballTeam("DreamTeam", 22);
            FootballPlayer gonzo = new FootballPlayer("Gonzo33", 8, "Goalkeeper");
            for (int i = 0; i < 22; i++)
            {
                team.AddNewPlayer(new FootballPlayer("Gonzo" + i, 8, "Goalkeeper"));
            }

            var expected = "No more positions available!";
            var result = team.AddNewPlayer(gonzo); 

            Assert.That(result, Is.EqualTo(expected));
        }


        [Test]
        public void Test_PickPlayer()
        {
            FootballTeam team = new FootballTeam("DreamTeam", 22);
            FootballPlayer player1 = new FootballPlayer("Gonzo", 8, "Goalkeeper");
            FootballPlayer player2 = new FootballPlayer("Pesho", 4, "Goalkeeper");

            team.AddNewPlayer(player1);
            team.AddNewPlayer(player2);

            FootballPlayer expected = team.PickPlayer("Gonzo");

            Assert.That(expected, Is.EqualTo(player1));
        }

        [Test]
        public void Test_PlayScore()
        {
            FootballTeam team = new FootballTeam("DreamTeam", 22);
            FootballPlayer player1 = new FootballPlayer("Gonzo", 8, "Goalkeeper");
            FootballPlayer player2 = new FootballPlayer("Pesho", 4, "Goalkeeper");

            team.AddNewPlayer(player1);
            team.AddNewPlayer(player2);

            var expected = team.PlayerScore(8);
            var actual = "Gonzo scored and now has 1 for this season!";

            Assert.That(expected, Is.EqualTo(actual));
        }

    }
}