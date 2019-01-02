using System;
using System.Windows;
using System.Windows.Controls;
using BookstoreLogic.Data;

namespace WPFBookstore
{
    public partial class MainWindow : Window
    {
        /* Callback Actions */
        #region
        public void Refresh()
        {
            ShowList();
        }

        public void SellReturn(bool success)
        {
            ShowList();

            if (success)
            {
                /* Success */
                if (sellResultWindow == null || !sellResultWindow.IsLoaded)
                {
                    sellResultWindow = new SellResultWindow(SellResultType.SellSuccess);
                    sellResultWindow.Show();
                }
            }
        }
        #endregion


        /* Selecting Bookstore lists */
        #region
        private void BooksButton_Click(object sender, RoutedEventArgs e)
        {
            currentLLSelection = BookstoreListSelection.Books;
            UpdateButtons();
            Refresh();
        }

        private void RentalsButton_Click(object sender, RoutedEventArgs e)
        {
            currentLLSelection = BookstoreListSelection.Rentals;
            UpdateButtons();
            Refresh();
        }
        #endregion


        /* Adding */
        #region
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            /* Decide what to do basing on current Bookstore List selection */
            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
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
                    case BookstoreListSelection.Books:
                        {
                            Book book = (Book)selection;

                            if (editBookWindow == null || !editBookWindow.IsLoaded)
                            {
                                editBookWindow = new EditBookWindow(libService, book, Refresh);
                                editBookWindow.Show();
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


        /* Selling books */
        #region
        private void SellBookButton_Click(object sender, RoutedEventArgs e)
        {
            object selection = DataContextContainer.SelectedItem;

            if (selection != null)
            {
                Book book = (Book)selection;

                if (book.State != BookState.Available)
                {
                    /* Error */
                    if (sellResultWindow == null || !sellResultWindow.IsLoaded)
                    {
                        sellResultWindow = new SellResultWindow(SellResultType.CannotSell);
                        sellResultWindow.Show();
                    }
                }
                else
                {
                    /* Try to sell the book */
                    if (bookSellingWindow == null || !bookSellingWindow.IsLoaded)
                    {
                        bookSellingWindow = new BookSellingWindow(libService, book, SellReturn);
                        bookSellingWindow.Show();
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
                    case BookstoreListSelection.Books:
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
                case BookstoreListSelection.Books:
                    {
                        libService.RemoveAllBooks();
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
                case BookstoreListSelection.Books:
                    {
                        DataContextContainer.ItemsSource = libService.GetAllBooks();
                        break;
                    }
                case BookstoreListSelection.Rentals:
                    {
  
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


        /* Updating buttons when Bookstore List selection changes */
        #region
        private void UpdateButtons()
        {
            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                    {
                        ControlsText.Text = "Manage Books";
                        AddButton.Content = "Add Book";
                        EditButton.Content = "Edit Book";
                        RemoveButton.Content = "Remove Book";
                        RemoveAllButton.Content = "Remove All Books";

                        AddButton.Visibility = Visibility.Visible;
                        SellBookButton.Visibility = Visibility.Visible;
                        ReturnBookButton.Visibility = Visibility.Visible;
                        EditButton.Visibility = Visibility.Visible;

                        RemoveButton.Visibility = Visibility.Visible;
                        RemoveAllButton.Visibility = Visibility.Visible;
                        break;
                    }
                case BookstoreListSelection.Rentals:
                    {
                        ControlsText.Text = "No actions to perform";
                        AddButton.Content = "";
                        EditButton.Content = "";
                        RemoveButton.Content = "";
                        RemoveAllButton.Content = "";

                        AddButton.Visibility = Visibility.Hidden;
                        SellBookButton.Visibility = Visibility.Hidden;
                        ReturnBookButton.Visibility = Visibility.Hidden;
                        EditButton.Visibility = Visibility.Hidden;

                        RemoveButton.Visibility = Visibility.Hidden;
                        RemoveAllButton.Visibility = Visibility.Hidden;
                        break;
                    }
                case BookstoreListSelection.None:
                default:
                    {
                        ControlsText.Text = "";
                        AddButton.Content = "";
                        EditButton.Content = "";
                        RemoveButton.Content = "";
                        RemoveAllButton.Content = "";

                        AddButton.Visibility = Visibility.Hidden;
                        SellBookButton.Visibility = Visibility.Hidden;
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
