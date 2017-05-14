using System.Linq;

using ElectronicReaderLibrary;
using ElectronicReaderLibrary.Data;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.Demo.DataTable
{
    [Binding]
    public sealed class DataTableDemoSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        [Given(@"I have electronic reader")]
        public void GivenIHaveElectronicReader()
        {
            this.reader = new ElectronicReader();
        }

        [When(@"I add new book with properties")]
        public void WhenIAddNewBookWithProperties(Table table)
        {
            //// Using Specflow.Asist CreateInstance:
            var bookFromTable = table.CreateInstance<BookInfo>();

            var book = new BookInfo
            {
                Title = bookFromTable.Title,
                Author = bookFromTable.Author,
                NumberOfPages = bookFromTable.NumberOfPages,
                StateInReader = BookStateInReader.Unloaded,
                ElectronicInfo = new ElectronicInfo
                {
                    DataFormat = DataFormat.Epub,
                    SizeInMegabytes = 67
                }
            };

            this.reader.AddTheBookToTheReaderStorage(book);
        }

        [Then(@"new book '(.*)' should be added to the reader")]
        public void ThenNewBookShouldBeAddedToTheReader(string title)
        {
            var actualBook = this.reader.RetrieveListOfBooks().FirstOrDefault(i => i.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(actualBook, "Failed to retrieve book by expected Title after adding it to the reader");
        }

        [Then(@"new book should be added to the reader")]
        public void ThenNewBookShouldBeAddedToTheReader(Table table)
        {
            //// Using Specflow.Asist CreateInstance:
            var expectedBook = table.CreateInstance<BookInfo>();

            var actualBook = this.reader.RetrieveListOfBooks().FirstOrDefault(i => i.Title.Equals(expectedBook.Title, System.StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(actualBook, "Failed to retrieve book by expected Title after adding it to the reader");

            Assert.AreEqual(expectedBook.Author, actualBook.Author, "Newlt added book: incorrect Author");
            Assert.AreEqual(expectedBook.NumberOfPages, actualBook.NumberOfPages, "Newlt added book: incorrect NumberOfPages");
        }

        //// Here we can directly use BookInfo type as parameter since we have Specflow StepsArgumentTransformation
        //// binding from table to BookInfo that will be executed BEFORE this step:
        [When(@"I add new book with all of properties")]
        public void WhenIAddNewBookWithAllOfProperties(BookInfo bookInfo)
        {
            this.reader.AddTheBookToTheReaderStorage(bookInfo);
        }

        [When(@"I add several books with properties")]
        public void WhenIAddSeveralBooksWithProperties(Table table)
        {
            //// Using Specflow.Asist CreateSet:
            var booksFromTable = table.CreateSet<BookInfo>();

           foreach(var bookFromTable in booksFromTable )
            {
                var book = new BookInfo
                {
                    Title = bookFromTable.Title,
                    Author = bookFromTable.Author,
                    NumberOfPages = bookFromTable.NumberOfPages,
                    StateInReader = BookStateInReader.Unloaded,
                    ElectronicInfo = new ElectronicInfo
                    {
                        DataFormat = DataFormat.Pdf,
                        SizeInMegabytes = 67
                    }
                };

                this.reader.AddTheBookToTheReaderStorage(book);
            }
        }

        [Then(@"new books should be added to the reader")]
        public void ThenNewBooksShouldBeAddedToTheReader(Table table)
        {
            //// Using Specflow.Asist CreateSet:
            var expectedBooks = table.CreateSet<BookInfo>();

           foreach (var expectedBook in expectedBooks)
            {
                var actualBook = this.reader.RetrieveListOfBooks().FirstOrDefault(i => i.Title.Equals(expectedBook.Title, System.StringComparison.OrdinalIgnoreCase));

                Assert.IsNotNull(actualBook, "Failed to retrieve book by expected Title after adding it to the reader");

                Assert.AreEqual(expectedBook.Author, actualBook.Author, "Newlt added book: incorrect Author");
                Assert.AreEqual(expectedBook.NumberOfPages, actualBook.NumberOfPages, "Newlt added book: incorrect NumberOfPages");
            }
        }
    }
}
