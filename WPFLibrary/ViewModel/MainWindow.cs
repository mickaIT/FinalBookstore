using System;
using System.Windows;
using System.Windows.Controls;
using LibraryLogic.Data;

namespace WPFLibrary
{
    public partial class MainWindow : Window
    {
        /* Callback Actions */
        #region
        public void Refresh()
        {
            ShowList();
        }

        public void RentReturn(bool success)
        {
            ShowList();

            if (success)
            {
                /* Success */
                if (rentResultWindow == null || !rentResultWindow.IsLoaded)
                {
                    rentResultWindow = new RentResultWindow(RentResultType.RentSuccess);
                    rentResultWindow.Show();
                }
            }
            else
            {
                /* Failure */
                if (rentResultWindow == null || !rentResultWindow.IsLoaded)
                {
                    rentResultWindow = new RentResultWindow(RentResultType.UserNotFound);
                    rentResultWindow.Show();
                }
            }
        }
        #endregion


        /* Selecting Library lists */
        #region
        private void UsersButton_Click(object sender, RoutedEventArgs e)
        {
            currentLLSelection = LibraryListSelection.Users;
            UpdateButtons();
            Refresh();
        }

        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            currentLLSelection = LibraryListSelection.Books;
            UpdateButtons();
            Refresh();
        }

        private void RentalsButton_Click(object sender, RoutedEventArgs e)
        {
            currentLLSelection = LibraryListSelection.Rentals;
            UpdateButtons();
            Refresh();
        }
        #endregion


        /* Adding */
        #region
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            /* Decide what to do basing on current Library List selection */
            switch (currentLLSelection)
            {
                case LibraryListSelection.Users:
                    {
                        if (addUserWindow == null || !addUserWindow.IsLoaded)
                        {
                            addUserWindow = new AddUserWindow(libService, Refresh);
                            addUserWindow.Show();
                        }
                        break;
                    }
                case LibraryListSelection.Books:
                    {
                        if (addBookWindow == null || !addBookWindow.IsLoaded)
                        {
                            addBookWindow = new AddBookWindow(libService, Refresh);
                            addBookWindow.Show();
                        }
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion


        /* Editing */
        #region
        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            object selection = DataContextContainer.SelectedItem;

            if (selection != null)
            {
                switch (currentLLSelection)
                {
                    case LibraryListSelection.Books:
                        {
                            Book book = (Book)selection;

                            if (editBookWindow == null || !editBookWindow.IsLoaded)
                            {
                                editBookWindow = new EditBookWindow(libService, book, Refresh);
                                editBookWindow.Show();
                            }
                            break;
                        }
                    case LibraryListSelection.Users:
                        {
                            User user = (User)selection;

                            if (editUserWindow == null || !editUserWindow.IsLoaded)
                            {
                                editUserWindow = new EditUserWindow(libService, user, Refresh);
                                editUserWindow.Show();
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }
        #endregion


        /* Renting books */
        #region
        private void RentBookButton_Click(object sender, RoutedEventArgs e)
        {
            object selection = DataContextContainer.SelectedItem;

            if (selection != null)
            {
                Book book = (Book)selection;

                if (book.State != BookState.Available)
                {
                    /* Error */
                    if (rentResultWindow == null || !rentResultWindow.IsLoaded)
                    {
                        rentResultWindow = new RentResultWindow(RentResultType.CannotBorrow);
                        rentResultWindow.Show();
                    }
                }
                else
                {
                    /* Try to rent the book */
                    if (bookRentingWindow == null || !bookRentingWindow.IsLoaded)
                    {
                        bookRentingWindow = new BookRentingWindow(libService, book, RentReturn);
                        bookRentingWindow.Show();
                    }
                }
            }
        }

        private void ReturnBookButton_Click(object sender, RoutedEventArgs e)
        {
            object selection = DataContextContainer.SelectedItem;

            if (selection != null)
            {
                Book book = (Book)selection;

                if (book.State == BookState.Available)
                {
                    /* Error */ 
                    if (bookReturnResultWindow == null || !bookReturnResultWindow.IsLoaded)
                    {
                        bookReturnResultWindow = new BookReturnResultWindow(false);
                        bookReturnResultWindow.Show();
                    }
                }
                else
                {
                    libService.RemoveRental(book);

                    /* Success */
                    if (bookReturnResultWindow == null || !bookReturnResultWindow.IsLoaded)
                    {
                        bookReturnResultWindow = new BookReturnResultWindow(true);
                        bookReturnResultWindow.Show();
                    }
                }

                Refresh();
            }
        }
        #endregion


        /* Removing */
        #region
        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            object selection = DataContextContainer.SelectedItem;

            if (selection != null)
            {
                switch (currentLLSelection)
                {
                    case LibraryListSelection.Books:
                        {
                            Book book = (Book)selection;

                            if (libService.CanRemoveBook(book.ID))
                            {
                                libService.RemoveBook(book);
                                Refresh();
                            }
                            else if (removalErrorWindow == null || !removalErrorWindow.IsLoaded)
                            {
                                removalErrorWindow = new CannotRemoveErrorWindow(book);
                                removalErrorWindow.Show();
                            }
                            break;
                        }
                    case LibraryListSelection.Users:
                        {
                            User user = (User)selection;

                            if (libService.CanRemoveUser(user.ID))
                            {
                                libService.RemoveUser(user);
                                Refresh();
                            }
                            else if (removalErrorWindow == null || !removalErrorWindow.IsLoaded)
                            {
                                removalErrorWindow = new CannotRemoveErrorWindow(user);
                                removalErrorWindow.Show();
                            }
                            break;
                        }
                    default:
                        {
                            break;
                        }
                }
            }
        }

        private void RemoveAllButton_Click(object sender, RoutedEventArgs e)
        {
            switch (currentLLSelection)
            {
                case LibraryListSelection.Books:
                    {
                        libService.RemoveAllBooks();
                        Refresh();
                        break;
                    }
                case LibraryListSelection.Users:
                    {
                        libService.RemoveAllUsers();
                        Refresh();
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }
        #endregion


        /* Showing different lists */
        #region
        private void ShowList()
        {
            DataContextContainer.ItemsSource = null;

            switch (currentLLSelection)
            {
                case LibraryListSelection.Users:
                    {
                        DataContextTitle.Content = "Users";
                        DataContextContainer.ItemsSource = libService.GetAllUsers();
                        break;
                    }
                case LibraryListSelection.Books:
                    {
                        DataContextTitle.Content = "Books";
                        DataContextContainer.ItemsSource = libService.GetAllBooks();
                        break;
                    }
                case LibraryListSelection.Rentals:
                    {
                        DataContextTitle.Content = "Rentals";
                        DataContextContainer.ItemsSource = libService.GetAllRentals();
                        break;
                    }
                default:
                    {
                        DataContextTitle.Content = "";
                        break;
                    }
            }
        }
#endregion


        /* Updating buttons when Library List selection changes */
        #region
        private void UpdateButtons()
        {
            switch (currentLLSelection)
            {
                case LibraryListSelection.Books:
                    {
                        ControlsText.Text = "Manage Books";
                        AddButton.Content = "Add Book";
                        EditButton.Content = "Edit Book";
                        RemoveButton.Content = "Remove Book";
                        RemoveAllButton.Content = "Remove All Books";

                        AddButton.Visibility = Visibility.Visible;
                        RentBookButton.Visibility = Visibility.Visible;
                        ReturnBookButton.Visibility = Visibility.Visible;
                        EditButton.Visibility = Visibility.Visible;

                        RemoveButton.Visibility = Visibility.Visible;
                        RemoveAllButton.Visibility = Visibility.Visible;
                        break;
                    }
                case LibraryListSelection.Users:
                    {
                        ControlsText.Text = "Manage Users";
                        AddButton.Content = "Add User";
                        EditButton.Content = "Edit User";
                        RemoveButton.Content = "Remove User";
                        RemoveAllButton.Content = "Remove All Users";

                        AddButton.Visibility = Visibility.Visible;
                        RentBookButton.Visibility = Visibility.Hidden;
                        ReturnBookButton.Visibility = Visibility.Hidden;
                        EditButton.Visibility = Visibility.Visible;

                        RemoveButton.Visibility = Visibility.Visible;
                        RemoveAllButton.Visibility = Visibility.Visible;
                        break;
                    }
                case LibraryListSelection.Rentals:
                    {
                        ControlsText.Text = "No actions to perform";
                        AddButton.Content = "";
                        EditButton.Content = "";
                        RemoveButton.Content = "";
                        RemoveAllButton.Content = "";

                        AddButton.Visibility = Visibility.Hidden;
                        RentBookButton.Visibility = Visibility.Hidden;
                        ReturnBookButton.Visibility = Visibility.Hidden;
                        EditButton.Visibility = Visibility.Hidden;

                        RemoveButton.Visibility = Visibility.Hidden;
                        RemoveAllButton.Visibility = Visibility.Hidden;
                        break;
                    }
                case LibraryListSelection.None:
                default:
                    {
                        ControlsText.Text = "";
                        AddButton.Content = "";
                        EditButton.Content = "";
                        RemoveButton.Content = "";
                        RemoveAllButton.Content = "";

                        AddButton.Visibility = Visibility.Hidden;
                        RentBookButton.Visibility = Visibility.Hidden;
                        ReturnBookButton.Visibility = Visibility.Hidden;
                        EditButton.Visibility = Visibility.Hidden;

                        RemoveButton.Visibility = Visibility.Hidden;
                        RemoveAllButton.Visibility = Visibility.Hidden;
                        break;
                    }
            }
        }
        #endregion


        /* Window events */
        #region
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContextContainer_SelectionChanged(this.DataContextContainer, null);
        }

        private void DataContextContainer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        #endregion
    }
}
