using System;
using System.Threading;

namespace BookstoreLogic.Data
{
    public class Rental
    {
        public static int RentalIDCounter = -1;

        public int ID { get; }
        public Book SoldBook { get; set; }
        public string InvoiceDate { get; set; }

        /* Creating rental */
        public Rental(Book book)
        {
            ID = Interlocked.Increment(ref RentalIDCounter);
            SoldBook = book;            

            InvoiceDate = DateTime.UtcNow.ToLocalTime().ToString("dd-MM-yyyy hh:mm:ss");

            StartRental();
        }

        /* Called just after creating this rental */
        private void StartRental()
        {
            SoldBook.SellBook();
        }

        /* Called just before removing this rental from the list */
        public void EndRental()
        {
            SoldBook.ReturnBook();
        }


        public override string ToString()
        {
            return "[ID: " + ID + "]"
                    + ",  Sold book: " + SoldBook.Title
                    +", ISBN: " + SoldBook.ISBN
                    + ",  Invoice date: " + InvoiceDate;
        }
    }
}
