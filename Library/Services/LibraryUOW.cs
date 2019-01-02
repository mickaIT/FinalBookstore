using LibraryLogic.Data;
using LibraryLogic.Logic;
using LibraryLogic.LogicImplementations;

namespace LibraryLogic.Services
{
    public class LibraryUOW
    {
        private LibraryState libraryData;

        private IBookDao booksDao;
        private IRentalDao rentalsDao;

        public LibraryUOW(LibraryState libData, IBookDao bDao, IRentalDao rDao)
        {
            libraryData = libData;

            booksDao = bDao;
            rentalsDao = rDao;
        }

        public void FillLibraryState(ILibraryFiller filler)
        {
            filler.Fill(libraryData);
        }


        
        public void RemoveAllData()
        {
            RemoveAllRentals();
            RemoveAllBooks();
        }

        public void RemoveAllBooks()
        {
            for (int i = libraryData.LibraryBooks.Count - 1; i >= 0; i--)
            {
                int bookID = libraryData.LibraryBooks[i].ID;

                if (booksDao.CanRemoveBook(bookID))
                    booksDao.RemoveBook(bookID);
            }
        }

  
        public void RemoveAllRentals()
        {
            libraryData.BookRentals.Clear();
        }


        // Getters (return left side if left side != null, otherwiser return right side)
        public IBookDao GetBooksDao => booksDao ?? (booksDao = new BookDaoBasicImpl(libraryData));
        public IRentalDao GetRentalsDao => rentalsDao ?? (rentalsDao = new RentalDaoBasicImpl(libraryData));
    }
}
