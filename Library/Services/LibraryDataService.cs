using System.Collections.Generic;
using LibraryLogic.Data;
using LibraryLogic.Logic;
using LibraryLogic.LogicImplementations;

namespace LibraryLogic.Services
{
    public class LibraryDataService
    {
        private LibraryUOW libUOW;

        public LibraryDataService()
        {
            LibraryState libraryState = new LibraryState();
            BookDaoBasicImpl bookDao = new BookDaoBasicImpl(libraryState);
            RentalDaoBasicImpl rentalDao = new RentalDaoBasicImpl(libraryState);

            libUOW = new LibraryUOW(libraryState, bookDao, rentalDao);
            FillLibraryDataWithConstants();
        }


        // Removing data
        #region 
        public void RemoveAllData()
        {
            libUOW.RemoveAllData();
        }
        public void RemoveAllBooks()
        {
            libUOW.RemoveAllBooks();
        } 
        public void RemoveAllRentals()
        {
            libUOW.RemoveAllRentals();
        }
        #endregion


        // Filling the library data
        #region
        public void FillLibraryDataWithConstants()
        {
            ConstantsLibraryFiller Filler = new ConstantsLibraryFiller();
            libUOW.FillLibraryState(Filler);
        }
        #endregion


        // Books Service
        #region
        public void AddBook(Book book)
        {
            libUOW.GetBooksDao.AddBook(book);
        }
        public void AddBook(string title, string author, string genre)
        {
            libUOW.GetBooksDao.AddBook(new Book(title, author, genre));
        }

        public void UpdateBook(BookUpdateData data)
        {
            libUOW.GetBooksDao.UpdateBook(data);
        }
        public void UpdateBook(int id, string title, string author, string genre)
        {
            libUOW.GetBooksDao.UpdateBook(new BookUpdateData(id, title, author, genre));
        }

        public bool CanRemoveBook(int id)
        {
            return libUOW.GetBooksDao.CanRemoveBook(id);
        }
        public void RemoveBook(int id)
        {
            libUOW.GetBooksDao.RemoveBook(id);
        }
        public void RemoveBook(Book book)
        {
            RemoveBook(book.ID);
        }
      
        public Book GetBook(int id)
        {
            return libUOW.GetBooksDao.GetBook(id);
        }
        public List<Book> GetAllBooks()
        {
            return libUOW.GetBooksDao.GetAllBooks();
        }
        public List<Book> GetBooksByAuthor(string author)
        {
            return libUOW.GetBooksDao.GetBooksByAuthor(author);
        }
        public List<Book> GetBooksByTitle(string title)
        {
            return libUOW.GetBooksDao.GetBooksByTitle(title);
        }
        public List<Book> GetBooksByGenre(string genre)
        {
            return libUOW.GetBooksDao.GetBooksByGenre(genre);
        }
        public List<Book> GetBooksByState(BookState state)
        {
            return libUOW.GetBooksDao.GetBooksByState(state);
        }

        public void SellBook(int id)
        {
            libUOW.GetBooksDao.BorrowBook(id);
        }
        public void ReturnBook(int id)
        {
            libUOW.GetBooksDao.ReturnBook(id);
        }
        #endregion


        // Rentals Service
        #region
        public void AddRental(int bookId)
        {
            Book book = libUOW.GetBooksDao.GetBook(bookId);

            libUOW.GetRentalsDao.AddRental(new Rental(book));
        }
        public void AddRental(Rental rental)
        {
            libUOW.GetRentalsDao.AddRental(rental);
        }
        
        public void RemoveRental(int id)
        {
            libUOW.GetRentalsDao.RemoveRental(id);
        }
        public void RemoveRental(Rental rental)
        {
            RemoveRental(rental.ID);
        }
        public void RemoveRental(Book rentedBook)
        {
            libUOW.GetRentalsDao.RemoveRental(rentedBook);
        }

        public Rental GetRental(int id)
        {
            return libUOW.GetRentalsDao.GetRental(id);
        }
        public List<Rental> GetAllRentals()
        {
            return libUOW.GetRentalsDao.GetAllRentals();
        }
        #endregion


        // Counting objects
        #region
        public int BooksCount()
        {
            return libUOW.GetBooksDao.GetAllBooks().Count;
        }

        public int RentalsCount()
        {
            return libUOW.GetRentalsDao.GetAllRentals().Count;
        }
        #endregion
    }
}
