namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        private Person person;
        private Database db;


        [Test]
        public void ConstructorOfPersonShoulBeCorrect()
        {
            person = new Person(123, "Pesho");
            Assert.AreEqual(123, person.Id);
            Assert.AreEqual("Pesho", person.UserName);

        }

        [TestCase(1)]
        [TestCase(16)]
        public void ConstructorOfDatabaseShouldBeCorrect(int count)
        {
            Database db = new Database(CreatePersonsArr(count));

            int expectedCount = count;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(17)]
        public void ConstructorThrowExeptionDataLenghtOver16(int count)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Database db = new Database(CreatePersonsArr(count));
            }, "Provided data length should be in range [0..16]!");
        }


        [TestCase(0)]
        [TestCase(15)]
        public void AddMethodShouldAddPersonAtLastPosition(int count)
        {
            Database db = new Database(CreatePersonsArr(count));
            Person person = new Person(1223, "Pesho");

            db.Add(person);
            int expectedCount = count + 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount, "");
        }

        [TestCase(16)]
        public void AddMethodThrowExeptionWhenAddElementAfterPosition16(int count)
        {
            db = new Database(CreatePersonsArr(count));
            Person person = new Person(1223, "Pesho");

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(person);
            }, "Array's capacity must be exactly 16 integers!");
        }

        [TestCase(2)]
        public void AddMethodThrowExeptionWhenAddPersonWithExistingUsername(int count)
        {
            db = new Database(CreatePersonsArr(count));
            Person person = new Person(123456, "Person_1");

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(person);
            }, "There is already user with this username!");
        }

        [TestCase(2)]
        public void AddMethodThrowExeptionWhenAddPersonWithExistingId(int count)
        {
            db = new Database(CreatePersonsArr(count));
            Person person = new Person(1, "No_such_a_name");

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(person);
            }, "There is already user with this Id!");
        }

        [TestCase(1)]
        [TestCase(16)]
        public void RemoveElementAtLastIndex(int count)
        {
            Database db = new Database(CreatePersonsArr(count));
            db.Remove();

            int expectedCount = count - 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(0)]
        public void RemoveElementFromEmptyDatabase(int count)
        {
            Database db = new Database(CreatePersonsArr(count));

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Remove();
            }, "The collection is empty!");
        }


        [TestCase(6)]
        public void FindPersonByUsername(long count)
        {
            Database db = new Database(CreatePersonsArr(6));
            Person personToFind = new Person(count, "Pesho_" + count);
            db.Add(personToFind);

            Person expectedPerson = personToFind;
            Person actualPerson = db.FindByUsername("Pesho_" + count);

            Assert.AreEqual(expectedPerson, actualPerson);
        }

        [TestCase(6)]
        public void FindPersonThrowExceptionUsenameIsNull(long count)
        {
            Database db = new Database(CreatePersonsArr(6));
            Person personToFind = new Person(count, "Pesho_" + count);
            db.Add(personToFind);

            Assert.Throws<ArgumentNullException>(() =>
            {
                db.FindByUsername("");
            }, "Username parameter is null!");
        }

        [TestCase("Gosho")]
        public void FindPersonThrowExceptionNoSuchUsernamel(string username)
        {
            
            Person personToFind = new Person(6, username);
            this.db.Add(personToFind);

            Assert.Throws<InvalidOperationException>(() =>
            {
                this.db.FindByUsername(username.ToLower());
            }, "No user is present by this username!");
            Assert.Throws<InvalidOperationException>(() =>
            {
                this.db.FindByUsername(username.ToUpper());
            }, "No user is present by this username!");
        }


         [TestCase(10)]
        public void FindPersonById(long id)
        {
            Person personToFind = new Person(id, "Pesho_6");
            this.db.Add(personToFind);

            Person expectedPerson = personToFind;
            Person actualPerson = this.db.FindById(id);

            Assert.AreEqual(expectedPerson, actualPerson);
        }

        [TestCase(-6)]
        public void FindPersonThrowExceptionIdIsNegativ(long count)
        {

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                db.FindById(count);
            }, "Id should be a positive number!");
        }

        [TestCase(123)]
        public void FindPersonThrowExceptionIdIsNotPresent(long count)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                db.FindById(count);
            }, "No user is present by this ID!");
        }

       

        public static Person[] CreatePersonsArr(int count)
        {
            Person[] persons = new Person[count];
            for (int i = 0; i < count; i++)
            {
                persons[i] = new Person(i, "Person_" + i);
            }
            return persons;
        }
    }
}