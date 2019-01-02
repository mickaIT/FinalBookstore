using System;
using System.Collections.Generic;
using System.Linq;
using BookstoreLogic.Logic;
using BookstoreLogic.Data;

namespace BookstoreLogic.LogicImplementations
{
    public class RentalDaoBasicImpl : IRentalDao
    {
        private BookstoreState BookstoreData;

        public RentalDaoBasicImpl(BookstoreState bookstoreData)
        {
            BookstoreData = bookstoreData;
        }




        public void AddRental(Rental rental)
        {
            BookstoreData.BookInvoices.Add(rental);
        }

        public void RemoveRental(int rentalISBN)
        {
            Rental rental = GetRental(rentalISBN);
                
            if (rental != null)
            {
                rental.EndRental();
                BookstoreData.BookInvoices.Remove(rental);
            }
            else
                throw new InvalidOperationException("RemoveRental: Cannot find specified rental");
        }

        public void RemoveRental(Book soldBook)
        {
            for (int i = 0; i < BookstoreData.BookInvoices.Count; i++)
            {
                if (BookstoreData.BookInvoices[i].SoldBook.ISBN == soldBook.ISBN)
                {
                    RemoveRental(BookstoreData.BookInvoices[i].ID);
                }
            } 
        }


        public Rental GetRental(int rentalISBN)
        {
            for (int i = 0; i < BookstoreData.BookInvoices.Count; i++)
            {
                if (BookstoreData.BookInvoices[i].ID == rentalISBN)
                {
                    return BookstoreData.BookInvoices[i];
                }
            }
            return null;
        }

        public List<Rental> GetAllInvoices()
        {
            return BookstoreData.BookInvoices;
        }
    }
}
