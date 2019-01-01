using System;
using System.Collections.Generic;
using System.Linq;
using LibraryLogic.Logic;
using LibraryLogic.Data;

namespace LibraryLogic.LogicImplementations
{
    public class UserDaoBasicImpl : IUsersDao
    {
        private LibraryState libraryData;

        public UserDaoBasicImpl(LibraryState libData)
        {
            libraryData = libData;
        }




        public void AddUser(User user)
        {
            libraryData.LibraryUsers.Add(user);
        }

        public void UpdateUser(UserUpdateData userData)
        {
            GetUser(userData.userId)?.UpdateUser(userData);
        }

        public bool CanRemoveUser(int userID)
        {
            if (GetUser(userID)?.BooksNumber == 0)
                return true;

            return false;
        }

        public void RemoveUser(int userID)
        {
            User user = GetUser(userID);

            if (user != null)
                libraryData.LibraryUsers.Remove(user);
            else
                throw new InvalidOperationException("RemoveUser: Cannot find specified user");
        }




        public User GetUser(int userID)
        {
            for (int i = 0; i < libraryData.LibraryUsers.Count; i++)
            {
                if (libraryData.LibraryUsers[i].ID == userID)
                {
                    return libraryData.LibraryUsers[i];
                }
            }
            return null;
        }

        public List<User> GetAllUsers()
        {
            return libraryData.LibraryUsers;
        }

        public List<User> GetUsersByFirstName(string firstName)
        {
            return GetAllUsers().Select(user => user).Where(user => user.FirstName.Equals(firstName)).ToList();
        }

        public List<User> GetUsersByLastName(string lastName)
        {
            return GetAllUsers().Select(user => user).Where(user => user.LastName.Equals(lastName)).ToList();
        }

        public List<User> GetUsersWithRentedBooks()
        {
            return GetAllUsers().Select(user => user).Where(user => (user.BooksNumber > 0)).ToList();
        }


        

        public void AddBook(int userID)
        {
            GetUser(userID)?.AddBook();
        }

        public void RemoveBook(int userID)
        {
            GetUser(userID)?.RemoveBook();
        }
    }
}
