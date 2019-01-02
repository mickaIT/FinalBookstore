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

        public RentalDaoBasicImpl(BookstoreState libData)
        {
            BookstoreData = libData;
        }




        public void AddRental(Rental rental)
        {
            BookstoreData.BookRentals.Add(rental);
        }

        public void RemoveRental(int rentalID)
        {
            Rental rental = GetRental(rentalID);
                
            if (rental != null)
            {
                rental.EndRental();
                BookstoreData.BookRentals.Remove(rental);
            }
            else
                throw new InvalidOperationException("RemoveRental: Cannot find specified rental");
        }

        public void RemoveRental(Book rentedBook)
        {
            for (int i = 0; i < BookstoreData.BookRentals.Count; i++)
            {
                if (BookstoreData.BookRentals[i].RentedBook.ID == rentedBook.ID)
                {
                    RemoveRental(BookstoreData.BookRentals[i].ID);
                }
            } 
        }


        public Rental GetRental(int rentalID)
        {
            for (int i = 0; i < BookstoreData.BookRentals.Count; i++)
            {
                if (BookstoreData.BookRentals[i].ID == rentalID)
                {
                    return BookstoreData.BookRentals[i];
                }
            }
            return null;
        }

        public List<Rental> GetAllRentals()
        {
            return BookstoreData.BookRentals;
        }
    }
}
