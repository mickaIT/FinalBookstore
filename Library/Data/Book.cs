using System.Threading;

namespace BookstoreLogic.Data
{
    public class Book
    {
        public static int BookISBNCounter = -1;

        public int ISBN { get; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Genre { get; set; }
        public BookState State { get; set; }

        /* Creating book */
        public Book (string title, string author, string genre)
        {
            ISBN = Interlocked.Increment(ref BookISBNCounter);
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



        /* Selling book */
        public void SellBook ()
        {
            State = BookState.Unavailable;
        }

        /* Returning book */
        public void ReturnBook ()
        {
            State = BookState.Available;
        }



        public override string ToString()
        {
            string bookStatus = State == BookState.Available ? ",   Status: Available" : ",    Status: Sold out";
            return "[ISBN: " + ISBN + "],  Title: " + Title + ",  Author: " + Author + ",  Genre: " + Genre + bookStatus;
        }
    }



    public enum BookState
    {
        Available,
        Unavailable,
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
