using System;

using ElectronicReaderLibrary.Data;

using TechTalk.SpecFlow;

namespace SpecflowTests.Demo.ContextExtensions
{
    /// <summary>
    /// The scenario context extensions.
    /// </summary>
    public static class ScenarioExtensions
    {
        /// <summary>
        /// The expected book information key
        /// </summary>
        private const string ExpectedBookInfoKey = "expectedBookInfo";

        /// <summary>
        /// Sets the expected book information.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="expectedBookInfo">The expected book information.</param>
        /// <exception cref="System.ArgumentNullException">context - The 'context' argument cannot be null</exception>
        /// <exception cref="System.InvalidOperationException">Cannot set BookInfo: the value is already set for the key'expectedBookInfo'</exception>
        public static void SetExpectedBookInfo(this ScenarioContext context, BookInfo expectedBookInfo)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The 'context' argument cannot be null");
            }

            BookInfo book;
            if (context.TryGetValue(ExpectedBookInfoKey, out book))
            {
                throw new InvalidOperationException("Cannot set BookInfo: the value is already set for the key 'expectedBookInfo'");
            }

            context.Add(ExpectedBookInfoKey, expectedBookInfo);
        }

        /// <summary>
        /// Gets the expected book information.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        /// <exception cref="System.ArgumentNullException">context - The 'context' argument cannot be null</exception>
        /// <exception cref="System.InvalidOperationException">Cannot get BookInfo: the value is not set for the key'expectedBookInfo'</exception>
        public static BookInfo GetExpectedBookInfo(this ScenarioContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The 'context' argument cannot be null");
            }

            BookInfo book;
            if (!context.TryGetValue(ExpectedBookInfoKey, out book))
            {
                throw new InvalidOperationException("Cannot get BookInfo: the value is not set for the key'expectedBookInfo'");
            }

            return book;
        }
    }
}
