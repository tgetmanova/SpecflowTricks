using System.Linq;

using ElectronicReaderLibrary;
using SpecflowTests.TestUtils;

using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.DataContextManagement
{
    /// <summary>
    /// The is classs binding for the steps related to the 
    /// "adding new book" functionality and the corresponding test cases
    /// </summary>
    [Binding]
    public sealed class DataContextManagementAddBookSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextManagementAddBookSteps"/> class.
        /// !!! Pay attention: Constructor injection for ElectronicReader
        /// </summary>
        /// <param name="reader">The reader.</param>
        public DataContextManagementAddBookSteps(ElectronicReader reader)
        {
            this.reader = reader;
        }

        [When(@"I add book with title '(.*)'")]
        public void WhenIAddBookWithTitle(string title)
        {
            var book = TestHelper.GetValidBookInfoWithTitle(title);
            this.reader.AddTheBookToTheReaderStorage(book);
        }

        [Then(@"book '(.*)' is added to the reader")]
        public void ThenBookIsAddedToTheReader(string title)
        {
            var books = this.reader.RetrieveListOfBooks();
            var newlyAddedBook = books.FirstOrDefault(b => b.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(newlyAddedBook, "The expected book is not added to the reader stoorage");
        }
    }
}
