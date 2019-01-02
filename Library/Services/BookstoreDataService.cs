using System.Collections.Generic;
using BookstoreLogic.Data;
using BookstoreLogic.Logic;
using BookstoreLogic.LogicImplementations;

namespace BookstoreLogic.Services
{
    public class BookstoreDataService
    {
        private BookstoreUOW bookstoreUOW;

        public BookstoreDataService()
        {
            BookstoreState BookstoreState = new BookstoreState();
            BookDaoBasicImpl bookDao = new BookDaoBasicImpl(BookstoreState);
            RentalDaoBasicImpl rentalDao = new RentalDaoBasicImpl(BookstoreState);

            bookstoreUOW = new BookstoreUOW(BookstoreState, bookDao, rentalDao);
            FillBookstoreDataWithConstants();
        }


        // Removing data
        #region 
        public void RemoveAllData()
        {
            bookstoreUOW.RemoveAllData();
        }
        public void RemoveAllBooks()
        {
            bookstoreUOW.RemoveAllBooks();
        } 
        public void RemoveAllInvoices()
        {
            bookstoreUOW.RemoveAllInvoices();
        }
        #endregion


        // Filling the Bookstore data
        #region
        public void FillBookstoreDataWithConstants()
        {
            ConstantsBookstoreFiller Filler = new ConstantsBookstoreFiller();
            bookstoreUOW.FillBookstoreState(Filler);
        }
        #endregion


        // Books Service
        #region
        public void AddBook(Book book)
        {
            bookstoreUOW.GetBooksDao.AddBook(book);
        }
        public void AddBook(string title, string author, string genre)
        {
            bookstoreUOW.GetBooksDao.AddBook(new Book(title, author, genre));
        }

        public void UpdateBook(BookUpdateData data)
        {
            bookstoreUOW.GetBooksDao.UpdateBook(data);
        }
        public void UpdateBook(int id, string title, string author, string genre)
        {
            bookstoreUOW.GetBooksDao.UpdateBook(new BookUpdateData(id, title, author, genre));
        }

        public bool CanRemoveBook(int id)
        {
            return bookstoreUOW.GetBooksDao.CanRemoveBook(id);
        }
        public void RemoveBook(int id)
        {
            bookstoreUOW.GetBooksDao.RemoveBook(id);
        }
        public void RemoveBook(Book book)
        {
            RemoveBook(book.ISBN);
        }
      
        public Book GetBook(int id)
        {
            return bookstoreUOW.GetBooksDao.GetBook(id);
        }
        public List<Book> GetAllBooks()
        {
            return bookstoreUOW.GetBooksDao.GetAllBooks();
        }
        public List<Book> GetBooksByAuthor(string author)
        {
            return bookstoreUOW.GetBooksDao.GetBooksByAuthor(author);
        }
        public List<Book> GetBooksByTitle(string title)
        {
            return bookstoreUOW.GetBooksDao.GetBooksByTitle(title);
        }
        public List<Book> GetBooksByGenre(string genre)
        {
            return bookstoreUOW.GetBooksDao.GetBooksByGenre(genre);
        }
        public List<Book> GetBooksByState(BookState state)
        {
            return bookstoreUOW.GetBooksDao.GetBooksByState(state);
        }

        public void SellBook(int id)
        {
            bookstoreUOW.GetBooksDao.SellBook(id);
        }
        public void ReturnBook(int id)
        {
            bookstoreUOW.GetBooksDao.ReturnBook(id);
        }
        #endregion


        // Invoices Service
        #region
        public void AddRental(int bookId)
        {
            Book book = bookstoreUOW.GetBooksDao.GetBook(bookId);

            bookstoreUOW.GetInvoicesDao.AddRental(new Rental(book));
        }
        public void AddRental(Rental rental)
        {
            bookstoreUOW.GetInvoicesDao.AddRental(rental);
        }
        
        public void RemoveRental(int id)
        {
            bookstoreUOW.GetInvoicesDao.RemoveRental(id);
        }
        public void RemoveRental(Rental rental)
        {
            RemoveRental(rental.ID);
        }
        public void RemoveRental(Book rentedBook)
        {
            bookstoreUOW.GetInvoicesDao.RemoveRental(rentedBook);
        }

        public Rental GetRental(int id)
        {
            return bookstoreUOW.GetInvoicesDao.GetRental(id);
        }
        public List<Rental> GetAllInvoices()
        {
            return bookstoreUOW.GetInvoicesDao.GetAllInvoices();
        }
        #endregion


        // Counting objects
        #region
        public int BooksCount()
        {
            return bookstoreUOW.GetBooksDao.GetAllBooks().Count;
        }

        public int InvoicesCount()
        {
            return bookstoreUOW.GetInvoicesDao.GetAllInvoices().Count;
        }
        #endregion
    }
}
