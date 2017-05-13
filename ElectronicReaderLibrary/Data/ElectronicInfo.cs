namespace ElectronicReaderLibrary.Data
{
    /// <summary>
    /// Electronic information.
    /// </summary>
    public class ElectronicInfo
    {
        /// <summary>
        /// Gets or sets the size in megabytes.
        /// </summary>
        public double SizeInMegabytes { get; set; }

        /// <summary>
        /// Gets or sets the data format.
        /// </summary>
        public DataFormat DataFormat { get; set; }
    }
}
