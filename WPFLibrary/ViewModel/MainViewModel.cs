using BookstoreLogic.Data;
using BookstoreLogic.Services;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;


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
        public BookstoreDataService bookstoreService { get; set; }

        private BookstoreListSelection currentLLSelection = BookstoreListSelection.None;
        private AddBookWindow addBookWindow = null;
        private EditBookWindow editBookWindow = null;
        private CannotRemoveErrorWindow removalErrorWindow = null;
        private BookChangeStatusResultWindow bookChangeStatusResultWindow = null;
        private BookSellingWindow bookSellingWindow = null;
        private SellResultWindow sellResultWindow = null;

        public MainViewModel()
        {

            bookstoreService = new BookstoreDataService();

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

        public void SellReturn(bool success)
        {
            ShowList();

            if (success)
            {
                /* Success */
                if (sellResultWindow == null || !sellResultWindow.IsLoaded)
                {
                    sellResultWindow = new SellResultWindow(SellResultType.SellSuccess);
                    sellResultWindow.Show();
                }
            }
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
        private void ShowList()
        {
            DataContextContainerItems = null;

            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    DataContextTitleContent = "Books";
                    DataContextContainerItems = new ObservableCollection<string>(bookstoreService.GetAllBooks().Select(book => book.ToString()));
                    break;
                }
                case BookstoreListSelection.Invoices:
                {
                    DataContextTitleContent = "Invoices history";
                    DataContextContainerItems = new ObservableCollection<string>(bookstoreService.GetAllInvoices().Select(sale => sale.ToString()));
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
                        addBookWindow = new AddBookWindow(bookstoreService, Refresh);
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
                RaisePropertyChanged(()=>DataContextContainerSelectedItem);
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
                        Book book = Book.FromString(selection.ToString());

                        if (editBookWindow == null || !editBookWindow.IsLoaded)
                        {
                            editBookWindow = new EditBookWindow(bookstoreService, book, Refresh);
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
                Book book = (Book)selection;

                if (book.State != BookState.Available)
                {
                    /* Error */
                    if (sellResultWindow == null || !sellResultWindow.IsLoaded)
                    {
                        sellResultWindow = new SellResultWindow(SellResultType.CannotSell);
                        sellResultWindow.Show();
                    }
                }
                else
                {
                    /* Try to sell the book */
                    if (bookSellingWindow == null || !bookSellingWindow.IsLoaded)
                    {
                        bookSellingWindow = new BookSellingWindow(bookstoreService, book, SellReturn);
                        bookSellingWindow.Show();
                    }
                }
            }
        }

        private ICommand _changeStatusButtonCommand;
        public ICommand ChangeStatusButtonCommand => _changeStatusButtonCommand ?? (_changeStatusButtonCommand = new RelayCommand(ChangeStatusButtonLogic));

        private void ChangeStatusButtonLogic()
        {
            object selection = DataContextContainerSelectedItem;

            if (selection != null)
            {
                Book book = (Book)selection;

                if (book.State == BookState.Available)
                {
                    /* Error */
                    if (bookChangeStatusResultWindow == null || !bookChangeStatusResultWindow.IsLoaded)
                    {
                        bookChangeStatusResultWindow = new BookChangeStatusResultWindow(false);
                    }
                }
                else
                {
                    bookstoreService.RemoveSale(book);

                    /* Success */
                    if (bookChangeStatusResultWindow == null || !bookChangeStatusResultWindow.IsLoaded)
                    {
                        bookChangeStatusResultWindow = new BookChangeStatusResultWindow(true);
                        bookChangeStatusResultWindow.Show();
                    }
                }

                Refresh();
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

            if (selection != null)
            {
                switch (currentLLSelection)
                {
                    case BookstoreListSelection.Books:
                    {
                        Book book = (Book)selection;

                        try
                        {
                            bookstoreService.RemoveBook(book);
                            Refresh();
                        }
                        catch
                        {
                            removalErrorWindow = new CannotRemoveErrorWindow(book);
                            removalErrorWindow.Show();
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


        private ICommand _removeAllButtonCommand;
        public ICommand RemoveAllButtonCommand => _removeAllButtonCommand ?? (_removeAllButtonCommand = new RelayCommand(RemoveAllButtonLogic));

        private void RemoveAllButtonLogic()
        {
            switch (currentLLSelection)
            {
                case BookstoreListSelection.Books:
                {
                    bookstoreService.RemoveAllBooks();
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