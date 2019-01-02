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
        }


        /* Buttons */
        #region
        private void RentBookButton_Click(object sender, RoutedEventArgs e)
        {
            try { 
                libService.AddRental(rentedBookId);
                windowCallback(true);
                this.Close();
            }
            catch
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

    }
}
