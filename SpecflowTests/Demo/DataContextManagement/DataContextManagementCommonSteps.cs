using ElectronicReaderLibrary;
using SpecflowTests.TestUtils;

using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.DataContextManagement
{
    /// <summary>
    /// The is classs binding for the steps related to the 
    /// general e-reader functionality and the corresponding test cases 
    /// like turn on/turn off the reader
    /// </summary>
    [Binding]
    public sealed class DataContextManagementCommonSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextManagementCommonSteps"/> class.
        /// !!! Pay attention: Constructor injection for ElectronicReader
        /// </summary>
        /// <param name="reader">The reader.</param>
        public DataContextManagementCommonSteps(ElectronicReader reader)
        {
            this.reader = reader;
        }

        [Given(@"I have my electronic reader")]
        public void GivenIHaveElectronicReader()
        {
            Assert.IsNotNull(this.reader, "Failed to resolve reader from object container");
        }

        [Given(@"I turned my reader on")]
        public void GivenITurnedOnTheReader()
        {
            this.reader.TurnOnReader();
        }

        [Given(@"I have added book with title '(.*)' in the reader")]
        public void GivenIHaveBookWithTitleInTheReader(string title)
        {
            var book = TestHelper.GetValidBookInfoWithTitle(title);
            this.reader.AddTheBookToTheReaderStorage(book);
        }
    }
}
