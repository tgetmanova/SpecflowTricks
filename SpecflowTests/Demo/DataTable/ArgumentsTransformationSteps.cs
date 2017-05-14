using System;
using System.Linq;

using ElectronicReaderLibrary.Data;

using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SpecflowTests.Demo.DataTable
{
    /// <summary>
    /// Here we extract separate binding class for methods that transform data
    /// </summary>
    [Binding]
    public sealed class ArgumentsTransformationSteps
    {
        /// <summary>
        /// Transforms the table to book information.
        /// </summary>
        /// <param name="table">The table.</param>
        [StepArgumentTransformation]
        public BookInfo TransformTableToBookInfo(Table table)
        {
            var bookFromTable = table.CreateInstance<BookInfo>();

            var transformedBook = new BookInfo
            {
                Title = bookFromTable.Title,
                Author = bookFromTable.Author,
                NumberOfPages = bookFromTable.NumberOfPages,
                StateInReader = bookFromTable.StateInReader, //// Specflow by default can work with Enums, so 'StateInReader' is resolved already in table instance
                ElectronicInfo = new ElectronicInfo
                {
                    //// Direct table rows addressing and parsing:
                    DataFormat = (DataFormat)Enum.Parse(typeof(DataFormat), table.Rows.First()["ElectronicInfo.DataFormat"]),
                    SizeInMegabytes = double.Parse(table.Rows.First()["ElectronicInfo.SizeInMB"])
                }
            };

            return transformedBook;
        }
    }
}
