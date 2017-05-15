using ElectronicReaderLibrary;

using SpecflowTests.TestUtils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TechTalk.SpecFlow;

namespace SpecflowTests.Demo.BindingRestrictions
{
    [Binding]
    public sealed class BindingRestrictionsDemoSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        /// <summary>
        /// Initializes a new instance of the <see cref="BindingRestrictionsDemoSteps"/> class.
        /// </summary>
        public BindingRestrictionsDemoSteps()
        {
            this.reader = new ElectronicReader();
        }

        [Given(@"I have bought book '(.*)' in hard-cover")]
        public void GivenIHaveBoughtBookInHardCover(string title)
        {
            //// Logic for obtaining paper book
        }

        //// Default binding
        [Given(@"I have uploaded book '(.*)' to my electronic reader")]
        public void GivenIHaveUploadedBookToMyElectronicReader(string title)
        {
            this.reader.AddTheBookToTheReaderStorage(TestHelper.GetValidBookInfoWithTitle(title));

            Assert.AreEqual(
                "BindingRestrictionsDemoFeature",
                FeatureContext.Current.FeatureInfo.Title,
                "Incorrect Feature name for which step binding is being executed");

            Assert.AreEqual(
                "Reading the electronic book with the reader",
                ScenarioContext.Current.ScenarioInfo.Title,
                "Incorrect Scenario name for which step binding is being executed");
        }

        //// This is specific binding, restricted to certain Feature:
        [Given(@"I have uploaded book '(.*)' to my electronic reader")]
        [Scope(Feature = "BindingRestrictionsDemo another, advanced Feature")]
        public void GivenIHaveUploadedBookToMyElectronicReaderAdvanced(string title)
        {
            //// Here some advanced settings are being setup 
            //// in order to prepare book for uploading to electronic reader
            this.reader.AddTheBookToTheReaderStorage(TestHelper.GetValidBookInfoWithTitle(title));

            Assert.AreEqual(
                "BindingRestrictionsDemo another, advanced Feature",
                FeatureContext.Current.FeatureInfo.Title,
                "Incorrect Feature name for which step binding is being executed");

            Assert.AreEqual(
                "Reading the electronic book with the reader with advanced settings",
                ScenarioContext.Current.ScenarioInfo.Title,
                "Incorrect Scenario name for which step binding is being executed");
        }

        [Given(@"I turned on my electronic reader")]
        public void GivenITurnedOnMyElectronicReader()
        {
            this.reader.TurnOnReader();
        }

        //// This is default binding 
        [When(@"I opened the '(.*)' book")]
        public void WhenIOpenedTheBook(string title)
        {
            //// This is default binding method impementation
            this.reader.OpenTheBook(title);

            Assert.AreEqual(
                "BindingRestrictionsDemoFeature",
                FeatureContext.Current.FeatureInfo.Title,
                "Incorrect Feature name for which step binding is being executed");

            Assert.AreEqual(
                  "Reading the electronic book with the reader",
                  ScenarioContext.Current.ScenarioInfo.Title,
                  "Incorrect Scenario name for which step binding is being executed");
        }

        //// This is specific binding , restricted to certain Scenario:
        [Scope(Scenario = "Reading the paper book - scenario restriction")]
        [When(@"I opened the '(.*)' book")]
        public void WhenIOpenedTheBookAnotherBinding(string title)
        {
            //// Here we can write logic for opening paper book and paging

            Assert.AreEqual(
                  "BindingRestrictionsDemoFeature",
                  FeatureContext.Current.FeatureInfo.Title,
                  "Incorrect Feature name for which step binding is being executed");

            Assert.AreEqual(
                  "Reading the paper book - scenario restriction",
                  ScenarioContext.Current.ScenarioInfo.Title,
                  "Incorrect Scenario name for which step binding is being executed");
        }

        //// we use scenario tag to bind this method
        [Scope(Tag = "paperFunctionality")]
        [When(@"I opened the '(.*)' book")]
        public void WhenIOpenedTheBookAnotherBindingWithTag(string title)
        {
            //// Here we can write logic for opening paper book and paging

            Assert.AreEqual(
                  "BindingRestrictionsDemoFeature",
                  FeatureContext.Current.FeatureInfo.Title,
                  "Incorrect Feature name for which step binding is being executed");

            Assert.AreEqual(
                  "Reading the paper book - restriction with tag",
                  ScenarioContext.Current.ScenarioInfo.Title,
                  "Incorrect Scenario name for which step binding is being executed");
        }
    }
}
