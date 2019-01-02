using System.Windows;
using System.Windows.Controls;
using BookstoreLogic.Data;

namespace WPFBookstore
{
    public partial class BookSellingWindow : Window
    {
        private int soldBookId;

        private void SetupWindow(Book book)
        {
            soldBookId = book.ID;
        }


        /* Buttons */
        #region
        private void SellBookButton_Click(object sender, RoutedEventArgs e)
        {
            try { 
                libService.AddRental(soldBookId);
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
