using System.Collections.Generic;

namespace LibraryLogic.Data
{
    public class BookstoreState
    {
        public List<Book> BookstoreBooks { get; set; }
        public List<Sale> BookInvoices { get; set; }

        
        public BookstoreState()
        {
            BookstoreBooks = new List<Book>();
            BookInvoices = new List<Sale>();
        }
    }
}
