using System.Windows;
using LibraryLogic.Data;

namespace WPFLibrary
{
    public partial class CannotRemoveErrorWindow : Window
    {
        private void DisplayErrorMessage(Book book)
        {
            string message =
                "You can't remove book '"
                + book.Title +
                "' written by "
                + book.Author +
                " because it's still rented to someone.";

            MessageBlock.Text = message;
        }

        private void DisplayErrorMessage(User user)
        {
            string message =
                "You can't remove "
                + user.GetUserName() +
                " because this user still possesses "
                + user.BooksNumber;

            if (user.BooksNumber > 1) message += " books.";
            else message += " book.";

            MessageBlock.Text = message;
        }



        private void OKButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
