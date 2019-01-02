using BookstoreLogic.Data;
using BookstoreLogic.Logic;
using BookstoreLogic.LogicImplementations;

namespace BookstoreLogic.Services
{
    public class BookstoreUOW
    {
        private BookstoreState BookstoreData;

        private IBookDao booksDao;
        private IRentalDao rentalsDao;

        public BookstoreUOW(BookstoreState libData, IBookDao bDao, IRentalDao rDao)
        {
            BookstoreData = libData;

            booksDao = bDao;
            rentalsDao = rDao;
        }

        public void FillBookstoreState(IBookstoreFiller filler)
        {
            filler.Fill(BookstoreData);
        }


        
        public void RemoveAllData()
        {
            RemoveAllRentals();
            RemoveAllBooks();
        }

        public void RemoveAllBooks()
        {
            for (int i = BookstoreData.BookstoreBooks.Count - 1; i >= 0; i--)
            {
                int bookID = BookstoreData.BookstoreBooks[i].ID;

                if (booksDao.CanRemoveBook(bookID))
                    booksDao.RemoveBook(bookID);
            }
        }

  
        public void RemoveAllRentals()
        {
            BookstoreData.BookRentals.Clear();
        }


        // Getters (return left side if left side != null, otherwiser return right side)
        public IBookDao GetBooksDao => booksDao ?? (booksDao = new BookDaoBasicImpl(BookstoreData));
        public IRentalDao GetRentalsDao => rentalsDao ?? (rentalsDao = new RentalDaoBasicImpl(BookstoreData));
    }
}
