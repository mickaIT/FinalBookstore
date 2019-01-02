using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookstoreLogic.LogicImplementations;
using BookstoreLogic.Data;
using System.Collections.Generic;

namespace Bookstore.Tests
{
    [TestClass()]
    public class RentalImplTests
    {
        private BookstoreState bookstoreState;
        private RentalDaoBasicImpl rentalDao;

        [TestInitialize()]
        public void SetUp()
        {
            this.bookstoreState = new BookstoreState();
            this.rentalDao = new RentalDaoBasicImpl(bookstoreState);
        }

        [TestCleanup()]
        public void TearDown()
        {
            this.rentalDao = null;
            this.bookstoreState = null;
        }


        [TestMethod()]
        public void AddRentalTest()
        {
            Book book = new Book("Turbo", "John Mango", "Crime");
            Rental rental = new Rental(book);
            rentalDao.AddRental(rental);

            Assert.AreEqual(rentalDao.GetAllInvoices()[0], rental);
        }


        [TestMethod()]
        public void RemoveRentalTest()
        {
            Rental rental1 = new Rental(new Book("t", "a", "g"));
            Rental rental2 = new Rental(new Book("t", "a", "g"));

            rentalDao.AddRental(rental1);
            rentalDao.AddRental(rental2);

            Assert.AreEqual(rentalDao.GetAllInvoices().Count, 2);
            rentalDao.RemoveRental(1);

            Assert.AreEqual(rentalDao.GetAllInvoices().Count, 1);
            Assert.IsTrue(rentalDao.GetAllInvoices().Contains(rental1));
        }



        [TestMethod()]
        public void GetRentalTest()
        {
            Rental rental1 = new Rental(new Book("t", "a", "g"));
            Rental rental2 = new Rental(new Book("t", "a", "g"));

            rentalDao.AddRental(rental1);
            rentalDao.AddRental(rental2);

            Rental testRental = rentalDao.GetRental(1);
            Assert.AreEqual(rental2, testRental);
        }

    }

}
