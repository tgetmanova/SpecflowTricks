namespace ElectronicReaderLibrary.Data
{
    /// <summary>
    /// Enumerates the states of the book in electronic reader.
    /// </summary>
    public enum BookStateInReader
    {
        /// <summary>
        /// The none
        /// </summary>
        None = 0,

        /// <summary>
        /// The opened for reading
        /// </summary>
        OpenedForReading = 1,

        /// <summary>
        /// The unloaded
        /// </summary>
        Unloaded = 2,

        /// <summary>
        /// The moved to trash
        /// </summary>
        MovedToTrash = 3
    }
}
