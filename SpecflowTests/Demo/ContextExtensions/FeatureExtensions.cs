using System;

using ElectronicReaderLibrary;

using TechTalk.SpecFlow;

namespace SpecflowTests.Demo.ContextExtensions
{
    /// <summary>
    /// The Feature context extensions.
    /// Can be useful for Feature-level data, 
    /// mostly for objects that are stateless, like service clients
    /// </summary>
    public static class FeatureExtensions
    {
        /// <summary>
        /// Sets the electronic reader service.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <exception cref="System.ArgumentNullException">context - The 'context' argument cannot be null</exception>
        /// <exception cref="System.InvalidOperationException">Cannot set reader service: the value is already set for the key 'readerService'</exception>
        public static void SetElectronicReaderService(this FeatureContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The 'context' argument cannot be null");
            }

            ElectronicReaderService readerService;
            if (context.TryGetValue("readerService", out readerService))
            {
                throw new InvalidOperationException("Cannot set reader service: the value is already set for the key 'readerService'");
            }

            context.Add("readerService", new ElectronicReaderService());
        }

        /// <summary>
        /// Gets the electronic reader service.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>Reader service.</returns>
        /// <exception cref="System.ArgumentNullException">context - The 'context' argument cannot be null</exception>
        /// <exception cref="System.InvalidOperationException">Cannot get reader service: the value is not set for the key 'readerService'</exception>
        public static ElectronicReaderService GetElectronicReaderService(this FeatureContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context), "The 'context' argument cannot be null");
            }

            ElectronicReaderService readerService;
            if (!context.TryGetValue("readerService", out readerService))
            {
                throw new InvalidOperationException("Cannot get reader service: the value is not set for the key 'readerService'");
            }

            return readerService;
        }
    }
}
