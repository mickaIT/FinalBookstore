using System.Windows;

namespace WPFLibrary
{
    public partial class RentResultWindow : Window
    {
        private void DisplayMessage(RentResultType type)
        {
            switch (type)
            {
                case RentResultType.RentSuccess:
                    {
                        MessageBlock.Text = "Book rented successfully.";
                        break;
                    }
                case RentResultType.CannotSell:
                    {
                        MessageBlock.Text = "Error. This book is sold.";
                        break;
                    }
                default:
                    {
                        MessageBlock.Text = "Error.";
                        break;
                    }
            }
        }



        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
