using System.Collections.Generic;
using LibraryLogic.Data;

namespace LibraryLogic.Logic
{
    public interface IRentalDao
    {
        void AddRental(Rental rental);
        void RemoveRental(int rentalID);
        void RemoveRental(Book rentedBook);

        Rental GetRental(int rentalID);
        List<Rental> GetAllRentals();
        List<Rental> GetRentalsByUsername(string username);
    }
}
