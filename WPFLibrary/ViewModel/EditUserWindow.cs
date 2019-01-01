using System.Windows;
using LibraryLogic.Data;

namespace WPFLibrary
{
    public partial class EditUserWindow : Window
    {
        private int editedUserId;

        private void SetupWindow(User user)
        {
            editedUserId = user.ID;

            NameText.Text = user.FirstName;
            SurnameText.Text = user.LastName;
        }

        private void UpdateUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfEmpty(NameText.Text) && !CheckIfEmpty(SurnameText.Text))
            {
                libService.UpdateUser(editedUserId, NameText.Text, SurnameText.Text);
                windowCallback();

                //Display success message
                if (editResultWindow == null || !editResultWindow.IsLoaded)
                {
                    editResultWindow = new ModificationResultWindow(ModificationResultType.UserEditCorrect);
                    editResultWindow.Show();
                }
            }
            else
            {
                //Display error message
                if (editResultWindow == null || !editResultWindow.IsLoaded)
                {
                    editResultWindow = new ModificationResultWindow(ModificationResultType.UserEditError);
                    editResultWindow.Show();
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
            if (editResultWindow != null && editResultWindow.IsLoaded)
                editResultWindow.Close();
            this.Close();
        }
    }
}