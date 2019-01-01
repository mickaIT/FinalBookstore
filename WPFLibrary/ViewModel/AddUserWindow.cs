using System;
using System.Windows;


namespace WPFLibrary
{
    public partial class AddUserWindow : Window
    {
        private void SetupWindow()
        {
            NameText.Text = "";
            SurnameText.Text = "";
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckIfEmpty(NameText.Text) && !CheckIfEmpty(SurnameText.Text))
            {
                libService.AddUser(NameText.Text, SurnameText.Text);
                windowCallback();

                //Display success message
                if (addResultWindow == null || !addResultWindow.IsLoaded)
                {
                    addResultWindow = new ModificationResultWindow(ModificationResultType.UserAddition);
                    addResultWindow.Show();
                }
            }
            else
            {
                //Display error message
                if (addResultWindow == null || !addResultWindow.IsLoaded)
                {
                    addResultWindow = new ModificationResultWindow(ModificationResultType.UserAdditionFail);
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
