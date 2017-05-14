using System;
using System.Collections.Generic;

using ElectronicReaderLibrary.Data;
using SpecflowTests.TestUtils;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using TechTalk.SpecFlow;

namespace SpecflowTests.StringFormatting
{
    [Binding]
    public sealed class StringFormattingDemoSteps
    {
        /// <summary>
        /// The exception
        /// </summary>
        private Exception exception;

        /// <summary>
        /// The reader
        /// </summary>
        private ElectronicReader reader;

        [Given(@"I have electronic reader with only one book '(.*)'")]
        public void GivenIHaveElectronicReaderWithOnlyOneBook(string title)
        {
            this.reader = new ElectronicReader(new List<BookInfo>{ TestHelper.GetValidBookInfoWithTitle(title) });
        }

        [Given(@"I turned on the reader")]
        public void GivenITurnedOnTheReader()
        {
            this.reader.TurnOnReader();
        }

        [Given(@"I opened the '(.*)' book")]
        public void GivenIOpenedTheBook(string title)
        {
            this.reader.OpenTheBook(title);
        }

        [Given(@"I added '(.*)' book to the reader")]
        public void GivenIAddedBookToTheReader(string title)
        {
            this.reader.AddTheBookToTheReaderStorage(TestHelper.GetValidBookInfoWithTitle(title));
        }

        [When(@"I try to open book with random title")]
        public void WhenITryToOpenBookWithRandomTitle()
        {
            var randomTitle = DateTime.Now.Ticks.ToString();

            try
            {
                this.reader.OpenTheBook(randomTitle);
            }
            catch (Exception exception)
            {
                this.exception = exception;
            }

            ScenarioContext.Current.Add("firstScenarioPlaceholder", randomTitle);
        }

        [When(@"I try to open book '(.*)'")]
        public void WhenITryToOpenBook(string title)
        {
            try
            {
                this.reader.OpenTheBook(title);
            }
            catch (Exception exception)
            {
                this.exception = exception;
            }
        }

        [Given(@"I want to add book with title '(.*)', author '(.*)', number of pages '(.*)' to the reader")]
        public void GivenIWantToAddBookWithTitleAuthorNumberOfPagesToTheReader(string title, string author, int pagesCount)
        {
            var bookToAdd = new BookInfo
            {
                Title = title,
                Author = author,
                NumberOfPages = pagesCount,
                ElectronicInfo = new ElectronicInfo
                {
                    DataFormat = DataFormat.Djvu,
                    SizeInMegabytes = 24
                },
                StateInReader = BookStateInReader.Unloaded
            };

            ScenarioContext.Current.Add(nameof(bookToAdd), bookToAdd);
        }

        [When(@"I try to add book to the reader")]
        public void WhenITryToAddBookToTheReader()
        {
            var bookToAdd = ScenarioContext.Current.Get<BookInfo>("bookToAdd");

            try
            {
                this.reader.AddTheBookToTheReaderStorage(bookToAdd);
            }
            catch(Exception exception)
            {
                this.exception = exception;
            }
        }


        [Then(@"I get validation error that contains '(.*)'")]
        public void ThenIGetValidationErrorThatContainsTheHistoryOfHungary(string expectedValidationMessage)
        {
            StringAssert.Contains(exception.Message, expectedValidationMessage, "Incorrect validation message");
        }

        [Then(@"I get validation error formatted as '(.*)'")]
        public void ThenIGetValidationErrorFormattedAs(string formattedMessage)
        {
            //// There can be several placeholders that can be passed as params object[] to string formatting
            var firstScenarioPlaceholder = ScenarioContext.Current.Get<string>("firstScenarioPlaceholder");
            var expectedValidationMessage = string.Format(formattedMessage, firstScenarioPlaceholder);

            StringAssert.Contains(exception.Message, expectedValidationMessage, "Incorrect validation message");
        }
    }
}
