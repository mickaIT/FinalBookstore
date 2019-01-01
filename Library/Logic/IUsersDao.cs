using System.Collections.Generic;
using LibraryLogic.Data;

namespace LibraryLogic.Logic
{
    public interface IUsersDao
    {
        void AddUser(User user);
        void UpdateUser(UserUpdateData userData);

        bool CanRemoveUser(int userID);
        void RemoveUser(int userID);

        User GetUser(int userID);
        List<User> GetAllUsers();
        List<User> GetUsersByFirstName(string firstName);
        List<User> GetUsersByLastName(string lastName);
        List<User> GetUsersWithRentedBooks();

        void AddBook(int userID);
        void RemoveBook(int userID);
    }
}