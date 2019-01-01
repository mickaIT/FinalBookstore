using System;
using System.Threading;

namespace LibraryLogic.Data
{
    public class Rental
    {
        public static int RentalIDCounter = -1;

        public int ID { get; }
        public Book RentedBook { get; set; }
        public User AssociatedUser { get; set; }
        public string RentalStartDate { get; set; }

        /* Creating rental */
        public Rental(Book book, User user)
        {
            ID = Interlocked.Increment(ref RentalIDCounter);
            RentedBook = book;            
            AssociatedUser = user;
            RentalStartDate = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss");

            StartRental();
        }

        /* Called just after creating this rental */
        private void StartRental()
        {
            RentedBook.BorrowBook();
            AssociatedUser.AddBook();
        }

        /* Called just before removing this rental from the list */
        public void EndRental()
        {
            RentedBook.ReturnBook();
            AssociatedUser.RemoveBook();
        }


        public override string ToString()
        {
            return "[ID: " + ID + "]"
                    + ",  Rented book: " + RentedBook.Title
                    + ",  Rented by: " + AssociatedUser.GetUserName() 
                    + ",  Start date: " + RentalStartDate;
        }
    }
}
