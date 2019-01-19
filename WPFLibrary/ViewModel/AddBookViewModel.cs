using BookstoreLogic.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace WPFBookstore.ViewModel
{
    public class AddBookViewModel : ViewModelBase
    {
        private string _titleText;
        private string _authorText;
        private string _genreText;

        public AddBookViewModel()
        {
            SetupWindow();
        }

        public string TitleText
        {
            get => _titleText;
            set
            {
                _titleText = value;
                RaisePropertyChanged(() => TitleText);
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

        private void SetupWindow()
        {
            TitleText = "";
            AuthorText = "";
            GenreText = "";
        }

        private ICommand _addBookButtonLogic;
        private string _countText;

        public ICommand AddBookButtonCommand => _addBookButtonLogic ?? (_addBookButtonLogic = new RelayCommand(AddBookButtonLogic));

        public string CountText
        {
            get => _countText;
            set
            {
                _countText = value; 
                RaisePropertyChanged(()=>CountText);
            }
        }

        private void AddBookButtonLogic()
        {
            if (!string.IsNullOrEmpty(TitleText) && !string.IsNullOrEmpty(AuthorText) && !string.IsNullOrEmpty( GenreText) && !string.IsNullOrEmpty(CountText))
            {
                ViewModelLocator.Main.BookstoreService.AddBook(TitleText, AuthorText, GenreText, Int32.Parse(CountText));

                ViewModelLocator.Main.ShowList(BookstoreListSelection.Books);

                MessageBox.Show("Book added successfully.", "Success", MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Error. There are some text fields which are empty. Make sure that everything is filled correctly.", "Failure", MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }
        }

    }
}
