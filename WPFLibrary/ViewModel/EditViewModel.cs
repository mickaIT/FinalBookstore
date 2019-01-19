using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using LibraryLogic.Data;

namespace WPFBookstore.ViewModel
{
    public class EditViewModel : ViewModelBase
    {
        private int editedBookId;
        private string _titleText;
        private string _authorText;
        private string _genreText;

        public string TitleText
        {
            get => _titleText;
            set
            {
                _titleText = value;
                RaisePropertyChanged(
                    () => TitleText);

            }
        }

        public string AuthorText
        {
            get => _authorText;
            set
            {
                _authorText = value;
                RaisePropertyChanged(() => AuthorText);
            }
        }

        public string GenreText
        {
            get => _genreText;
            set
            {
                _genreText = value;
                RaisePropertyChanged(() => GenreText);
            }
        }

        public EditViewModel()
        {
            object selection = ViewModelLocator.Main.DataContextContainerSelectedItem;

            if (selection == null)
            {
                MessageBox.Show("Select sth, pl0x", "Human, pl0x, stahp", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            SetupWindow(Book.FromString(selection.ToString()));
        }

        private void SetupWindow(Book book)
        {
            editedBookId = book.ISBN;

            TitleText = book.Title;
            AuthorText = book.Author;
            GenreText = book.Genre;
        }

        private ICommand _updateButtonCommand;
        public ICommand UpdateButtonCommand => _updateButtonCommand ?? (_updateButtonCommand = new RelayCommand(UpdateBookButtonLogic));


        private void UpdateBookButtonLogic()
        {
            if (!CheckIfEmpty(TitleText.ToString()) && !CheckIfEmpty(AuthorText.ToString()) && !CheckIfEmpty(GenreText.ToString()))
            {
                ViewModelLocator.Main.BookstoreService.UpdateBook(editedBookId, TitleText, AuthorText, GenreText);
                ViewModelLocator.Main.ShowList(BookstoreListSelection.Books);

                //Display success message
                MessageBox.Show("Book updated successfully.", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                //Display error message
                MessageBox.Show(
                    "Error. There might be some text fields which are empty. Make sure that everything is filled correctly.",
                    "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private bool CheckIfEmpty(string text)
        {
            for (int i = 0; i < text.Length; i++)
            {
                if (text[i].ToString() != " ")
                {
                    return false;
                }
            }
            return true;
        }

    }
}
