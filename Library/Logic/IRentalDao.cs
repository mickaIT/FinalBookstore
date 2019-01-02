using System.Collections.Generic;
using BookstoreLogic.Data;

namespace BookstoreLogic.Logic
{
    public interface IRentalDao
    {
        void AddRental(Rental rental);
        void RemoveRental(int rentalISBN);
        void RemoveRental(Book soldBook);

        Rental GetRental(int rentalISBN);
        List<Rental> GetAllInvoices();
    }
}
