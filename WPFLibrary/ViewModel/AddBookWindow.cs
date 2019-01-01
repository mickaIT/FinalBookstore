using System.Windows;

namespace WPFLibrary
{
    public partial class AddBookWindow : Window
    {
        private void SetupWindow()
        {
            TitleText.Text = "";
            AuthorText.Text = "";
            GenreText.Text = "";
        }

        private void AddBookButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfEmpty(TitleText.Text) && !CheckIfEmpty(AuthorText.Text) && !CheckIfEmpty(GenreText.Text))
            {
                libService.AddBook(TitleText.Text, AuthorText.Text, GenreText.Text);
                windowCallback();

                //Display success message
                if (addResultWindow == null || !addResultWindow.IsLoaded)
                {
                    addResultWindow = new ModificationResultWindow(ModificationResultType.BookAddition);
                    addResultWindow.Show();
                }
            }
            else
            {
                //Display error message
                if (addResultWindow == null || !addResultWindow.IsLoaded)
                {
                    addResultWindow = new ModificationResultWindow(ModificationResultType.BookAdditionFail);
                    addResultWindow.Show();
                }
            }
        }

        private bool CheckIfEmpty(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].ToString() != " ")
                    return false;
            }
            return true;
        }



        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            if (addResultWindow != null && addResultWindow.IsLoaded)
                addResultWindow.Close();
            this.Close();
        }
    }
}