using System.Linq;

using ElectronicReaderLibrary;
using ElectronicReaderLibrary.Data;

using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.DataContextManagement
{
    /// <summary>
    /// The is classs binding for the steps related to the 
    /// "opening book" functionality and the corresponding test cases
    /// </summary>
    [Binding]
    public sealed class DataContextManagementOpenBookSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextManagementOpenBookSteps"/> class.
        /// !!! Pay attention: Constructor injection for ElectronicReader
        /// </summary>
        /// <param name="reader">The reader.</param>
        public DataContextManagementOpenBookSteps(ElectronicReader reader)
        {
            this.reader = reader;
        }

        [When(@"I open the book '(.*)'")]
        public void WhenIOpenTheBook(string title)
        {
            this.reader.OpenTheBook(title);
        }

        [Then(@"book '(.*)' is opened in the reader")]
        public void ThenBookIsOpenedInTheReader(string title)
        {
            var books = this.reader.RetrieveListOfBooks();
            var openedBook = books.FirstOrDefault(b =>
                b.StateInReader == BookStateInReader.OpenedForReading
                && b.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(openedBook, "The expected book is not opened for reading");
        }
    }
}
