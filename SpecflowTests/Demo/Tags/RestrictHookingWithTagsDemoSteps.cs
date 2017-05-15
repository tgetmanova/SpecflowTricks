using System.Linq;

using ElectronicReaderLibrary;
using SpecflowTests.TestUtils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TechTalk.SpecFlow;

namespace SpecflowTests.Demo.Tags
{
    [Binding]
    public sealed class RestrictHookingWithTagsDemoSteps
    {
        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        /// <summary>
        /// The reader service
        /// </summary>
        private static ElectronicReaderService readerService;

        //// Before feature tagged as 'InitializeReaderService'
        [BeforeFeature("InitializeReaderService")]
        public static void SetupElectronicReaderService()
        {
            readerService = new ElectronicReaderService();
        }

        //// Before 'SetupReader' tagged (at the Feature-leve) scenario, 1st setup step - initialize device:
        [BeforeScenario("SetupReader", Order = 1)]
        public void SetupElectronicReaderStep1()
        {
            Assert.IsNotNull(readerService, "The reader service has not been instantiated with BeforeFeature hook");

            this.reader = readerService.GetReader();
        }

        //// Before 'SetupReader' tagged (at the Feature-level) scenario, 2nd setup step - turn on device:
        [BeforeScenario("SetupReader", Order = 2)]
        public void SetupElectronicReaderStep2()
        {
            Assert.IsNotNull(this.reader, "The reader has not been instantiated with BeforeScenario hook");

            this.reader.TurnOnReader();
        }

        //// Before 'ensureSomeBookExists'/addSomeBook tagged scenario:
        [BeforeScenario("ensureSomeBookExists", "addSomeBook")]
        public void SetupSomeBookInTheReader()
        {
            Assert.IsNotNull(readerService, "The reader service has not been instantiated with BeforeFeature hook");
            Assert.IsNotNull(this.reader, "The reader has not been instantiated with BeforeScenario hook");

            this.reader.AddTheBookToTheReaderStorage(TestHelper.GetValidBookInfoWithTitle("RandomBook"));
        }

        //// After scenario tagged as 'resetReader'
        [AfterScenario("resetReader")]
        public void ResetElectronicReaderService()
        {
            this.reader = null;
        }

        //// After all scenarios
        [AfterScenario]
        public void AfterAllScenarios()
        {
            ////  After all scenarios
        }

        //// Before all features
        [BeforeFeature]
        public static void BeforeAllFeatures()
        {
            //// Before all features
        }

        [Given(@"I have an excellent electronic reader")]
        public void GivenIHaveAnExcellentElectronicReader()
        {
            Assert.IsNotNull(this.reader, "The reader has not been instantiated with BeforeScenario hook");
        }

        [Given(@"I have uploaded new book to the reader")]
        public void GivenIHaveUploadedNewBookToTheReader()
        {
            this.reader.AddTheBookToTheReaderStorage(TestHelper.GetValidBookInfoWithTitle("randomTitle"));
        }

        //// !! Pay attention - we can use multiple bindings and even of different Specflow blocks (Given,Then,When)
        [Given(@"my reader has at least one book")]
        [Then(@"my reader has at least one book")]
        public void ThenMyReaderHasAtLeastOneBook()
        {
            var books = this.reader.RetrieveListOfBooks();

            Assert.IsTrue(books.Any(), "No books but expected at least one");
        }

        [Then(@"I can read some book")]
        public void ThenICanReadSomeBook()
        {
            this.reader.OpenTheBook(this.reader.RetrieveListOfBooks().First().Title);
        }

        [Then(@"I can move some book to trash")]
        public void ThenICanMoveSomeBookToTrash()
        {
            //// Delete book
        }
    }
}
