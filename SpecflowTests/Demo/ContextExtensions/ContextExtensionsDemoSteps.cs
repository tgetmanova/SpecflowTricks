using System.Linq;

using ElectronicReaderLibrary;
using ElectronicReaderLibrary.Data;

using TechTalk.SpecFlow;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.Demo.ContextExtensions
{
    [Binding]
    public sealed class ContextExtensionsDemoSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        //// Use Feature-level context extension to setup ElectronicReaderService
        //// before feature so that service lives for all scenarios in this Feature 
        //// till the end of the last scenario in Feature:
        [BeforeFeature("ContextExtensionsDemoFeatureTag")]
        [Given(@"I have electronic reader")]
        public static void SetupElectronicReaderServiceinFeatureContext()
        {
            FeatureContext.Current.SetElectronicReaderService();
        }

        [Scope(Feature = "ContextExtensionsDemoFeature")]
        [Given(@"I have electronic reader")]
        public void GivenIHaveElectronicReader()
        {
            //// Use Feature-level context extension to retrieve ElectronicReaderService 
            //// and invoke method on it:
            this.reader = FeatureContext.Current.GetElectronicReaderService().GetReader();
        }

        [Given(@"I want to add new book with properties title '(.*)', author '(.*)', count of pages '(.*)'")]
        public void GivenIWantToAddNewBookWithPropertiesTitleAuthorCountOfPages(string title, string author, int pagesCount)
        {
            var book = new BookInfo
            {
                Title = title,
                Author = author,
                NumberOfPages = pagesCount,
                StateInReader = BookStateInReader.Unloaded,
                ElectronicInfo = new ElectronicInfo
                {
                    DataFormat = DataFormat.Epub,
                    SizeInMegabytes = 67
                }
            };

            //// Convenient way to set data to context with C# extensions methods against Scenario context
            ScenarioContext.Current.SetExpectedBookInfo(book);
        }

        [When(@"I add this book to the reader")]
        public void WhenIAddThisBookToTheReader()
        {
            //// Convenient way to retrieve and use data from the Scenario context extension:
            this.reader.AddTheBookToTheReaderStorage(ScenarioContext.Current.GetExpectedBookInfo());
        }

        [Then(@"new book should be added to the reader with correct properties")]
        public void ThenNewBookShouldBeAddedToTheReaderWithCorrectProperties()
        {
            //// Convenient way to retrieve and use data from the Scenario context extension:
            var expectedProperties = ScenarioContext.Current.GetExpectedBookInfo();

            var actualBook = this.reader.RetrieveListOfBooks()
                .FirstOrDefault(b => b.Title.Equals(expectedProperties.Title, System.StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(actualBook, $"Failed to retreieve book by expected title {expectedProperties.Title}");

            Assert.AreEqual(expectedProperties.Author, actualBook.Author, "Incorrect Author");
            Assert.AreEqual(expectedProperties.NumberOfPages, actualBook.NumberOfPages, "Incorrect count of pages");
        }
    }
}
