using LibraryLogic.Data;
using LibraryLogic.Logic;
using System.Linq;

namespace LibraryLogic.Logic_Implementations
{
    public class ConstantsBookstoreFiller : IBookstoreFiller
    {
        public void Fill(Model.BookstoreDataContext state)
        {
            FillBooks(state);
            FillInvoices(state);
        }

        

        private void FillBooks(Model.BookstoreDataContext state)
        {
            state.AddBook(new Book("Ślepnąc od świateł", "Jakub Żulczyk", "Modern literature", 1));
            state.AddBook(new Book("Tatuażysta z Auschwitz", "Heather Morris ", "Historic", 5));
            state.AddBook(new Book("Myszy i ludzie", "John Steinbeck", "Modern literature", 3));
            state.AddBook(new Book("Opowieść podręcznej", "Margaret Atwood", "Modern literature", 4));
            state.AddBook(new Book("Wielki Gatsby", "F. Scott Fitzgerald", "Drama", 2));
            state.AddBook(new Book("Pachnidło. Historia pewnego mordercy", "Patrick Süskind", "Drama", 2));
            state.AddBook(new Book("Mój piękny syn", "David Sheff", "Biography", 2));
        }
        
        private void FillInvoices(Model.BookstoreDataContext state)
        {
            state.AddSale(new Sale(new Book("Ślepnąc od świateł", "Jakub Żulczyk", "Modern literature", 1, 0)));
            //state.AddSale(new Sale(state.BookstoreBooks[3]));
            //state.AddSale(new Sale(state.BookstoreBooks[5]));
            //state.AddSale(new Sale(state.BookstoreBooks[0]));
            //state.AddSale(new Sale(state.BookstoreBooks[4]));
        }
    }
}
