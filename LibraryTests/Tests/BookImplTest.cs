using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookstoreLogic.Data;
using BookstoreLogic.LogicImplementations;
using System.Collections.Generic;

namespace BookstoreTests
{
    [TestClass]
    public class BookImplTest
    {
        private BookstoreState bookstoreState;
        private BookDaoBasicImpl bookDao;

        [TestInitialize()]
        public void SetUp()
        {
            bookstoreState = new BookstoreState();
            bookDao = new BookDaoBasicImpl(bookstoreState);
        }

        [TestCleanup()]
        public void TearDown()
        {
            bookDao = null;
            bookstoreState = null;
        }

        [TestMethod()]
        public void AddBookTest()
        {
            //given
            Book book = new Book("Magic Title", "Harry Potter", "horror");
            //when
            bookDao.AddBook(book);
            //then
            Assert.AreEqual(bookDao.GetAllBooks()[0], book);
        }

        [TestMethod()]
        public void GetBookByIdTest()
        {
            //given
            Book book1 = new Book("Title1", "Tset", "Ttse");
            Book book2 = new Book("Title2", "Tset", "Ttse");
            Book book3 = new Book("Title3", "Le", "Ttse");
            bookDao.AddBook(book1);
            bookDao.AddBook(book2);
            bookDao.AddBook(book3);
            //then
            Assert.AreEqual(bookDao.GetBook(book2.ISBN), book2);
        }

        [TestMethod()]
        public void GetBookByAuthorTest()
        {
            //given
            Book book1 = new Book("Title1", "Tset", "Ttse");
            Book book2 = new Book("Title2", "Tset", "Ttse");
            Book book3 = new Book("Title3", "Le", "Ttse");
            bookDao.AddBook(book1);
            bookDao.AddBook(book2);
            bookDao.AddBook(book3);
            //when
            List<Book> result = bookDao.GetBooksByAuthor(book1.Author);
            //then
            Assert.IsTrue(result.Count == 2);     
            Assert.IsTrue(result.Contains(book1));
            Assert.IsTrue(result.Contains(book2));
        }

        [TestMethod()]
        public void GetBookByTitleTest()
        {
            //given
            Book book1 = new Book("Title1", "Tset", "Ttse");
            Book book2 = new Book("Title2", "Tset", "Ttse");
            Book book3 = new Book("Title1", "Le", "Ttse");
            bookDao.AddBook(book1);
            bookDao.AddBook(book2);
            bookDao.AddBook(book3);
            //when
            List<Book> result = bookDao.GetBooksByTitle(book1.Title);
            //then
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(book1));
            Assert.IsTrue(result.Contains(book3));
        }

        [TestMethod()]
        public void GetBookByGenreTest()
        {
            //given
            Book book1 = new Book("Title1", "Tset", "AaA");
            Book book2 = new Book("Title2", "Tset", "Ttse");
            Book book3 = new Book("Title3", "Le", "Ttse");
            bookDao.AddBook(book1);
            bookDao.AddBook(book2);
            bookDao.AddBook(book3);
            //when
            List<Book> result = bookDao.GetBooksByGenre(book1.Genre);
            //then
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result.Contains(book1));
        }

        [TestMethod()]
        public void SellBookTest()
        {
            //given
            Book book1 = new Book("Title1", "Steve Jobs", "fantasy");
            bookDao.AddBook(book1);
            //when
            bookDao.SellBook(book1.ISBN);
            //then
            Assert.AreEqual(bookDao.GetBook(book1.ISBN).State, BookState.Unavailable);
        }

        [TestMethod()]
        public void ReturnBookTest()
        {
            //given
            Book book1 = new Book("Title1", "George Washington", "horror");
            bookDao.AddBook(book1);
            //when
            bookDao.SellBook(book1.ISBN);
            bookDao.ReturnBook(book1.ISBN);
            //then
            Assert.AreEqual(bookDao.GetBook(book1.ISBN).State, BookState.Available);
        }

        [TestMethod()]
        public void GetBookByStateTest()
        {
            //given
            Book book1 = new Book("Title1", "Test", "Ttse");
            Book book2 = new Book("Title2", "Tset", "Ttse");
            Book book3 = new Book("Title3", "Le", "Ttse");
            bookDao.AddBook(book1);
            bookDao.AddBook(book2);
            bookDao.AddBook(book3);

            bookDao.SellBook(book1.ISBN);
           
            List<Book> soldOut = bookDao.GetBooksByState(BookState.Unavailable);

            Assert.IsTrue(soldOut.Count == 1);
            Assert.IsTrue(soldOut.Contains(book1));
        }

        [TestMethod()]
        public void RemoveBookTest()
        {
            //given
            Book book1 = new Book("Title1", "Test", "Ttse");
            Book book2 = new Book("Title2", "Tset", "Ttse");
            Book book3 = new Book("Title3", "Le", "Ttse");
            bookDao.AddBook(book1);
            bookDao.AddBook(book2);
            bookDao.AddBook(book3);
            //when
            bookDao.RemoveBook(book2.ISBN);
            //then
            List<Book> result = bookDao.GetAllBooks();
            Assert.IsTrue(result.Count == 2);
            Assert.IsTrue(result.Contains(book1));
            Assert.IsTrue(result.Contains(book3));
        }

        [TestMethod()]
        public void CanRemoveBookTest()
        {
            //given
            Book book1 = new Book("Title1", "Test", "Ttse");
            bookDao.AddBook(book1);
            //when
            bookDao.SellBook(book1.ISBN);
            //then
            Assert.IsFalse(bookDao.CanRemoveBook(book1.ISBN));
        }

    }
}
