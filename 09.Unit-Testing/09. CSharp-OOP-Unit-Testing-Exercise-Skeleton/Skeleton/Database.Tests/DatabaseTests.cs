namespace Database.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class DatabaseTests
    {
        private Database db;

        [SetUp]
        public void Setup()
        {
            db = new Database();
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void ConstructorShouldSetDataCorrectly(int[] data)
        {
            Database db = new Database(data);

            int expectedCount = data.Length;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19 })]
        public void ConstructorShouldThrowExceptionWhenInputDataIsAbove16(int[] data)
        {

            Assert.Throws<InvalidOperationException>(() =>
            {
                Database db = new Database(data);
            }, "Array's capacity must be exactly 16 integers!");
        }

        [TestCase(new int[] { })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15 })]
        public void AddElementAtNextFreeCell(int[] data)
        {
            Database db = new Database(data);
            db.Add(1);

            int expectedCount = data.Length + 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void AddElementOver16(int[] data)
        {
            Database db = new Database(data);

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Add(1);
            }, "Array's capacity must be exactly 16 integers!");
        }

        [TestCase(new int[] { 1 })]
        [TestCase(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 })]
        public void RemoveElementAtLastIndex(int[] data)
        {
            Database db = new Database(data);
            db.Remove();

            int expectedCount = data.Length - 1;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);
        }

        [Test]
        public void RemoveElementFromEmptyDatabase()
        {
            Database db = new Database(new int[] { });

            Assert.Throws<InvalidOperationException>(() =>
            {
                db.Remove();
            }, "The collection is empty!");
        }

        [Test]
        public void FetchShouldReturnTheElementsEsEnErray()
        {
            Database db = new Database(new int[] { 1, 2, 3, 4, 5, 6 });
            int[] coppyArray = db.Fetch();

            int expectedCount = coppyArray.Length;
            int actualCount = db.Count;

            Assert.AreEqual(expectedCount, actualCount);

        }

    }

}
