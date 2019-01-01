using System.Windows;

namespace WPFLibrary
{
    public partial class BookReturnResultWindow : Window
    {
        private void DisplayMessage(bool success)
        {
            if (success)
                MessageBlock.Text = "Book returned successfully.";
            else
                MessageBlock.Text = "Error. This book is not currently borrowed.";
        }



        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
