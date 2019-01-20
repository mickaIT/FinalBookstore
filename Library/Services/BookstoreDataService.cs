using System.Collections.Generic;
using LibraryLogic.Data;
using LibraryLogic.Logic_Implementations;

namespace LibraryLogic.Services
{
    public class BookstoreDataService
    {
        //private BookstoreUOW bookstoreUOW;
        private Model.BookstoreDataContext bookstore;

        public BookstoreDataService()
        {
            //string connectionString = @"Data Source = (LocalDB)\MSSQLLocalDB; AttachDbFilename = C:\Users\Adam\Documents\bookstore.mdf; Integrated Security = True; Connect Timeout = 30";
            bookstore = new Model.BookstoreDataContext();
            //BookstoreState BookstoreState = new BookstoreState();
            //BookDaoBasicImpl bookDao = new BookDaoBasicImpl(BookstoreState);
            //SaleDaoBasicImpl saleDao = new SaleDaoBasicImpl(BookstoreState);

            //bookstoreUOW = new BookstoreUOW(BookstoreState, bookDao, saleDao);
            FillBookstoreDataWithConstants();
        }


        // Removing data
        #region 
        public void RemoveAllData()
        {
            bookstore.RemoveData();
            //bookstoreUOW.RemoveAllData();
        }
        public void RemoveAllBooks()
        {
            bookstore.RemoveBooks();
            //bookstoreUOW.RemoveAllBooks();
        }
        public void RemoveAllInvoices()
        {
            bookstore.RemoveSales();
            //bookstoreUOW.RemoveAllInvoices();
        }
        #endregion


        // Filling the Bookstore data
        #region
        public void FillBookstoreDataWithConstants()
        {
            ConstantsBookstoreFiller Filler = new ConstantsBookstoreFiller();
            Filler.Fill(bookstore);
            //bookstoreUOW.FillBookstoreState(Filler);
        }
        #endregion


        // Books Service
        #region
        public void AddBook(Book book)
        {
            bookstore.AddBook(book);
            //bookstoreUOW.GetBooksDao.AddBook(book);
        }
        public void AddBook(string title, string author, string genre, int count)
        {
            bookstore.AddBook(new Book(title, author, genre, count));
            //bookstoreUOW.GetBooksDao.AddBook(new Book(title, author, genre, count));
        }

        public void UpdateBook(BookUpdateData data)
        {
            bookstore.UpdateBook(data);
            //bookstoreUOW.GetBooksDao.UpdateBook(data);
        }
        public void UpdateBook(int id, string title, string author, string genre)
        {
            bookstore.UpdateBook(new BookUpdateData(id, title, author, genre));
            //bookstoreUOW.GetBooksDao.UpdateBook(new BookUpdateData(id, title, author, genre));
        }

        public void RemoveBook(int id)
        {
            bookstore.RemoveBook(id);
            //bookstoreUOW.GetBooksDao.RemoveBook(id);
        }
        public void RemoveBook(Book book)
        {
            RemoveBook(book.ISBN);
        }
      
        public Model.Book GetBook(int id)
        {
            return bookstore.GetBook(id);
            //return bookstoreUOW.GetBooksDao.GetBook(id);
        }
        public List<Model.Book> GetAllBooks()
        {
            return bookstore.GetAllBooks();
            //return bookstoreUOW.GetBooksDao.GetAllBooks();
        }
        public List<Model.Book> GetBooksByAuthor(string author)
        {
            return bookstore.GetBooksByAuthor(author);
            //return bookstoreUOW.GetBooksDao.GetBooksByAuthor(author);
        }
        public List<Model.Book> GetBooksByTitle(string title)
        {
            return bookstore.GetBooksByTitle(title);
            //return bookstoreUOW.GetBooksDao.GetBooksByTitle(title);
        }
        public List<Model.Book> GetBooksByGenre(string genre)
        {
            return bookstore.GetBooksByGenre(genre);
            //return bookstoreUOW.GetBooksDao.GetBooksByGenre(genre);
        }
        public List<Model.Book> GetBooksByState(BookState state)
        {
            return bookstore.GetBooksByState(state);
            //return bookstoreUOW.GetBooksDao.GetBooksByState(state);
        }

        public void SellBook(int id)
        {
            bookstore.SellBook(id);
            //bookstoreUOW.GetBooksDao.SellBook(id);
        }
        public void ChangeStatus(int id)
        {
            bookstore.ChangeStatus(id);
            //bookstoreUOW.GetBooksDao.ChangeStatus(id);
        }
        #endregion


        // Invoices Service
        #region
        public void AddSale(int bookId)
        {
            Model.Book book = GetBook(bookId);

            Book _book = new Book(book.Title, book.Author, book.Genre, book.Count, book.ISBN);
            AddSale(new Sale(_book));

            //Book book = bookstoreUOW.GetBooksDao.GetBook(bookId);

            //bookstoreUOW.GetInvoicesDao.AddSale(new Sale(book));
        }
        public void AddSale(Sale sale)
        {
            bookstore.AddSale(sale);
            //bookstoreUOW.GetInvoicesDao.AddSale(sale);
        }

        public void RemoveSale(int id)
        {
            bookstore.RemoveSale(id);
            //bookstoreUOW.GetInvoicesDao.RemoveSale(id);
        }
        public void RemoveSale(Sale sale)
        {
            RemoveSale(sale.ID);
        }
        public void RemoveSale(Book soldBook)
        {
            bookstore.RemoveSale(soldBook);
            //bookstoreUOW.GetInvoicesDao.RemoveSale(soldBook);
        }

        public Model.Sale GetSale(int id)
        {
            return bookstore.GetSale(id);
            //return bookstoreUOW.GetInvoicesDao.GetSale(id);
        }
        public List<Model.Sale> GetAllInvoices()
        {
            return bookstore.GetAllInvoices();
            //return bookstoreUOW.GetInvoicesDao.GetAllInvoices();
        }
        #endregion


        // Counting objects
        #region
        public int BooksCount()
        {
            return GetAllBooks().Count;
            //return bookstoreUOW.GetBooksDao.GetAllBooks().Count;
        }

        public int InvoicesCount()
        {
            return GetAllInvoices().Count;
            //return bookstoreUOW.GetInvoicesDao.GetAllInvoices().Count;
        }
        #endregion
    }
}
