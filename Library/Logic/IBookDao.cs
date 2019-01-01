using System.Collections.Generic;
using LibraryLogic.Data;

namespace LibraryLogic.Logic
{
    public interface IBookDao
    {
        void AddBook(Book book);
        void UpdateBook(BookUpdateData bookData);

        bool CanRemoveBook(int bookID);
        void RemoveBook(int bookID);

        Book GetBook(int bookID);
        List<Book> GetAllBooks();
        List<Book> GetBooksByAuthor(string author);
        List<Book> GetBooksByTitle(string title);
        List<Book> GetBooksByGenre(string genre);
        List<Book> GetBooksByState(BookState state);
       
        void BorrowBook(int bookID);
        void ReturnBook(int bookID);
    }
}
