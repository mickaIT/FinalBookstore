using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using LibraryLogic.Data;
using LibraryLogic.Services;
using WPFBookstore.View;
using System.Linq.Expressions;

namespace WPFBookstore.ViewModel
{
    public enum BookstoreListSelection
    {
        Books,
        Invoices,
        None,
    }

    public class MainViewModel : ViewModelBase
    {
        public BookstoreDataService BookstoreService { get; set; }

        private BookstoreListSelection currentLLSelection = BookstoreListSelection.None;
        private AddBookWindow addBookWindow = null;
        private EditBookWindow editBookWindow = null;

        public MainViewModel()
        {

            BookstoreService = new BookstoreDataService();

            UpdateButtons();
            ShowList();
        }


        private ICommand _booksButtonCommand;
        public ICommand BooksButtonCommand => _booksButtonCommand ?? (_booksButtonCommand = new RelayCommand(BooksButtonLogic));

        private ICommand _invoiceButtonCommand;
        public ICommand InvoiceButtonCommand => _invoiceButtonCommand ?? (_invoiceButtonCommand = new RelayCommand(InvoiceButtonLogic));

        /* Selecting Bookstore lists */
        #region
        private void BooksButtonLogic()
        {
            currentLLSelection = BookstoreListSelection.Books;
            UpdateButtons();
            Refresh();
        }

        private void InvoiceButtonLogic()
        {
            currentLLSelection = BookstoreListSelection.Invoices;
            UpdateButtons();
            Refresh();
        }
        #endregion

        #region
        public void Refresh()
        {
            ShowList();
        }

        #endregion


        /* Updating buttons when Bookstore List selection changes */
        #region
        private void UpdateButtons()
        {
            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    ControlsText = "Manage Books";
                    AddButtonContent = "Add Book";
                    EditButtonContent = "Edit Book";
                    RemoveButtonContent = "Remove Book";
                    RemoveAllButtonContent = "Remove All Books";

                    AddButtonVisibility = Visibility.Visible;
                    SellBookButtonVisibility = Visibility.Visible;
                    EditButtonVisibility = Visibility.Visible;

                    RemoveButtonVisibility = Visibility.Visible;
                    RemoveAllButtonVisibility = Visibility.Visible;
                    break;
                }
                case BookstoreListSelection.Invoices:
                {
                    ControlsText = "No actions to perform select \"Books\" to manage data";
                    AddButtonContent = "";
                    EditButtonContent = "";
                    RemoveButtonContent = "";
                    RemoveAllButtonContent = "";

                    AddButtonVisibility = Visibility.Hidden;
                    SellBookButtonVisibility = Visibility.Hidden;

                    EditButtonVisibility = Visibility.Hidden;

                    RemoveButtonVisibility = Visibility.Hidden;
                    RemoveAllButtonVisibility = Visibility.Hidden;
                    break;
                }
                case BookstoreListSelection.None:
                default:
                {
                    ControlsText = "";
                    AddButtonContent = "";
                    EditButtonContent = "";
                    RemoveButtonContent = "";
                    RemoveAllButtonContent = "";

                    AddButtonVisibility = Visibility.Hidden;
                    SellBookButtonVisibility = Visibility.Hidden;

                    EditButtonVisibility = Visibility.Hidden;

                    RemoveButtonVisibility = Visibility.Hidden;
                    RemoveAllButtonVisibility = Visibility.Hidden;
                    break;
                }
            }
        }
        #endregion

        #region

        public void ShowList(BookstoreListSelection refresh)
        {
            currentLLSelection = refresh;
            ShowList();
        }

        private void ShowList()
        {
            DataContextContainerItems = null;

            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    DataContextTitleContent = "Books";
                    DataContextContainerItems = new ObservableCollection<string>(BookstoreService.GetAllBooks().Select(book => book.ToString()));
                    break;
                }
                case BookstoreListSelection.Invoices:
                {
                    DataContextTitleContent = "Invoices history";
                    DataContextContainerItems = new ObservableCollection<string>(BookstoreService.GetAllInvoices().Select(sale => sale.ToString()));
                    break;
                }
                default:
                {
                    DataContextTitleContent = "";
                    break;
                }
            }
        }

        #endregion

        /* Adding */
        #region

        private ICommand _addButtonCommand;
        public ICommand AddButtonCommand => _addButtonCommand ?? (_addButtonCommand = new RelayCommand(AddButtonLogic));

        private void AddButtonLogic()
        {
            /* Decide what to do basing on current Bookstore List selection */
            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    if (addBookWindow == null || !addBookWindow.IsLoaded)
                    {
                        addBookWindow = new AddBookWindow();
                        addBookWindow.Show();
                    }
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        #endregion


        /* Editing */

        #region


        private object _dataContextContainerSelectedItem;

        public object DataContextContainerSelectedItem
        {
            get => _dataContextContainerSelectedItem;
            set
            {
                _dataContextContainerSelectedItem = value;
                RaisePropertyChanged(() => DataContextContainerSelectedItem);
            }
        }

        public void DataContextContainerSelectionChangedCommand()
        {
            MessageBox.Show("lol");

        }

        private ICommand _editButtonCommand;
        public ICommand EditButtonCommand => _editButtonCommand ?? (_editButtonCommand = new RelayCommand(EditButtonLogic));

        private void EditButtonLogic()
        {
            object selection = DataContextContainerSelectedItem;

            if (selection != null)
            {
                switch (currentLLSelection)
                {
                    case BookstoreListSelection.Books:
                    {
                        //Book book = Book.FromString(selection.ToString());

                        if (editBookWindow == null || !editBookWindow.IsLoaded)
                        {
                            editBookWindow = new EditBookWindow();
                            editBookWindow.Show();
                        }
                        break;
                    }
                    default:
                    {
                        break;
                    }
                }
            }
        }
        #endregion


        /* Selling books */
        #region

        private ICommand _sellButtonCommand;
        public ICommand SellButtonCommand => _sellButtonCommand ?? (_sellButtonCommand = new RelayCommand(SellBookButtonLogic));


        private void SellBookButtonLogic()
        {
            object selection = DataContextContainerSelectedItem;

            if (selection != null)
            {
                Book book = Book.FromString(selection.ToString());

                if (book.State != BookState.Available)
                {
                    /* Error */
                    MessageBox.Show("Error. This book is sold.", "Failure", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    /* Try to sell the book */

                    MessageBoxResult result = MessageBox.Show("Do you want to sell?", "Selling", MessageBoxButton.YesNo,
                        MessageBoxImage.Question);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            try
                            {
                                BookstoreService.AddSale(book.ISBN);
                                ShowList(BookstoreListSelection.Books);
                                MessageBox.Show("Succesfully sold", "Selling", MessageBoxButton.OK,
                                    MessageBoxImage.Information);
                            }
                            catch (UnavailableBookException)
                            {
                                MessageBox.Show("Not sold, no more books", "Selling", MessageBoxButton.OK,
                                    MessageBoxImage.Error);
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }
        #endregion


        /* Removing */
        #region

        private ICommand _removeButtonCommand;
        public ICommand RemoveButtonCommand => _removeButtonCommand ?? (_removeButtonCommand = new RelayCommand(RemoveButtonLogic));

        private void RemoveButtonLogic()
        {
            object selection = DataContextContainerSelectedItem;

            if (selection == null)
            {
                MessageBox.Show("Select sth, pl0x", "Human, pl0x, stahp", MessageBoxButton.OK,
                    MessageBoxImage.Information);
                return;
            }

            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    Book book = Book.FromString(selection.ToString());

                    try
                    {
                        BookstoreService.RemoveBook(book);
                        Refresh();
                    }
                    catch (NullReferenceException)
                    {
                        MessageBox.Show("Book not found", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    break;
                }
            }

        }


        private ICommand _removeAllButtonCommand;
        public ICommand RemoveAllButtonCommand => _removeAllButtonCommand ?? (_removeAllButtonCommand = new RelayCommand(RemoveAllButtonLogic));

        private void RemoveAllButtonLogic()
        {
            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    BookstoreService.RemoveAllBooks();
                    Refresh();
                    break;
                }
                default:
                {
                    break;
                }
            }
        }
        #endregion


        /* Showing different lists */


        private string _controlsText;
        private string _addButtonContent;
        private string _editButtonContent;
        private string _removeButtonContent;
        private string _removeAllButtonContent;
        private Visibility _addButtonVisibility;
        private Visibility _sellBookButtonVisibility;
        private Visibility _editButtonVisibility;
        private Visibility _removeButtonVisibility;
        private Visibility _removeAllButtonVisibility;
        private string _dataContextTitleContent;
        private ObservableCollection<string> _dataContextContainerItems;

        public string ControlsText
        {
            get => _controlsText;
            set
            {
                _controlsText = value;
                RaisePropertyChanged(() => ControlsText);
            }

        }

        public string AddButtonContent
        {
            get => _addButtonContent;
            set
            {
                _addButtonContent = value;
                RaisePropertyChanged(() => AddButtonContent);
            }
        }


        public string EditButtonContent
        {
            get => _editButtonContent;
            set
            {
                _editButtonContent = value;
                RaisePropertyChanged(() => EditButtonContent);
            }

        }

        public string RemoveButtonContent
        {
            get => _removeButtonContent;
            set
            {
                _removeButtonContent = value;
                RaisePropertyChanged(() => RemoveButtonContent);
            }

        }

        public string RemoveAllButtonContent
        {
            get => _removeAllButtonContent;
            set
            {
                _removeAllButtonContent = value;
                RaisePropertyChanged(() => RemoveButtonContent);
            }
        }

        public Visibility AddButtonVisibility
        {
            get => _addButtonVisibility;
            set
            {
                _addButtonVisibility = value;
                RaisePropertyChanged(() => AddButtonVisibility);
            }
        }

        public Visibility SellBookButtonVisibility
        {
            get => _sellBookButtonVisibility;
            set
            {
                _sellBookButtonVisibility = value;
                RaisePropertyChanged(() => SellBookButtonVisibility);
            }
        }

        public Visibility EditButtonVisibility
        {
            get => _editButtonVisibility;
            set
            {
                _editButtonVisibility = value;
                RaisePropertyChanged(() => EditButtonVisibility);
            }
        }

        public Visibility RemoveButtonVisibility
        {
            get => _removeButtonVisibility;
            set
            {
                _removeButtonVisibility = value;
                RaisePropertyChanged(() => RemoveButtonVisibility);
            }
        }

        public Visibility RemoveAllButtonVisibility
        {
            get => _removeAllButtonVisibility;
            set
            {
                _removeAllButtonVisibility = value;
                RaisePropertyChanged(() => RemoveAllButtonVisibility);
            }
        }

        public string DataContextTitleContent
        {
            get => _dataContextTitleContent;
            set
            {
                _dataContextTitleContent = value;
                RaisePropertyChanged(() => DataContextTitleContent);
            }
        }

        public ObservableCollection<string> DataContextContainerItems
        {
            get => _dataContextContainerItems;
            set
            {
                _dataContextContainerItems = value;
                RaisePropertyChanged(() => DataContextContainerItems);
            }
        }

    }
}