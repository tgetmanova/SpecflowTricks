using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ElectronicReaderLibrary.Data;

namespace ElectronicReaderLibrary
{
    /// <summary>
    /// Electronic reader.
    /// </summary>
    public class ElectronicReader
    {
        /// <summary>
        /// The books
        /// </summary>
        public IList<BookInfo> books = new List<BookInfo>();

        /// <summary>
        /// The active book
        /// </summary>
        public BookInfo activeBook;

        /// <summary>
        /// The state
        /// </summary>
        public ReaderState state;

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectronicReader"/> class.
        /// </summary>
        public ElectronicReader()
        {
            this.state = ReaderState.TurnedOff;

            this.books = new List<BookInfo>
            {
                new BookInfo
                {
                    Title = "Crime and Punishment",
                    Author = "Dostoyevsky F. M.",
                    NumberOfPages = 524,
                    ElectronicInfo = new ElectronicInfo { DataFormat = DataFormat.Djvu, SizeInMegabytes = 34 },
                    StateInReader = BookStateInReader.Unloaded
                },
                 new BookInfo
                {
                    Title = "Demons",
                    Author = "Dostoyevsky F. M.",
                    NumberOfPages = 900,
                    ElectronicInfo = new ElectronicInfo { DataFormat = DataFormat.Epub, SizeInMegabytes = 56 },
                    StateInReader = BookStateInReader.Unloaded
                },
                  new BookInfo
                {
                    Title = "The Idiot",
                    Author = "Dostoyevsky F. M.",
                    NumberOfPages = 633,
                    ElectronicInfo = new ElectronicInfo { DataFormat = DataFormat.Fb2, SizeInMegabytes = 3 },
                    StateInReader = BookStateInReader.Unloaded
                }
            };
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ElectronicReader"/> class.
        /// </summary>
        /// <param name="booksToSetup">The books to setup.</param>
        public ElectronicReader(IList<BookInfo> booksToSetup)
        {
            this.state = ReaderState.TurnedOff;
            this.books = booksToSetup;
        }

        /// <summary>
        /// Retrieves the list of books.
        /// </summary>
        /// <returns>The books. </returns>
        public IEnumerable<BookInfo> RetrieveListOfBooks()
        {
            return this.books;
        }

        /// <summary>
        /// Turns the off reader.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The device is already turned off</exception>
        public void TurnOffReader()
        {
            if (this.state == ReaderState.TurnedOff)
            {
                throw new InvalidOperationException("The device is already turned off");
            }

            this.state = ReaderState.TurnedOff;
        }

        /// <summary>
        /// Turns the reader.
        /// </summary>
        /// <exception cref="System.InvalidOperationException">The device is already turned on</exception>
        public void TurnOnReader()
        {
            if (this.state != ReaderState.TurnedOff)
            {
                throw new InvalidOperationException("The device is already turned on");
            }

            this.state = ReaderState.Active;
        }

        /// <summary>
        /// Adds the book to the reader storage.
        /// </summary>
        /// <param name="bookToAdd">The book to add.</param>
        public void AddTheBookToTheReaderStorage(BookInfo bookToAdd)
        {
            this.ValidateTheNewBook(bookToAdd);

            this.books.Add(bookToAdd);
        }

        /// <summary>
        /// Opens the book by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public void OpenTheBook(string title)
        {
            this.ValidateReaderState(new[] { ReaderState.Reading, ReaderState.Active });

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title), "The 'title' should be specified");
            }

            if (!this.IsBookPresentedInReader(title))
            {
                throw new InvalidOperationException($"The book '{title}' has not been found in electronic reader storage");
            }

            var openedBook = this.GetCurrentlyOpenedBook();

            if (openedBook != null)
            {
                throw new InvalidOperationException($"Electronic reader is reading \"{openedBook.Title}\" book now");
            }

            this.activeBook = this.GetBookByTitle(title);
            this.state = ReaderState.Reading;

            var bookToUpdate = new BookInfo
            {
                Title = this.activeBook.Title,
                Author = this.activeBook.Author,
                ElectronicInfo = new ElectronicInfo
                {
                    DataFormat = this.activeBook.ElectronicInfo.DataFormat,
                    SizeInMegabytes = this.activeBook.ElectronicInfo.SizeInMegabytes
                },
                NumberOfPages = this.activeBook.NumberOfPages,
                StateInReader = BookStateInReader.OpenedForReading
            };

            this.UpdateBookInReader(bookToUpdate);
        }

        /// <summary>
        /// Closes the book by tile.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        public void CloseTheBook(string title)
        {
            this.ValidateReaderState(new[] { ReaderState.Reading });

            if (string.IsNullOrEmpty(title))
            {
                throw new ArgumentNullException(nameof(title), "The 'title' should be specified");
            }

            if (!this.IsBookPresentedInReader(title))
            {
                throw new InvalidOperationException($"The book {title} has not been found in electronic reader storage");
            }

            var openedBook = this.GetCurrentlyOpenedBook();

            if (openedBook == null)
            {
                throw new InvalidOperationException($"Electronic reader is not reading the book now");
            }

            if (!openedBook.Title.Equals(title, StringComparison.OrdinalIgnoreCase))
            {
                throw new InvalidOperationException($"Electronic reader is now reading the other book: {openedBook.Title}, but not {title}");
            }

            this.activeBook = null;
            this.state = ReaderState.Active;

            var bookToUpdate = new BookInfo
            {
                Title = this.activeBook.Title,
                Author = this.activeBook.Author,
                ElectronicInfo = new ElectronicInfo
                {
                    DataFormat = this.activeBook.ElectronicInfo.DataFormat,
                    SizeInMegabytes = this.activeBook.ElectronicInfo.SizeInMegabytes
                },
                NumberOfPages = this.activeBook.NumberOfPages,
                StateInReader = BookStateInReader.Unloaded
            };

            this.UpdateBookInReader(bookToUpdate);
        }

        /// <summary>
        /// Determines whether [is book presented in reader].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is book presented in reader]; otherwise, <c>false</c>.
        /// </returns>
        private bool IsBookPresentedInReader(string title)
        {
            var book = this.books.FirstOrDefault(item => item.Title.Equals(title, StringComparison.OrdinalIgnoreCase));

            return book != null;
        }

        /// <summary>
        /// Gets the currently opened book.
        /// </summary>
        /// <returns></returns>
        private BookInfo GetCurrentlyOpenedBook()
        {
            return this.books.SingleOrDefault(b => b.StateInReader == BookStateInReader.OpenedForReading);
        }

        /// <summary>
        /// Updates the book in reader.
        /// </summary>
        /// <param name="bookUpdate">The book update.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        private void UpdateBookInReader(BookInfo bookUpdate)
        {
            var bookToUpdate = this.books.FirstOrDefault(b => b.Title.Equals(bookUpdate.Title, StringComparison.OrdinalIgnoreCase));
            if (bookToUpdate == null)
            {
                throw new InvalidOperationException($"Cannot find book: {bookUpdate.Title}");
            }

            this.books[this.books.IndexOf(bookToUpdate)] = bookUpdate;
        }

        /// <summary>
        /// Gets the book by title.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <returns>The Book. </returns>
        /// <exception cref="System.InvalidOperationException"></exception>
        private BookInfo GetBookByTitle(string title)
        {
            var resolvedBookInfo = this.books.FirstOrDefault(b => b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
            if (resolvedBookInfo == null)
            {
                throw new InvalidOperationException($"Cannot find book with title: {title}");
            }

            return resolvedBookInfo;
        }

        /// <summary>
        /// Validates the state of the reader.
        /// </summary>
        /// <param name="expectedState">The expected state.</param>
        /// <exception cref="System.InvalidOperationException"></exception>
        private void ValidateReaderState(ReaderState[] expectedStates)
        {
            if (!expectedStates.Contains(this.state))
            {
                throw new InvalidOperationException($"Invalid Reader state: {this.state}, expected: {expectedStates}");
            }
        }

        /// <summary>
        /// Validates the new book.
        /// </summary>
        /// <param name="book">The book.</param>
        /// <exception cref="System.ArgumentNullException">book - The book should be specified</exception>
        /// <exception cref="System.InvalidOperationException"></exception>
        private void ValidateTheNewBook(BookInfo book)
        {
            var stringBuilder = new StringBuilder();

            if (book == null)
            {
                throw new ArgumentNullException(nameof(book), "The book should be specified");
            }

            if (string.IsNullOrEmpty(book.Title))
            {
                stringBuilder.Append($"The Title: {book.Title} is invalid");
                stringBuilder.AppendLine();
            }

            if (string.IsNullOrEmpty(book.Author))
            {
                stringBuilder.Append($"The Author: {book.Author} is invalid");
                stringBuilder.AppendLine();
            }

            if (book.NumberOfPages <= 0)
            {
                stringBuilder.Append($"The Number of pages: {book.NumberOfPages} is invalid");
                stringBuilder.AppendLine();
            }

            if (book.ElectronicInfo == null)
            {
                stringBuilder.Append($"The electronic info should be specified");
                stringBuilder.AppendLine();
            }

            if (book.ElectronicInfo.DataFormat == DataFormat.None)
            {
                stringBuilder.Append($"The electronic info DataFormat should be specified");
                stringBuilder.AppendLine();
            }

            if (book.ElectronicInfo.SizeInMegabytes <= 0)
            {
                stringBuilder.Append($"The electronic info SizeInMegabytes is invalid");
                stringBuilder.AppendLine();
            }

            if (book.StateInReader != BookStateInReader.Unloaded)
            {
                stringBuilder.Append($"The state {book.StateInReader} is invalid. Should be: Unloaded");
                stringBuilder.AppendLine();
            }

            var validationString = stringBuilder.ToString();
            if (!string.IsNullOrEmpty(validationString))
            {
                throw new InvalidOperationException($"Cannot add new book: {Environment.NewLine} {validationString}");
            }
        }
    }
}
