using Microsoft.VisualStudio.TestTools.UnitTesting;
using LibraryLogic.LogicImplementations;
using LibraryLogic.Data;
using System.Collections.Generic;

namespace Library.Tests
{
    [TestClass()]
    public class RentalImplTests
    {
        private LibraryState libState;
        private RentalDaoBasicImpl rentalDao;

        [TestInitialize()]
        public void SetUp()
        {
            this.libState = new LibraryState();
            this.rentalDao = new RentalDaoBasicImpl(libState);
        }

        [TestCleanup()]
        public void TearDown()
        {
            this.rentalDao = null;
            this.libState = null;
        }


        [TestMethod()]
        public void AddRentalTest()
        {
            Book book = new Book("Turbo", "John Mango", "Crime");
            Rental rental = new Rental(book);
            rentalDao.AddRental(rental);

            Assert.AreEqual(rentalDao.GetAllRentals()[0], rental);
        }


        [TestMethod()]
        public void RemoveRentalTest()
        {
            Rental rental1 = new Rental(new Book("t", "a", "g"));
            Rental rental2 = new Rental(new Book("t", "a", "g"));

            rentalDao.AddRental(rental1);
            rentalDao.AddRental(rental2);

            Assert.AreEqual(rentalDao.GetAllRentals().Count, 2);
            rentalDao.RemoveRental(1);

            Assert.AreEqual(rentalDao.GetAllRentals().Count, 1);
            Assert.IsTrue(rentalDao.GetAllRentals().Contains(rental1));
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
