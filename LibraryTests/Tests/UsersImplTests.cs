using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using LibraryLogic.LogicImplementations;
using LibraryLogic.Data;

namespace Library.Tests
{
    [TestClass()]
    public class UsersTests
    {
        private LibraryState libState;
        private UserDaoBasicImpl userDao;

        [TestInitialize()]
        public void SetUp()
        {
            this.libState = new LibraryState();
            this.userDao = new UserDaoBasicImpl(libState);
        }

        [TestCleanup()]
        public void TearDown()
        {
            this.userDao = null;
            this.libState = null;
        }

        [TestMethod()]
        public void AddUserTest()
        {
            //given
            User user = new User("Hom", "Tanks");
            //when
            userDao.AddUser(user);
            //then
            Assert.AreEqual(userDao.GetAllUsers()[0], user);
        }


        [TestMethod()]
        public void GetUsersByFirstNameTest()
        {
            //given
            User user1 = new User("John", "Hanks");
            User user2 = new User("Jack", "Hanks");
            User user3 = new User("Joseph", "Hanks");
            userDao.AddUser(user1);
            userDao.AddUser(user2);
            userDao.AddUser(user3);
            //when
            List<User> testUsers = userDao.GetUsersByFirstName(user2.FirstName);
            //then
            Assert.AreEqual(testUsers[0], user2);
        }


        [TestMethod()]
        public void GetUserByIdTest()
        {
            //given
            User user1 = new User("John", "Hanks");
            User user2 = new User("Jack", "Hanks");
            User user3 = new User("Joseph", "Hanks");
            userDao.AddUser(user1);
            userDao.AddUser(user2);
            userDao.AddUser(user3);
            //when
            User testUser = userDao.GetUser(user1.ID);
            //then
            Assert.AreEqual(testUser, user1);
        }

        [TestMethod()]
        public void GetUserByLastNameTest()
        {
            //given
            User user1 = new User("John", "Hanks");
            User user2 = new User("Jack", "Banks");
            User user3 = new User("Joseph", "Xanks");
            userDao.AddUser(user1);
            userDao.AddUser(user2);
            userDao.AddUser(user3);
            //when
            List<User> testUser = userDao.GetUsersByLastName(user1.LastName);
            //then
            Assert.AreEqual(testUser, user1);
        }

        [TestMethod()]
        public void RemoveUserTest()
        {
            //given
            User user1 = new User("John", "Lennon");
            User user2 = new User("Mike", "Love");
            User user3 = new User("Ringo", "Starr");
            userDao.AddUser(user1);
            userDao.AddUser(user2);
            userDao.AddUser(user3);
            //when
            userDao.RemoveUser(2);
            //then
            List<User> allUsers = userDao.GetAllUsers();
            Assert.IsTrue(allUsers.Count == 2);
            Assert.IsTrue(allUsers.Contains(user1));
            Assert.IsFalse(allUsers.Contains(user3));
            Assert.IsTrue(allUsers.Contains(user2));
        }

        [TestMethod()]
        public void UpdateUserTest()
        {
            //given
            User user1 = new User("John", "Lennon");
            User user2 = new User("Mike", "Love");
            User user3 = new User("Ringo", "Starr");
            userDao.AddUser(user1);
            userDao.AddUser(user2);
            userDao.AddUser(user3);
            //when
            UserUpdateData data = new UserUpdateData(1, "Majk", "Bove");
            userDao.UpdateUser(data);
            //then
            Assert.AreEqual(userDao.GetUser(1).FirstName, "Majk");
        }

        [TestMethod()]
        public void CanRemoveUserTest()
        {
            //given
            User user = new User("Hom", "Tanks");
            //when
            userDao.AddUser(user);
            Assert.AreEqual(userDao.CanRemoveUser(0), true);

            userDao.AddBook(0);
            //then
            Assert.AreEqual(userDao.CanRemoveUser(0), false);
        }

        [TestMethod()]
        public void GetUsersWithRentedBooksTest()
        {
            //given
            User user1 = new User("John", "Hanks");
            User user2 = new User("Jack", "Banks");
            User user3 = new User("Joseph", "Xanks");
            userDao.AddUser(user1);
            userDao.AddUser(user2);
            userDao.AddUser(user3);

            userDao.AddBook(0);
            //when
            List<User> testUsers = userDao.GetUsersWithRentedBooks();
            //then
            Assert.AreEqual(testUsers[0], user1);
        }

    }
}