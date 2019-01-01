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
            UserDaoBasicImpl userDao = new UserDaoBasicImpl(libraryState);
            RentalDaoBasicImpl rentalDao = new RentalDaoBasicImpl(libraryState);

            libUOW = new LibraryUOW(libraryState, bookDao, userDao, rentalDao);
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
        public void RemoveAllUsers()
        {
            libUOW.RemoveAllUsers();
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

        public void BorrowBook(int id)
        {
            libUOW.GetBooksDao.BorrowBook(id);
        }
        public void ReturnBook(int id)
        {
            libUOW.GetBooksDao.ReturnBook(id);
        }
        #endregion


        // Users Service
        #region
        public void AddUser(User user)
        {
            libUOW.GetUsersDao.AddUser(user);
        }
        public void AddUser(string name, string surname)
        {
            libUOW.GetUsersDao.AddUser(new User(name, surname));
        }

        public void UpdateUser(UserUpdateData data)
        {
            libUOW.GetUsersDao.UpdateUser(data);
        }
        public void UpdateUser(int id, string name, string surname)
        {
            libUOW.GetUsersDao.UpdateUser(new UserUpdateData(id, name, surname));
        }

        public bool CanRemoveUser(int id)
        {
            return libUOW.GetUsersDao.CanRemoveUser(id);
        }
        public void RemoveUser(int id)
        {
            libUOW.GetUsersDao.RemoveUser(id);
        }
        public void RemoveUser(User user)
        {
            RemoveUser(user.ID);
        }

        public User GetUser(int id)
        {
            return libUOW.GetUsersDao.GetUser(id);
        }
        public List<User> GetAllUsers()
        {
            return libUOW.GetUsersDao.GetAllUsers();
        }
        public List<User> GetUsersByFirstName(string firstName)
        {
            return libUOW.GetUsersDao.GetUsersByFirstName(firstName);
        }
        public List<User> GetUsersByLastName(string lastName)
        {
            return libUOW.GetUsersDao.GetUsersByLastName(lastName);
        }
        public List<User> GetUsersWithBooks()
        {
            return libUOW.GetUsersDao.GetUsersWithRentedBooks();
        }

        public void AddBookToUser(int userId)
        {
            libUOW.GetUsersDao.AddBook(userId);
        }
        public void RemoveBookFromUser(int userId)
        {
            libUOW.GetUsersDao.RemoveBook(userId);
        }
        #endregion


        // Rentals Service
        #region
        public void AddRental(int bookId, int userId)
        {
            Book book = libUOW.GetBooksDao.GetBook(bookId);
            User user = libUOW.GetUsersDao.GetUser(userId);

            libUOW.GetRentalsDao.AddRental(new Rental(book, user));
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
        public List<Rental> GetRentalsByUsername(string username)
        {
            return libUOW.GetRentalsDao.GetRentalsByUsername(username);
        }
        #endregion


        // Counting objects
        #region
        public int BooksCount()
        {
            return libUOW.GetBooksDao.GetAllBooks().Count;
        }
        public int UsersCount()
        {
            return libUOW.GetUsersDao.GetAllUsers().Count;
        }
        public int RentalsCount()
        {
            return libUOW.GetRentalsDao.GetAllRentals().Count;
        }
        #endregion
    }
}
