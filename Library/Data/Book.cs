using System.Threading;

namespace LibraryLogic.Data
{
    public class Book
    {
        public static int BookIDCounter = -1;

        public int ID { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public BookState State { get; set; }

        /* Creating book */
        public Book (string title, string author, string genre)
        {
            ID = Interlocked.Increment(ref BookIDCounter);
            Title = title;
            Author = author;
            Genre = genre;

            State = BookState.Available;
        }

        /* Editing book */
        public void UpdateBookData (BookUpdateData data)
        {
            Title = data.title;
            Author = data.author;
            Genre = data.genre;
        }



        /* Borrowing book */
        public void BorrowBook ()
        {
            State = BookState.Borrowed;
        }

        /* Returning book */
        public void ReturnBook ()
        {
            State = BookState.Available;
        }



        public override string ToString()
        {
            string bookStatus = State == BookState.Available ? ",   Status: Available" : ",    Status: Borrowed";
            return "[ID: " + ID + "],  Title: " + Title + ",  Author: " + Author + ",  Genre: " + Genre + bookStatus;
        }
    }



    public enum BookState
    {
        Available,
        Borrowed,
    }



    public class BookUpdateData
    {
        public int bookId;
        public string title;
        public string author;
        public string genre;

        public BookUpdateData(int bookId, string title, string author, string genre)
        {
            this.bookId = bookId;
            this.title = title;
            this.author = author;
            this.genre = genre;
        }
    }
}
