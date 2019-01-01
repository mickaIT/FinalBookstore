using System.Windows;
using System.Windows.Controls;
using LibraryLogic.Data;

namespace WPFLibrary
{
    public partial class BookRentingWindow : Window
    {
        private int rentedBookId;

        private void SetupWindow(Book book)
        {
            rentedBookId = book.ID;
            ShowList();          
        }


        /* Buttons */
        #region
        private void RentBookButton_Click(object sender, RoutedEventArgs e)
        {
            object selection = DataContextContainer.SelectedItem;

            if (selection != null)
            {
                User user = (User)selection;
                libService.AddRental(rentedBookId, user.ID);
                windowCallback(true);
                this.Close();
            }
            else
            {
                windowCallback(false);
                this.Close();
            } 
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        #endregion


        /* Showing user list */
        #region
        private void ShowList()
        {
            DataContextTitle.Content = "Users";
            DataContextContainer.ItemsSource = libService.GetAllUsers();
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
