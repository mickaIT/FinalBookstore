using System.Collections.Generic;

namespace LibraryLogic.Data
{
    public class LibraryState
    {
        public List<Book> LibraryBooks { get; set; }
        public List<User> LibraryUsers { get; set; }
        public List<Rental> BookRentals { get; set; }

        
        public LibraryState()
        {
            LibraryBooks = new List<Book>();
            LibraryUsers = new List<User>();
            BookRentals = new List<Rental>();
        }
    }
}
