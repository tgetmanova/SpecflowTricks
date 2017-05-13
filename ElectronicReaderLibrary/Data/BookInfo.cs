namespace ElectronicReaderLibrary.Data
{
    /// <summary>
    /// Book information.
    /// </summary>
    public class BookInfo
    {
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the author.
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Gets or sets the number of pages.
        /// </summary>
        public int NumberOfPages { get; set; }

        /// <summary>
        /// Gets or sets the state in reader.
        /// </summary>
        public BookStateInReader StateInReader { get; set; }

        /// <summary>
        /// Gets or sets the electronic information.
        /// </summary>
        public ElectronicInfo ElectronicInfo { get; set; }
    }
}
