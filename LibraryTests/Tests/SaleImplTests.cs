using Microsoft.VisualStudio.TestTools.UnitTesting;
using BookstoreLogic.LogicImplementations;
using BookstoreLogic.Data;
using System.Collections.Generic;

namespace Bookstore.Tests
{
    [TestClass()]
    public class SaleImplTests
    {
        private BookstoreState bookstoreState;
        private SaleDaoBasicImpl saleDao;

        [TestInitialize()]
        public void SetUp()
        {
            this.bookstoreState = new BookstoreState();
            this.saleDao = new SaleDaoBasicImpl(bookstoreState);
        }

        [TestCleanup()]
        public void TearDown()
        {
            this.saleDao = null;
            this.bookstoreState = null;
        }


        [TestMethod()]
        public void AddSaleTest()
        {
            Book book = new Book("Turbo", "John Mango", "Crime");
            Sale sale = new Sale(book);
            saleDao.AddSale(sale);

            Assert.AreEqual(saleDao.GetAllInvoices()[0], sale);
        }


        [TestMethod()]
        public void RemoveSaleTest()
        {
            Sale sale1 = new Sale(new Book("t", "a", "g"));
            Sale sale2 = new Sale(new Book("t", "a", "g"));

            saleDao.AddSale(sale1);
            saleDao.AddSale(sale2);

            Assert.AreEqual(saleDao.GetAllInvoices().Count, 2);
            saleDao.RemoveSale(1);

            Assert.AreEqual(saleDao.GetAllInvoices().Count, 1);
            Assert.IsTrue(saleDao.GetAllInvoices().Contains(sale1));
        }



        [TestMethod()]
        public void GetSaleTest()
        {
            Sale sale1 = new Sale(new Book("t", "a", "g"));
            Sale sale2 = new Sale(new Book("t", "a", "g"));

            saleDao.AddSale(sale1);
            saleDao.AddSale(sale2);

            Sale testSale = saleDao.GetSale(1);
            Assert.AreEqual(sale2, testSale);
        }

    }

}
