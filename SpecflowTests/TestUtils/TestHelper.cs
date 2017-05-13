using System;

using ElectronicReaderLibrary.Data;

namespace SpecflowTests.TestUtils
{
    internal class TestHelper
    {
        internal static BookInfo GetValidBookInfoWithTitle(string  title)
        {
            return new BookInfo
            {
                Title = title,
                Author = $"Author{DateTime.Now.Ticks}",
                NumberOfPages = 300,
                ElectronicInfo = new ElectronicInfo
                {
                    DataFormat = DataFormat.Fb2,
                    SizeInMegabytes = 30
                },
                StateInReader = BookStateInReader.Unloaded
            };
        }
    }
}
