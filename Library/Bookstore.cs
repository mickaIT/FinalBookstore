using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryLogic.Model
{
    public partial class Book
    {
        public override string ToString()
        {
            string bookStatus = BookState ? ", Status: Available " : ", Status: unavailable ";
            return "ISBN: " + ISBN + ", Title: " + Title + ", Author: " + Author + ", Genre: " + Genre + bookStatus + ", Count: " + Count;
        }

        public static Book FromString(string serializedObject)
        {
            string[] elements = serializedObject.Split(new string[] { ", " }, StringSplitOptions.None);

            return new Book()
            {
                Title = elements[1].Substring(7),
                Author = elements[2].Substring(8),
                Genre = elements[3].Substring(7),
                Count = int.Parse(elements[5].Substring(7)),
                ISBN = int.Parse(elements[0].Substring(6)),
            };
        }
    }

    public partial class Sale
    {
        public override string ToString()
        {
            return "[ID: " + Id + "]"
                    + ",  Sold book: " + Book.Title
                    + ", ISBN: " + Book.ISBN
                    + ",  Invoice date: " + InvoiceDate;
        }
    }

    public partial class BookstoreDataContext
    {
        #region LibraryState
        public List<Book> BookstoreBooks
        {
            get { return Books.ToList(); }
        }

        public List<Sale> BookInvoices
        {
            get { return BookInvoices.ToList(); }
        }
        #endregion

        #region IBookDao
        public void AddBook(LibraryLogic.Data.Book book)
        {
            Book _book = new Book()
            {
                ISBN = book.ISBN,
                Title = book.Title,
                Author = book.Author,
                Genre = book.Genre,
                Count = book.Count,
                BookState = book.State == LibraryLogic.Data.BookState.Available
            };
            Books.InsertOnSubmit(_book);
            SubmitChanges();
        }

        public void UpdateBook(LibraryLogic.Data.BookUpdateData bookData)
        {
            IQueryable<Book> bookQuery = from book in Books where book.ISBN == bookData.bookId select book;
            foreach (Book book in bookQuery)
            {
                book.Author = bookData.author;
                book.Genre = bookData.genre;
                book.Title = bookData.title;
            }
            SubmitChanges();
        }

        public void RemoveBook(int bookISBN)
        {
            
            IQueryable<Book> bookQuery = from book in Books where book.Id == bookISBN select book;
            foreach (Book book in bookQuery)
            {
                // Remove associated sales
                IQueryable<Sale> salesQuery = from sale in Sales
                                             where sale.SoldBook.Equals(book.Id)
                                             select sale;
                Sales.DeleteAllOnSubmit(salesQuery);

                Books.DeleteOnSubmit(book);
            }
            SubmitChanges();
        }

        public Book GetBook(int bookISBN)
        {
            var bookQuery = from book in Books where book.Id == bookISBN select book;
            return bookQuery.FirstOrDefault();
        }

        public List<Book> GetAllBooks()
        {
            return Books.ToList();
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            return Books.Where(book => book.Author.Equals(author)).ToList();
        }

        public List<Book> GetBooksByTitle(string title)
        {
            return Books.Where(book => book.Title.Equals(title)).ToList();
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            return Books.Where(book => book.Genre.Equals(genre)).ToList();
        }

        public List<Book> GetBooksByState(LibraryLogic.Data.BookState state)
        {
            bool _state = state == LibraryLogic.Data.BookState.Available;
            return Books.Where(book => book.BookState == _state).ToList();
        }

        public void SellBook(int bookISBN)
        {
            Book book = GetBook(bookISBN);
            if (book.Count <= 0)
            {
                book.BookState = false;
                throw new LibraryLogic.Data.UnavailableBookException("No more books available.");
            }
            else if (book.Count == 1)
            {
                book.BookState = false;
                book.Count -= 1;
            }
            else
            {
                book.Count -= 1;
            }
            SubmitChanges();
        }

        public void ChangeStatus(int bookISBN)
        {
            Book book = GetBook(bookISBN);
            book.BookState = !book.BookState;
            SubmitChanges();
        }
        #endregion

        #region ISaleDao
        public void AddSale(LibraryLogic.Data.Sale sale)
        {
            Book _book = Books.Where(book => book.ISBN == sale.SoldBook.ISBN).FirstOrDefault();

            if (_book == null)
            {
                throw new InvalidOperationException("AddSale: There's no such book in the catalog.");
            }

            SellBook(_book.ISBN); // Fixed

            Sale _sale = new Sale()
            {
                SoldBook = _book.Id,
                InvoiceDate = sale.InvoiceDate,
            };

            Sales.InsertOnSubmit(_sale);
            SubmitChanges();
        }

        public void RemoveSale(int saleISBN)
        {
            Sale sale = GetSale(saleISBN);
            Sales.DeleteOnSubmit(sale);
            SubmitChanges();
        }

        public void RemoveSale(LibraryLogic.Data.Book soldBook)
        {
            Sale _sale = Sales.Where(sale => sale.Book.ISBN == soldBook.ISBN).FirstOrDefault();

            if (_sale == null)
            {
                throw new InvalidOperationException("RemoveSale: There's no such book in the catalog.");
            }

            Sales.DeleteOnSubmit(_sale);
            SubmitChanges();
        }

        public Sale GetSale(int saleISBN)
        {
            return Sales.Where(sale => sale.Id == saleISBN).FirstOrDefault();
        }

        public List<Sale> GetAllInvoices()
        {
            return Sales.ToList();
        }
        #endregion

        #region BookstoreUOW
        public void RemoveData()
        {
            Sales.DeleteAllOnSubmit<Sale>(Sales);
            Books.DeleteAllOnSubmit<Book>(Books);
            SubmitChanges();
        }

        public void RemoveBooks()
        {
            Sales.DeleteAllOnSubmit<Sale>(Sales);
            Books.DeleteAllOnSubmit<Book>(Books);
            SubmitChanges();
        }

        public void RemoveSales()
        {
            Sales.DeleteAllOnSubmit<Sale>(Sales);
            SubmitChanges();
        }
        #endregion
    }
}
