namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void Test_Constuctor()
        {
            Book book = new Book("It", "Steven King");

            Assert.AreEqual("It", book.BookName);
            Assert.AreEqual("Steven King", book.Author);
            Assert.AreEqual(0, book.FootnoteCount);
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_BookName_Exceptions(string bookName) 
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book(bookName, "Steven King"); 
            }, $"Invalid {bookName}!"); // ????????????
        }

        [TestCase(null)]
        [TestCase("")]
        public void Test_BookAuthor_Exceptions(string bookAuthor)
        {
            Assert.Throws<ArgumentException>(() =>
            {
                Book book = new Book("It", bookAuthor);
            }, $"Invalid {bookAuthor}!"); // ????????????
        }

        [Test]
        public void Test_AddFootnote()
        {
            Book book = new Book("It", "Steven King");
            book.AddFootnote(12, "This is footnote text for number 12");
            book.AddFootnote(6, "This is footnote text for number 6");

            Assert.AreEqual(2, book.FootnoteCount);
        }

        [Test]
        public void Test_AddFootnote_Exceptions()
        {
            Book book = new Book("It", "Steven King");
            book.AddFootnote(12, "This is footnote text for number 12");
            book.AddFootnote(6, "This is footnote text for number 6");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.AddFootnote(12, "This is footnote text for number 12");
            }, "Footnote already exists!");
        }

        [TestCase(12)]
        [TestCase(6)]
        [TestCase(1)]
        public void Test_FindFootnote(int number)
        {
            Book book = new Book("It", "Steven King");
            book.AddFootnote(number, $"This is footnote text for number {number}");

            string expected = $"Footnote #{number}: This is footnote text for number {number}";
            string actual = book.FindFootnote(number);

            Assert.AreEqual(expected, actual);  
        }

        [TestCase(42)]
        [TestCase(66)]
        [TestCase(-100)]
        public void Test_FindFootnate_Exceptions(int number)
        {
            Book book = new Book("It", "Steven King");
            book.AddFootnote(12, "This is footnote text for number 12");
            book.AddFootnote(6, "This is footnote text for number 6");

            Assert.Throws<InvalidOperationException>(() =>
            {
                book.FindFootnote(number);
            }, "Footnote doesn't exists!");
        }

        [Test]
        public void Test_AlterFootnote()
        {
            Book book = new Book("It", "Steven King");
            book.AddFootnote(12, "This is footnote text for number 12");
            book.AddFootnote(6, "This is footnote text for number 6");

            book.AlterFootnote(12, "new text");

            string expected = $"Footnote #12: new text";
            string actual = book.FindFootnote(12);

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Test_AlterFootnote_Exceptions()
        {
            Book book = new Book("It", "Steven King");
            book.AddFootnote(12, "This is footnote text for number 12");
            book.AddFootnote(6, "This is footnote text for number 6");

            Assert.Throws(typeof(InvalidOperationException), () =>
            {
                book.AlterFootnote(62, "new text");
            }, "Footnote does not exists!");

        }

    }
}