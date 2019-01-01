using System;
using System.Collections.Generic;
using System.Linq;
using LibraryLogic.Logic;
using LibraryLogic.Data;

namespace LibraryLogic.LogicImplementations
{
    public class RentalDaoBasicImpl : IRentalDao
    {
        private LibraryState libraryData;

        public RentalDaoBasicImpl(LibraryState libData)
        {
            libraryData = libData;
        }




        public void AddRental(Rental rental)
        {
            libraryData.BookRentals.Add(rental);
        }

        public void RemoveRental(int rentalID)
        {
            Rental rental = GetRental(rentalID);
                
            if (rental != null)
            {
                rental.EndRental();
                libraryData.BookRentals.Remove(rental);
            }
            else
                throw new InvalidOperationException("RemoveRental: Cannot find specified rental");
        }

        public void RemoveRental(Book rentedBook)
        {
            for (int i = 0; i < libraryData.BookRentals.Count; i++)
            {
                if (libraryData.BookRentals[i].RentedBook.ID == rentedBook.ID)
                {
                    RemoveRental(libraryData.BookRentals[i].ID);
                }
            } 
        }


        public Rental GetRental(int rentalID)
        {
            for (int i = 0; i < libraryData.BookRentals.Count; i++)
            {
                if (libraryData.BookRentals[i].ID == rentalID)
                {
                    return libraryData.BookRentals[i];
                }
            }
            return null;
        }

        public List<Rental> GetAllRentals()
        {
            return libraryData.BookRentals;
        }

        public List<Rental> GetRentalsByUsername(string username)
        {
            return GetAllRentals().Select(rental => rental).Where(rental => rental.AssociatedUser.GetUserName().Equals(username)).ToList();
        }
    }
}
