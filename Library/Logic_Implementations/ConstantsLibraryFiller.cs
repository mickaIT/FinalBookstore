using LibraryLogic.Logic;
using LibraryLogic.Data;

namespace LibraryLogic.LogicImplementations
{
    public class ConstantsLibraryFiller : ILibraryFiller
    {
        public void Fill(LibraryState state)
        {
            FillBooks(state);
            FillUsers(state);
            FillRentals(state);
        }

        

        private void FillBooks(LibraryState state)
        {
            state.LibraryBooks.Add(new Book("The Chemistry of Death", "Simon Beckett", "Crime"));
            state.LibraryBooks.Add(new Book("The Chemistry of Death", "Simon Beckett", "Crime"));
            state.LibraryBooks.Add(new Book("Whistleblower", "Tess Gerritsen", "Crime"));
            state.LibraryBooks.Add(new Book("Whistleblower", "Tess Gerritsen", "Crime"));
            state.LibraryBooks.Add(new Book("Whistleblower", "Tess Gerritsen", "Crime"));
            state.LibraryBooks.Add(new Book("Roses are Red", "James Patterson", "Thriller"));
            state.LibraryBooks.Add(new Book("Brief Answers to the Big Questions", "Stephen Hawking", "Science"));
        }

        private void FillUsers(LibraryState state)
        {
            state.LibraryUsers.Add(new User("Jake", "Nadd"));
            state.LibraryUsers.Add(new User("Jack", "Sparrow"));
            state.LibraryUsers.Add(new User("David", "Hunter"));
            state.LibraryUsers.Add(new User("Freddie", "Mercury"));
            state.LibraryUsers.Add(new User("Khem", "Val"));
        }
        
        private void FillRentals(LibraryState state)
        {
            state.BookRentals.Add(new Rental(state.LibraryBooks[1], state.LibraryUsers[4]));
            state.BookRentals.Add(new Rental(state.LibraryBooks[3], state.LibraryUsers[3]));
            state.BookRentals.Add(new Rental(state.LibraryBooks[5], state.LibraryUsers[4]));
            state.BookRentals.Add(new Rental(state.LibraryBooks[0], state.LibraryUsers[1]));
            state.BookRentals.Add(new Rental(state.LibraryBooks[4], state.LibraryUsers[0]));
        }
    }
}
