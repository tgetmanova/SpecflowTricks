using System.Linq;

using ElectronicReaderLibrary;
using ElectronicReaderLibrary.Data;
using SpecflowTests.TestUtils;

using TechTalk.SpecFlow;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SpecflowTests.Demo.Tags
{
    [Binding]
    public sealed class CategorizeAndFilterWithTagsDemoSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        [Given(@"I have electronic reader with book '(.*)'")]
        public void GivenIHaveElectronicReaderWithBook(string title)
        {
            this.reader = new ElectronicReader();
            var book = TestHelper.GetValidBookInfoWithTitle(title);
            this.reader.AddTheBookToTheReaderStorage(book);
        }

        [Given(@"I turned on my reader")]
        public void GivenITurnedOnMyReader()
        {
            this.reader.TurnOnReader();
        }

        [When(@"I open book '(.*)'")]
        public void WhenIOpenBook(string title)
        {
            this.reader.OpenTheBook(title);
        }

        [Then(@"Book '(.*)' should be opened")]
        public void ThenBookShouldBeOpened(string title)
        {
            var book = this.reader.RetrieveListOfBooks().SingleOrDefault(i => i.Title.Equals(title, System.StringComparison.OrdinalIgnoreCase));

            Assert.IsNotNull(book, "Failed to retrieve book by title");

            Assert.AreEqual(BookStateInReader.OpenedForReading, book.StateInReader, "The book should be opeed for readin");
        }

        [Then(@"Feature tags contain '(.*)'")]
        public void ThenFeatureTagsContain(string expectedTagsArrayString)
        {
            var expectedTags = expectedTagsArrayString.Split(',');

            var actualFeatureTags = FeatureContext.Current.FeatureInfo.Tags;

            Assert.IsTrue(expectedTags.All(et => actualFeatureTags.Contains(et)), "The Feature tags don't contain at least one expected tag");
        }

        [Then(@"Scenario tags contain '(.*)'")]
        public void ThenScenarioTagsContain(string expectedTagsArrayString)
        {
            var expectedTags = expectedTagsArrayString.Split(',');

            var actualScenarioTags = ScenarioContext.Current.ScenarioInfo.Tags;

            Assert.IsTrue(expectedTags.All(et => actualScenarioTags.Contains(et)), "The Scenario tags don't contain at least one expected tag");
        }

        [Then(@"if Scenario example '(.*)' is '(.*)' then Scenario tags also contain '(.*)'")]
        public void ThenIfScenarionExampleIsThenScenarioTagsAlsoContain(string currentExample, string expectedExample, string expectedExampleLevelTag)
        {
            var actualScenarioTags = ScenarioContext.Current.ScenarioInfo.Tags;

            if (currentExample == expectedExample)
            {
                CollectionAssert.Contains(actualScenarioTags, expectedExampleLevelTag, "Example-level tag is expected");
            }
        }
    }
}
