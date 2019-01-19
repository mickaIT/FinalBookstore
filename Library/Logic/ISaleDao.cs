using System.Collections.Generic;
using LibraryLogic.Data;

namespace LibraryLogic.Logic
{
    public interface ISaleDao
    {
        void AddSale(Sale sale);
        void RemoveSale(int saleISBN);
        void RemoveSale(Book soldBook);

        Sale GetSale(int saleISBN);
        List<Sale> GetAllInvoices();
    }
}
