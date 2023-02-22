namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System.Text;

    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test_TextBook()
        {
            TextBook book = new TextBook("To", "King", "Horor");
            
            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Book: To - 0");
            sb.AppendLine($"Category: Horor");
            sb.AppendLine($"Author: King");

            var expected = sb.ToString().TrimEnd();
            var actual = book.ToString();

            Assert.That(actual, Is.EqualTo(expected));


            Assert.IsNotNull(book);
            Assert.That(book.Title, Is.EqualTo("To"));
            Assert.That(book.Author, Is.EqualTo("King"));
            Assert.That(book.Category, Is.EqualTo("Horor"));
            Assert.That(book.Holder, Is.EqualTo(null));
            Assert.That(book.InventoryNumber, Is.EqualTo(0));
        }

       

        [Test]
        public void Test_Library()
        {
            UniversityLibrary library = new UniversityLibrary();

            Assert.IsNotNull(library);
            Assert.That(library.Catalogue.Count, Is.EqualTo(0));

        }

        [Test]
        public void Test_Library_AddTextBook()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");

            var actual = library.AddTextBookToLibrary(book);

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Book: To - 1");
            sb.AppendLine($"Category: Horor");
            sb.AppendLine($"Author: King");

            var expected = sb.ToString().TrimEnd();

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(library.Catalogue.Count, Is.EqualTo(1));
            Assert.That(book.InventoryNumber, Is.EqualTo(1));
        }

        [Test]
        public void Test_LoanTextBook()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            library.AddTextBookToLibrary(book);

            var actual = library.LoanTextBook(1, "Pesho");
            var expected = "To loaned to Pesho.";

            Assert.That(library.Catalogue.Count, Is.EqualTo(1));
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(book.Holder, Is.EqualTo("Pesho"));
        }


        [Test]
        public void Test_LoanTextBook_To_Other()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, "Pesho");

            var actual = library.LoanTextBook(1, "Gosho");
            var expected = "To loaned to Gosho.";

            Assert.That(library.Catalogue.Count, Is.EqualTo(1));
            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(book.Holder, Is.EqualTo("Gosho"));
        }

        [Test]
        public void Test_LoanTextBook_Not_Returned()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            library.AddTextBookToLibrary(book);

            library.LoanTextBook(1, "Pesho");
            var actual = library.LoanTextBook(1, "Pesho");
            var expected = "Pesho still hasn't returned To!";

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(book.Holder, Is.EqualTo("Pesho"));
        }

        [Test]
        public void Test_LoanTextBook_Not_Returned_Empty()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            library.AddTextBookToLibrary(book);

            library.LoanTextBook(1, "");
            var actual = library.LoanTextBook(1, "");
            var expected = " still hasn't returned To!";

            Assert.That(actual, Is.EqualTo(expected));
            Assert.That(book.Holder, Is.EqualTo(""));
        }


        [Test]
        public void Test_ReturnTextBook()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, "Pesho");

            library.ReturnTextBook(1);

            var expected = "To is returned to the library.";

            Assert.That(book.Holder, Is.EqualTo(""));
            Assert.That(book.Holder, Is.EqualTo(string.Empty));
            Assert.That(book.Holder, Is.EqualTo(string.Empty));

        }


        [Test]
        public void Test_ReturnTextBook_Expected()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            library.AddTextBookToLibrary(book);
            library.LoanTextBook(1, "Pesho");

            var actual = library.ReturnTextBook(1);

            var expected = "To is returned to the library.";

            Assert.That(expected, Is.EqualTo(actual));

        }

        [Test]
        public void Test_Catalog_Count()
        {
            UniversityLibrary library = new UniversityLibrary();
            TextBook book = new TextBook("To", "King", "Horor");
            TextBook book2 = new TextBook("It", "Spilberg", "Sci-Fi");
            library.AddTextBookToLibrary(book);
            library.AddTextBookToLibrary(book2);


            Assert.That(library.Catalogue.Count, Is.EqualTo(2)) ;
        }

    }
}