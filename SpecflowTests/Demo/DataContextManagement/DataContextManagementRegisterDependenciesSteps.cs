using System.Collections.Generic;
using System.Linq;

using ElectronicReaderLibrary;
using ElectronicReaderLibrary.Data;

using TechTalk.SpecFlow;
using BoDi;

namespace SpecflowTests.DataContextManagement
{
    /// <summary>
    /// This is separate class (but it is not mandatory to have separate class 
    /// for objectContainer setup) where we use built-in Specflow
    /// BoDi library: IObjectContainer objectContainer to setup dependencies
    /// </summary>
    [Binding]
    public sealed class DataContextManagementRegisterDependenciesSteps
    {
        /// <summary>
        /// The object container
        /// </summary>
        private readonly IObjectContainer objectContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="DataContextManagementRegisterDependenciesSteps"/> class.
        /// </summary>
        /// <param name="objectContainer">The object container.</param>
        public DataContextManagementRegisterDependenciesSteps(IObjectContainer objectContainer)
        {
            this.objectContainer = objectContainer;
        }

        //// The customization of the container with before scenario hook
        //// constructor injection happens for IObjectContainer objectContainer
        //// then we add reader and dependent graph objects to the container
        //// so that binding classes can specify ElectronicReader dependencies (a constructor argument of type ElectronicReader).
        //// List of books is registered as Instance:
        [BeforeScenario("registerInstanceAsOptionForBookList", "contextInjectionDemo")]
        public void RegisterDependenciesWithBooksListAsInstance()
        {
            var reader = new ElectronicReader();
            objectContainer.RegisterInstanceAs(reader);
            objectContainer.RegisterInstanceAs<IList<BookInfo>>(reader.RetrieveListOfBooks().ToList());
        }

        //// The same as above, but
        //// List of books is registered as Type:
        [BeforeScenario("registerTypeAsOptionForBookList")]
        public void RegisterDependenciesWithBookListAsType()
        {
            var reader = new ElectronicReader();
            objectContainer.RegisterInstanceAs(reader);
            objectContainer.RegisterTypeAs<List<BookInfo>, IList<BookInfo>>();
        }
    }
}
