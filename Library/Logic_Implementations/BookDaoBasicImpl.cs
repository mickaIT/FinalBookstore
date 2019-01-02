using System;
using System.Collections.Generic;
using System.Linq;
using BookstoreLogic.Logic;
using BookstoreLogic.Data;

namespace BookstoreLogic.LogicImplementations
{
    public class BookDaoBasicImpl : IBookDao
    {
        private BookstoreState BookstoreData;

        public BookDaoBasicImpl(BookstoreState libData)
        {
            BookstoreData = libData;
        }




        public void AddBook(Book book)
        {
            BookstoreData.BookstoreBooks.Add(book);
        }

        public void UpdateBook(BookUpdateData bookData)
        {
            GetBook(bookData.bookId)?.UpdateBookData(bookData);
        }

        public bool CanRemoveBook(int bookID)
        {
            if (GetBook(bookID)?.State == BookState.Available)
                return true;

            return false;
        }

        public void RemoveBook(int bookID)
        {
            Book book = GetBook(bookID);

            if (book != null)
                BookstoreData.BookstoreBooks.Remove(book);
            else
                throw new InvalidOperationException("RemoveBook: Cannot find specified book");
        }




        public Book GetBook(int bookID)
        {
            for (int i = 0; i < BookstoreData.BookstoreBooks.Count; i++)
            {
                if (BookstoreData.BookstoreBooks[i].ID == bookID)
                {
                    return BookstoreData.BookstoreBooks[i];
                }
            }
            return null;
        }

        public List<Book> GetAllBooks()
        {
            return BookstoreData.BookstoreBooks;
        }

        public List<Book> GetBooksByAuthor(string author)
        {
            return GetAllBooks().Select(book => book).Where(book => book.Author.Equals(author)).ToList();
        }

        public List<Book> GetBooksByTitle(string title)
        {
            return GetAllBooks().Select(book => book).Where(book => book.Title.Equals(title)).ToList();
        }

        public List<Book> GetBooksByGenre(string genre)
        {
            return GetAllBooks().Select(book => book).Where(book => book.Genre.Equals(genre)).ToList();
        }

        public List<Book> GetBooksByState(BookState state)
        {
            return GetAllBooks().Select(book => book).Where(book => book.State.Equals(state)).ToList();
        }




        public void BorrowBook(int bookID)
        {
            GetBook(bookID)?.BorrowBook();
        }

        public void ReturnBook(int bookID)
        {
            GetBook(bookID)?.ReturnBook();
        }
    }
}
