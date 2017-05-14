namespace ElectronicReaderLibrary
{
    /// <summary>
    /// 'Service' methods to manage electronic reader instances
    /// </summary>
    public class ElectronicReaderService
    {
        /// <summary>
        /// Gets the reader.
        /// </summary>
        /// <returns></returns>
        public ElectronicReader GetReader()
        {
            return new ElectronicReader();
        }
    }
}
