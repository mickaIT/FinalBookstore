using System.Windows;
using BookstoreLogic.Services;

namespace WPFBookstore
{
    public partial class MainWindow : Window
    {
        public BookstoreDataService libService { get; set; }
        BookstoreListSelection currentLLSelection = BookstoreListSelection.None;

        AddBookWindow addBookWindow = null;
        EditBookWindow editBookWindow = null;

        CannotRemoveErrorWindow removalErrorWindow = null;
        BookReturnResultWindow bookReturnResultWindow = null;
        BookRentingWindow bookRentingWindow = null;
        RentResultWindow rentResultWindow = null;


        public MainWindow()
        {
            libService = new BookstoreDataService();

            InitializeComponent();
            UpdateButtons();
            ShowList();
        }
    }



    public enum BookstoreListSelection
    {
        Books,
        Rentals,
        None,
    }
}
