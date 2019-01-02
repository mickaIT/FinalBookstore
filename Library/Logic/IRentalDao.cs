using System.Collections.Generic;
using BookstoreLogic.Data;

namespace BookstoreLogic.Logic
{
    public interface IRentalDao
    {
        void AddRental(Rental rental);
        void RemoveRental(int rentalID);
        void RemoveRental(Book rentedBook);

        Rental GetRental(int rentalID);
        List<Rental> GetAllRentals();
    }
}
