using BookstoreLogic.Logic;
using BookstoreLogic.Data;

namespace BookstoreLogic.LogicImplementations
{
    public class ConstantsBookstoreFiller : IBookstoreFiller
    {
        public void Fill(BookstoreState state)
        {
            FillBooks(state);
            FillRentals(state);
        }

        

        private void FillBooks(BookstoreState state)
        {
            state.BookstoreBooks.Add(new Book("The Chemistry of Death", "Simon Beckett", "Crime"));
            state.BookstoreBooks.Add(new Book("The Chemistry of Death", "Simon Beckett", "Crime"));
            state.BookstoreBooks.Add(new Book("Whistleblower", "Tess Gerritsen", "Crime"));
            state.BookstoreBooks.Add(new Book("Whistleblower", "Tess Gerritsen", "Crime"));
            state.BookstoreBooks.Add(new Book("Whistleblower", "Tess Gerritsen", "Crime"));
            state.BookstoreBooks.Add(new Book("Roses are Red", "James Patterson", "Thriller"));
            state.BookstoreBooks.Add(new Book("Brief Answers to the Big Questions", "Stephen Hawking", "Science"));
        }
        
        private void FillRentals(BookstoreState state)
        {
            state.BookRentals.Add(new Rental(state.BookstoreBooks[1]));
            state.BookRentals.Add(new Rental(state.BookstoreBooks[3]));
            state.BookRentals.Add(new Rental(state.BookstoreBooks[5]));
            state.BookRentals.Add(new Rental(state.BookstoreBooks[0]));
            state.BookRentals.Add(new Rental(state.BookstoreBooks[4]));
        }
    }
}
